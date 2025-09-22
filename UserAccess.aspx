<%@ page title="User Access(Group Authority)" language="VB" masterpagefile="~/MasterPage.master" autoeventwireup="false" CodeFile="UserAccess.aspx.vb" Inherits="UserAccess" enableeventvalidation="false" culture="en-GB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script runat="server">

   
</script>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     
    <script type="text/javascript">
    function CheckBoxListSelectContact(cbControl) {
        var chkBoxSelectAll = document.getElementById("<%=chkContactSelectAll.ClientID%>").checked;
          
        var cbCompany = document.getElementById('<%=chkCompanyList.ClientID%>').getElementsByTagName("input");  
        var cbPerson = document.getElementById('<%=chkPersonList.ClientID%>').getElementsByTagName("input");  
        var cbUserStaff = document.getElementById('<%=chkUserStaffList.ClientID%>').getElementsByTagName("input");  
        var cbReports = document.getElementById('<%=chkReportsList.ClientID%>').getElementsByTagName("input");  
     
        if (chkBoxSelectAll == false) {
            for (i = 0; i < cbCompany.length; i++) cbCompany[i].checked = false;
            for (i = 0; i < cbPerson.length; i++) cbPerson[i].checked = false;
            for (i = 0; i < cbUserStaff.length; i++) cbUserStaff[i].checked = false;
            for (i = 0; i < cbReports.length; i++) cbReports[i].checked = false;
        }
        else  if (chkBoxSelectAll == true) {
            for (i = 0; i < cbCompany.length; i++) cbCompany[i].checked = true;
            for (i = 0; i < cbPerson.length; i++) cbPerson[i].checked = true;
            for (i = 0; i < cbUserStaff.length; i++) cbUserStaff[i].checked = true;
            for (i = 0; i < cbReports.length; i++) cbReports[i].checked = true;
        }
       
    }

    function CheckBoxListSelectReport(cbControl) {
        var chkBoxSelectAll = document.getElementById("<%=chkReportSelectAll.ClientID%>").checked;

           var cbReports = document.getElementById('<%=chkReportsList.ClientID%>').getElementsByTagName("input");

         if (chkBoxSelectAll == false) {
               for (i = 0; i < cbReports.length; i++) cbReports[i].checked = false;
         }
         else if (chkBoxSelectAll == true) {
              for (i = 0; i < cbReports.length; i++) cbReports[i].checked = true;
         }

    }

        function CheckBoxListSelectAsset(cbControl) {
            var chkBoxSelectAll = document.getElementById("<%=chkAssetSelectAll.ClientID%>").checked;

            var cbAsset = document.getElementById('<%=chkAssetList.ClientID%>').getElementsByTagName("input");
            var cbAssetSupplier = document.getElementById('<%=chkAssetSupplierList.ClientID%>').getElementsByTagName("input");
            var cbAssetBrand = document.getElementById('<%=chkAssetBrandList.ClientID%>').getElementsByTagName("input");
            var cbAssetClass = document.getElementById('<%=chkAssetClassList.ClientID%>').getElementsByTagName("input");
            var cbAssetColor = document.getElementById('<%=chkAssetColorList.ClientID%>').getElementsByTagName("input");
            var cbAssetModel = document.getElementById('<%=chkAssetModelList.ClientID%>').getElementsByTagName("input");
            var cbAssetGroup = document.getElementById('<%=chkAssetGroupList.ClientID%>').getElementsByTagName("input");
            var cbAssetStatus = document.getElementById('<%=chkAssetStatusList.ClientID%>').getElementsByTagName("input");
            var cbAssetMovementType = document.getElementById('<%=chkAssetMovementTypeList.ClientID%>').getElementsByTagName("input");

               if (chkBoxSelectAll == false) {
                   for (i = 0; i < cbAsset.length; i++) cbAsset[i].checked = false;
                   for (i = 0; i < cbAssetSupplier.length; i++) cbAssetSupplier[i].checked = false;
                   for (i = 0; i < cbAssetBrand.length; i++) cbAssetBrand[i].checked = false;
                   for (i = 0; i < cbAssetClass.length; i++) cbAssetClass[i].checked = false;
                   for (i = 0; i < cbAssetColor.length; i++) cbAssetColor[i].checked = false;
                   for (i = 0; i < cbAssetModel.length; i++) cbAssetModel[i].checked = false;
                   for (i = 0; i < cbAssetGroup.length; i++) cbAssetGroup[i].checked = false;
                   for (i = 0; i < cbAssetStatus.length; i++) cbAssetStatus[i].checked = false;
                   for (i = 0; i < cbAssetMovementType.length; i++) cbAssetMovementType[i].checked = false;
               }
               else if (chkBoxSelectAll == true) {
                   for (i = 0; i < cbAsset.length; i++) cbAsset[i].checked = true;
                   for (i = 0; i < cbAssetSupplier.length; i++) cbAssetSupplier[i].checked = true;
                   for (i = 0; i < cbAssetBrand.length; i++) cbAssetBrand[i].checked = true;
                   for (i = 0; i < cbAssetClass.length; i++) cbAssetClass[i].checked = true;
                   for (i = 0; i < cbAssetColor.length; i++) cbAssetColor[i].checked = true;
                   for (i = 0; i < cbAssetModel.length; i++) cbAssetModel[i].checked = true;
                   for (i = 0; i < cbAssetGroup.length; i++) cbAssetGroup[i].checked = true;
                   for (i = 0; i < cbAssetStatus.length; i++) cbAssetStatus[i].checked = true;
                   for (i = 0; i < cbAssetMovementType.length; i++) cbAssetMovementType[i].checked = true;
               }

        }

    function CheckBoxListSelectSvc(cbControl) {
        var chkBoxSelectAll = document.getElementById("<%=chkSvcSelectAll.ClientID%>").checked;

          var cbService = document.getElementById('<%=chkServiceList.ClientID%>').getElementsByTagName("input");
          var cbContract = document.getElementById('<%=chkContractList.ClientID%>').getElementsByTagName("input");
     
          if (chkBoxSelectAll == false) {
              for (i = 0; i < cbService.length; i++) cbService[i].checked = false;
              for (i = 0; i < cbContract.length; i++) cbContract[i].checked = false;
            }
          else if (chkBoxSelectAll == true) {
              for (i = 0; i < cbService.length; i++) cbService[i].checked = true;
              for (i = 0; i < cbContract.length; i++) cbContract[i].checked = true;
             }

    }

    function CheckBoxListSelectAR(cbControl) {
        var chkBoxSelectAll = document.getElementById("<%=chkARSelectAll.ClientID%>").checked;

        var cbInvoice = document.getElementById('<%=chkInvoiceList.ClientID%>').getElementsByTagName("input");
        var cbBatchInvoice = document.getElementById('<%=chkBatchInvoiceList.ClientID%>').getElementsByTagName("input");
      
        var cbCreditNote = document.getElementById('<%=chkCreditNoteList.ClientID%>').getElementsByTagName("input");
        var cbReceipt = document.getElementById('<%=chkReceiptList.ClientID%>').getElementsByTagName("input");
        var cbAdjustment = document.getElementById('<%=chkAdjustmentList.ClientID%>').getElementsByTagName("input");

        if (chkBoxSelectAll == false) {
            for (i = 0; i < cbInvoice.length; i++) cbInvoice[i].checked = false;
            for (i = 0; i < cbCreditNote.length; i++) cbCreditNote[i].checked = false;
            for (i = 0; i < cbReceipt.length; i++) cbReceipt[i].checked = false;
            for (i = 0; i < cbAdjustment.length; i++) cbAdjustment[i].checked = false;
            for (i = 0; i < cbBatchInvoice.length; i++) cbBatchInvoice[i].checked = false;
        }
        else if (chkBoxSelectAll == true) {
            for (i = 0; i < cbInvoice.length; i++) cbInvoice[i].checked = true;
            for (i = 0; i < cbCreditNote.length; i++) cbCreditNote[i].checked = true;
            for (i = 0; i < cbReceipt.length; i++) cbReceipt[i].checked = true;
            for (i = 0; i < cbAdjustment.length; i++) cbAdjustment[i].checked = true;
            for (i = 0; i < cbBatchInvoice.length; i++) cbBatchInvoice[i].checked = true;
        }

    }

    function CheckBoxListSelectSetup(cbControl) {
        var chkBoxSelectAll = document.getElementById("<%=chkSetupSelectAll.ClientID%>").checked;

        var cbUserStaff = document.getElementById('<%=chkUserStaffList.ClientID%>').getElementsByTagName("input");
        var cbTerms = document.getElementById('<%=chkTermsList.ClientID%>').getElementsByTagName("input");  
    
        var cbCompanyGroup = document.getElementById('<%=chkCompanyGroupList.ClientID%>').getElementsByTagName("input");
        var cbCity = document.getElementById('<%=chkCityList.ClientID%>').getElementsByTagName("input");
        var cbState = document.getElementById('<%=chkStateList.ClientID%>').getElementsByTagName("input");
        var cbCountry = document.getElementById('<%=chkCountryList.ClientID%>').getElementsByTagName("input");

        var cbSchType = document.getElementById('<%=chkSchTypeList.ClientID%>').getElementsByTagName("input");
        var cbTarget = document.getElementById('<%=chkTargetList.ClientID%>').getElementsByTagName("input");
        var cbSvcFreq = document.getElementById('<%=chkSvcFreqList.ClientID%>').getElementsByTagName("input");
        var cbInvFreq = document.getElementById('<%=chkInvFreqList.ClientID%>').getElementsByTagName("input");

        var cbVehicle = document.getElementById('<%=chkVehicleList.ClientID%>').getElementsByTagName("input");
        var cbTeam = document.getElementById('<%=chkTeamList.ClientID%>').getElementsByTagName("input");
        var cbLocGroup = document.getElementById('<%=chkLocGroupList.ClientID%>').getElementsByTagName("input");
        var cbPostal = document.getElementById('<%=chkPostalList.ClientID%>').getElementsByTagName("input");

        var cbTermCode = document.getElementById('<%=chkTermCodeList.ClientID%>').getElementsByTagName("input");
        var cbUserAccess = document.getElementById('<%=chkUserAccessList.ClientID%>').getElementsByTagName("input");
        var cbContractGroupCategory = document.getElementById('<%=chkContractGroupCategoryList.ClientID%>').getElementsByTagName("input");
        var cbDepartment = document.getElementById('<%=chkDepartmentList.ClientID%>').getElementsByTagName("input");

        var cbServiceMaster = document.getElementById('<%=chkServiceMasterList.ClientID%>').getElementsByTagName("input");
        var cbIndustry = document.getElementById('<%=chkIndustryList.ClientID%>').getElementsByTagName("input");
        var cbEmailSetup = document.getElementById('<%=chkEmailSetupList.ClientID%>').getElementsByTagName("input");

        var cbHoliday = document.getElementById('<%=chkHolidayList.ClientID%>').getElementsByTagName("input");
        var cbUOM = document.getElementById('<%=chkUOMList.ClientID%>').getElementsByTagName("input");
        var cbCOA = document.getElementById('<%=chkCOAList.ClientID%>').getElementsByTagName("input");
        var cbTaxRate = document.getElementById('<%=chkTaxRateList.ClientID%>').getElementsByTagName("input");

        var cbBillingCode = document.getElementById('<%=chkBillingCodeList.ClientID%>').getElementsByTagName("input");
        var cbMassChange = document.getElementById('<%=chkMassChangeList.ClientID%>').getElementsByTagName("input");
        var cbEventLog = document.getElementById('<%=chkEventLogList.ClientID%>').getElementsByTagName("input");
        var cbMobile = document.getElementById('<%=chkMobileList.ClientID%>').getElementsByTagName("input");

        var cbCurrency = document.getElementById('<%=chkCurrencyList.ClientID%>').getElementsByTagName("input");

        var cbMarketSegment = document.getElementById('<%=chkMarketSegmentList.ClientID%>').getElementsByTagName("input");
        var cbServiceType = document.getElementById('<%=chkServiceTypeList.ClientID%>').getElementsByTagName("input");
        var cbServiceModule = document.getElementById('<%=chkServiceModuleList.ClientID%>').getElementsByTagName("input");

        var cbContactsModuleSetup = document.getElementById('<%=chkContactsModuleSetup.ClientID%>').getElementsByTagName("input");
        var cbLockServiceRecord = document.getElementById('<%=chkLockServiceRecord.ClientID%>').getElementsByTagName("input");
        var cbChemicalsList = document.getElementById('<%=chkChemicalsList.ClientID%>').getElementsByTagName("input");

        var cbBankList = document.getElementById('<%=chkBankList.ClientID%>').getElementsByTagName("input");
        var cbNotesTemplateList = document.getElementById('<%=chkNotesTemplateList.ClientID%>').getElementsByTagName("input");

        var cbSettlementTypeList = document.getElementById('<%=chkSettlementTypeList.ClientID%>').getElementsByTagName("input");
        var cbServiceActionList = document.getElementById('<%=chkServiceActionList.ClientID%>').getElementsByTagName("input");
        var cbPeriodList = document.getElementById('<%=chkPeriodList.ClientID%>').getElementsByTagName("input");
        var cbBatchEmailList = document.getElementById('<%=ChkBatchEmailList.ClientID%>').getElementsByTagName("input");
        var cbLocationList = document.getElementById('<%=chkLocationList.ClientID%>').getElementsByTagName("input");
        var cbSetupOPSList = document.getElementById('<%=chkOpsModuleList.ClientID%>').getElementsByTagName("input");
        var cbSetupARList = document.getElementById('<%=chkARModuleList.ClientID%>').getElementsByTagName("input");
        var cbSMSetupList = document.getElementById('<%=chkSMSSetupList.ClientID%>').getElementsByTagName("input");


        if (chkBoxSelectAll == false) {
            for (i = 0; i < cbUserStaff.length; i++) cbUserStaff[i].checked = false;
            for (i = 0; i < cbTerms.length; i++) cbTerms[i].checked = false;
        
              for (i = 0; i < cbCompanyGroup.length; i++) cbCompanyGroup[i].checked = false;
              for (i = 0; i < cbCity.length; i++) cbCity[i].checked = false;
              for (i = 0; i < cbState.length; i++) cbState[i].checked = false;
              for (i = 0; i < cbCountry.length; i++) cbCountry[i].checked = false;

              for (i = 0; i < cbSchType.length; i++) cbSchType[i].checked = false;
              for (i = 0; i < cbTarget.length; i++) cbTarget[i].checked = false;
              for (i = 0; i < cbSvcFreq.length; i++) cbSvcFreq[i].checked = false;
              for (i = 0; i < cbInvFreq.length; i++) cbInvFreq[i].checked = false;

              for (i = 0; i < cbVehicle.length; i++) cbVehicle[i].checked = false;
              for (i = 0; i < cbTeam.length; i++) cbTeam[i].checked = false;
              for (i = 0; i < cbLocGroup.length; i++) cbLocGroup[i].checked = false;
              for (i = 0; i < cbPostal.length; i++) cbPostal[i].checked = false;

              for (i = 0; i < cbTermCode.length; i++) cbTermCode[i].checked = false;
              for (i = 0; i < cbUserAccess.length; i++) cbUserAccess[i].checked = false;
              for (i = 0; i < cbContractGroupCategory.length; i++) cbContractGroupCategory[i].checked = false;
              for (i = 0; i < cbDepartment.length; i++) cbDepartment[i].checked = false;

              for (i = 0; i < cbServiceMaster.length; i++) cbServiceMaster[i].checked = false;
              for (i = 0; i < cbIndustry.length; i++) cbIndustry[i].checked = false;
              for (i = 0; i < cbEmailSetup.length; i++) cbEmailSetup[i].checked = false;

              for (i = 0; i < cbHoliday.length; i++) cbHoliday[i].checked = false;
              for (i = 0; i < cbUOM.length; i++) cbUOM[i].checked = false;
              for (i = 0; i < cbCOA.length; i++) cbCOA[i].checked = false;
              for (i = 0; i < cbTaxRate.length; i++) cbTaxRate[i].checked = false;

              for (i = 0; i < cbBillingCode.length; i++) cbBillingCode[i].checked = false;
              for (i = 0; i < cbMassChange.length; i++) cbMassChange[i].checked = false;
              for (i = 0; i < cbEventLog.length; i++) cbEventLog[i].checked = false;
              for (i = 0; i < cbMobile.length; i++) cbMobile[i].checked = false;

              for (i = 0; i < cbCurrency.length; i++) cbCurrency[i].checked = false;

              for (i = 0; i < cbMarketSegment.length; i++) cbMarketSegment[i].checked = false;
              for (i = 0; i < cbServiceType.length; i++) cbServiceType[i].checked = false;
              for (i = 0; i < cbServiceModule.length; i++) cbServiceModule[i].checked = false;

              for (i = 0; i < cbContactsModuleSetup.length; i++) cbContactsModuleSetup[i].checked = false;
              for (i = 0; i < cbLockServiceRecord.length; i++) cbLockServiceRecord[i].checked = false;
              for (i = 0; i < cbChemicalsList.length; i++) cbChemicalsList[i].checked = false;
              for (i = 0; i < cbSettlementTypeList.length; i++) cbSettlementTypeList[i].checked = false;
              for (i = 0; i < cbServiceActionList.length; i++) cbServiceActionList[i].checked = false;
            
              for (i = 0; i < cbBankList.length; i++) cbBankList[i].checked = false;
              for (i = 0; i < cbNotesTemplateList.length; i++) cbNotesTemplateList[i].checked = false;
              for (i = 0; i < cbPeriodList.length; i++) cbPeriodList[i].checked = false;
              for (i = 0; i < cbBatchEmailList.length; i++) cbBatchEmailList[i].checked = false;
              for (i = 0; i < cbLocationList.length; i++) cbLocationList[i].checked = false;
              for (i = 0; i < cbSetupOPSList.length; i++) cbSetupOPSList[i].checked = false;
              for (i = 0; i < cbSetupARList.length; i++) cbSetupARList[i].checked = false;
              for (i = 0; i < cbSMSetupList.length; i++) cbSetupARList[i].checked = false;
              
          }
        else if (chkBoxSelectAll == true) {
            for (i = 0; i < cbUserStaff.length; i++) cbUserStaff[i].checked = true;
            for (i = 0; i < cbTerms.length; i++) cbTerms[i].checked = true;

              for (i = 0; i < cbCompanyGroup.length; i++) cbCompanyGroup[i].checked = true;
              for (i = 0; i < cbCity.length; i++) cbCity[i].checked = true;
              for (i = 0; i < cbState.length; i++) cbState[i].checked = true;
              for (i = 0; i < cbCountry.length; i++) cbCountry[i].checked = true;

              for (i = 0; i < cbSchType.length; i++) cbSchType[i].checked = true;
              for (i = 0; i < cbTarget.length; i++) cbTarget[i].checked = true;
              for (i = 0; i < cbSvcFreq.length; i++) cbSvcFreq[i].checked = true;
              for (i = 0; i < cbInvFreq.length; i++) cbInvFreq[i].checked = true;

              for (i = 0; i < cbVehicle.length; i++) cbVehicle[i].checked = true;
              for (i = 0; i < cbTeam.length; i++) cbTeam[i].checked = true;
              for (i = 0; i < cbLocGroup.length; i++) cbLocGroup[i].checked = true;
              for (i = 0; i < cbPostal.length; i++) cbPostal[i].checked = true;

              for (i = 0; i < cbTermCode.length; i++) cbTermCode[i].checked = true;
              for (i = 0; i < cbUserAccess.length; i++) cbUserAccess[i].checked = true;
              for (i = 0; i < cbContractGroupCategory.length; i++) cbContractGroupCategory[i].checked = true;
              for (i = 0; i < cbDepartment.length; i++) cbDepartment[i].checked = true;

              for (i = 0; i < cbServiceMaster.length; i++) cbServiceMaster[i].checked = true;
              for (i = 0; i < cbIndustry.length; i++) cbIndustry[i].checked = true;
              for (i = 0; i < cbEmailSetup.length; i++) cbEmailSetup[i].checked = true;

              for (i = 0; i < cbHoliday.length; i++) cbHoliday[i].checked = true;
              for (i = 0; i < cbUOM.length; i++) cbUOM[i].checked = true;
              for (i = 0; i < cbCOA.length; i++) cbCOA[i].checked = true;
              for (i = 0; i < cbTaxRate.length; i++) cbTaxRate[i].checked = true;

              for (i = 0; i < cbBillingCode.length; i++) cbBillingCode[i].checked = true;
              for (i = 0; i < cbMassChange.length; i++) cbMassChange[i].checked = true;
              for (i = 0; i < cbEventLog.length; i++) cbEventLog[i].checked = true;
              for (i = 0; i < cbMobile.length; i++) cbMobile[i].checked = true;

              for (i = 0; i < cbCurrency.length; i++) cbCurrency[i].checked = true;

              for (i = 0; i < cbMarketSegment.length; i++) cbMarketSegment[i].checked = true;
              for (i = 0; i < cbServiceType.length; i++) cbServiceType[i].checked = true;
              for (i = 0; i < cbServiceModule.length; i++) cbServiceModule[i].checked = true;

              for (i = 0; i < cbContactsModuleSetup.length; i++) cbContactsModuleSetup[i].checked = true;
              for (i = 0; i < cbLockServiceRecord.length; i++) cbLockServiceRecord[i].checked = true;
              for (i = 0; i < cbChemicalsList.length; i++) cbChemicalsList[i].checked = true;
              for (i = 0; i < cbSettlementTypeList.length; i++) cbSettlementTypeList[i].checked = true;
              for (i = 0; i < cbServiceActionList.length; i++) cbServiceActionList[i].checked = true;

              for (i = 0; i < cbBankList.length; i++) cbBankList[i].checked = true;
              for (i = 0; i < cbNotesTemplateList.length; i++) cbNotesTemplateList[i].checked = true;
              for (i = 0; i < cbPeriodList.length; i++) cbPeriodList[i].checked = true;
              for (i = 0; i < cbBatchEmailList.length; i++) cbBatchEmailList[i].checked = true;
              for (i = 0; i < cbLocationList.length; i++) cbLocationList[i].checked = true;
          }

    }

    function TabChanged(sender, e) {
        if (sender.get_activeTabIndex() == 1 || sender.get_activeTabIndex() == 2|| sender.get_activeTabIndex() == 3|| sender.get_activeTabIndex() == 4|| sender.get_activeTabIndex() == 5) {

            if (document.getElementById("<%=txtGroupAuthority.ClientID%>").value == '') {
                $find('<%=tb1.ClientID%>').set_activeTabIndex(0);
                alert("Please select a group authority to proceed.");
                return;
            }
            document.getElementById('<%=GridView1.ClientID()%>').style.display = 'none';

            document.getElementById("<%=btnADD.ClientID%>").style.display = 'none';
             document.getElementById("<%=btnDelete.ClientID%>").style.display = 'none';
          
        }
        else {
            document.getElementById('<%=GridView1.ClientID()%>').style.display = 'block';
            document.getElementById("<%=btnADD.ClientID%>").style.display = 'inline';
            document.getElementById("<%=btnDelete.ClientID%>").style.display = 'inline';
         }

    }

        function ResetScrollPosition() {
            setTimeout("window.scrollTo(0,0)", 0);
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
    </script>

        <style type="text/css">
        
       
         .ajax__tab_xp .ajax__tab_header {font-family:Calibri;font-size:15px;text-align:left;}
     
    .CellFormat{
        font-size:15px;
        font-weight:bold;
        font-family:'Calibri';
        color:black;
        text-align:right;
        width:20%;
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
  
         .roundbutton1 {
            border: 2px solid #a1a1a1;   
    background: #dddddd;   
    border-radius: 25px;
    height:30px;
    width:90%;
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
               
            </ProgressTemplate>
        </asp:UpdateProgress>
 
     <asp:UpdatePanel ID="updPanelCompany" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
           <asp:controlBundle name="ModalPopupExtender_Bundle"/>
            <asp:controlBundle name="ListSearchExtender_Bundle"/>
               <asp:controlBundle name="TabContainer_Bundle"/>
                <asp:controlBundle name="CollapsiblePanelExtender_Bundle"/>
   </ControlBundles>
    </asp:ToolkitScriptManager>     
      <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">User Access (Group Authority)</h3>
     <table style="width:100%;text-align:center;">
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
              
                <td style="width:30%;text-align:left;">
             
                       </td>
                <td style="text-align: right">
                   
                  <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" />
                </td>
                
            </tr>
                  <tr>
              <td colspan="2">
               <br /></td>
            </tr>
            <tr class="Centered">
                <td colspan="2">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="650px" DataKeyNames="Rcno" OnRowDataBound = "OnRowDataBound" OnSelectedIndexChanged = "OnSelectedIndexChanged"  DataSourceID="SqlDataSource1" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True">
                        <Columns>
                              <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False">
                <ControlStyle Width="100px" />
                <ItemStyle Width="100px" />
                </asp:CommandField>
                            <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                 <EditItemTemplate>
                                     <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                                 </EditItemTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                 </ItemTemplate>
                             </asp:TemplateField>
                            <asp:BoundField DataField="GroupAccess" HeaderText="GroupAccess" SortExpression="GroupAccess" >
                                 <ControlStyle Width="230px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="230px" HorizontalAlign="Left" />
                </asp:BoundField>
                             <asp:BoundField DataField="Comments" HeaderText="Description" SortExpression="Comments" >
                                 <ControlStyle Width="350px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="350px" HorizontalAlign="Left" />
                </asp:BoundField>
           
                            <asp:BoundField DataField="x0302" HeaderText="x0302" SortExpression="x0302" Visible="False" />
                            <asp:BoundField DataField="x0302Add" HeaderText="x0302Add" SortExpression="x0302Add" Visible="False" />
                            <asp:BoundField DataField="x0302Edit" HeaderText="x0302Edit" SortExpression="x0302Edit" Visible="False" />
                            <asp:BoundField DataField="x0302Delete" HeaderText="x0302Delete" SortExpression="x0302Delete" Visible="False" />
                            <asp:BoundField DataField="x0302Trans" HeaderText="x0302Trans" SortExpression="x0302Trans" Visible="False" />
                            <asp:BoundField DataField="x0303" HeaderText="x0303" SortExpression="x0303" Visible="False" />
                            <asp:BoundField DataField="x0303Add" HeaderText="x0303Add" SortExpression="x0303Add" Visible="False" />
                            <asp:BoundField DataField="x0303Edit" HeaderText="x0303Edit" SortExpression="x0303Edit" Visible="False" />
                            <asp:BoundField DataField="x0303Delete" HeaderText="x0303Delete" SortExpression="x0303Delete" Visible="False" />
                            <asp:BoundField DataField="x0303Trans" HeaderText="x0303Trans" SortExpression="x0303Trans" Visible="False" />
                            <asp:BoundField DataField="x0304" HeaderText="x0304" SortExpression="x0304" Visible="False" />
                            <asp:BoundField DataField="x0304Add" HeaderText="x0304Add" SortExpression="x0304Add" Visible="False" />
                            <asp:BoundField DataField="x0304Edit" HeaderText="x0304Edit" SortExpression="x0304Edit" Visible="False" />
                            <asp:BoundField DataField="x0304Delete" HeaderText="x0304Delete" SortExpression="x0304Delete" Visible="False" />
                            <asp:BoundField DataField="x0304Trans" HeaderText="x0304Trans" SortExpression="x0304Trans" Visible="False" />
                            <asp:BoundField DataField="x0304Security" HeaderText="x0304Security" SortExpression="x0304Security" Visible="False" />
                            <asp:BoundField DataField="x0305" HeaderText="x0305" SortExpression="x0305" Visible="False" />
                            <asp:BoundField DataField="x0302EditAcct" HeaderText="x0302EditAcct" SortExpression="x0302EditAcct" Visible="False" />
                            <asp:BoundField DataField="x0302Change" HeaderText="x0302Change" SortExpression="x0302Change" Visible="False" />
                            <asp:BoundField DataField="x0302Notes" HeaderText="x0302Notes" SortExpression="x0302Notes" Visible="False" />
                            <asp:BoundField DataField="x0303Notes" HeaderText="x0303Notes" SortExpression="x0303Notes" Visible="False" />
                            <asp:BoundField DataField="x0302ViewAll" HeaderText="x0302ViewAll" SortExpression="x0302ViewAll" Visible="False" />
                            <asp:BoundField DataField="x0306" HeaderText="x0306" SortExpression="x0306" Visible="False" />
                            <asp:BoundField DataField="x2414" HeaderText="x2414" SortExpression="x2414" Visible="False" />
                            <asp:BoundField DataField="x2414Add" HeaderText="x2414Add" SortExpression="x2414Add" Visible="False" />
                            <asp:BoundField DataField="x2414Edit" HeaderText="x2414Edit" SortExpression="x2414Edit" Visible="False" />
                            <asp:BoundField DataField="x2414Delete" HeaderText="x2414Delete" SortExpression="x2414Delete" Visible="False" />
                            <asp:BoundField DataField="x2414Print" HeaderText="x2414Print" SortExpression="x2414Print" Visible="False" />
                            <asp:BoundField DataField="x2414Button" HeaderText="x2414Button" SortExpression="x2414Button" Visible="False" />
                            <asp:BoundField DataField="x2411" HeaderText="x2411" SortExpression="x2411" Visible="False" />
                            <asp:BoundField DataField="x2411Set1" HeaderText="x2411Set1" SortExpression="x2411Set1" Visible="False" />
                            <asp:BoundField DataField="x2411JobOrder" HeaderText="x2411JobOrder" SortExpression="x2411JobOrder" Visible="False" />
                            <asp:BoundField DataField="x2411Asset" HeaderText="x2411Asset" SortExpression="x2411Asset" Visible="False" />
                            <asp:BoundField DataField="x2411Contract" HeaderText="x2411Contract" SortExpression="x2411Contract" Visible="False" />
                            <asp:BoundField DataField="x2411Set2" HeaderText="x2411Set2" SortExpression="x2411Set2" Visible="False" />
                            <asp:BoundField DataField="x2415Print" HeaderText="x2415Print" SortExpression="x2415Print" Visible="False" />
                            <asp:BoundField DataField="x2415ChSt" HeaderText="x2415ChSt" SortExpression="x2415ChSt" Visible="False" />
                            <asp:BoundField DataField="x2415" HeaderText="x2415" SortExpression="x2415" Visible="False" />
                            <asp:BoundField DataField="x2415Add" HeaderText="x2415Add" SortExpression="x2415Add" Visible="False" />
                            <asp:BoundField DataField="x2415Edit" HeaderText="x2415Edit" SortExpression="x2415Edit" Visible="False" />
                            <asp:BoundField DataField="x2415Delete" HeaderText="x2415Delete" SortExpression="x2415Delete" Visible="False" />
                            <asp:BoundField DataField="x2412" HeaderText="x2412" SortExpression="x2412" Visible="False" />
                            <asp:BoundField DataField="x2412Add" HeaderText="x2412Add" SortExpression="x2412Add" Visible="False" />
                            <asp:BoundField DataField="x2412Edit" HeaderText="x2412Edit" SortExpression="x2412Edit" Visible="False" />
                            <asp:BoundField DataField="x2412Delete" HeaderText="x2412Delete" SortExpression="x2412Delete" Visible="False" />
                            <asp:BoundField DataField="x2412Print" HeaderText="x2412Print" SortExpression="x2412Print" Visible="False" />
                            <asp:BoundField DataField="x2412ChSt" HeaderText="x2412ChSt" SortExpression="x2412ChSt" Visible="False" />
                            <asp:BoundField DataField="x2412Update" HeaderText="x2412Update" SortExpression="x2412Update" Visible="False" />
                            <asp:BoundField DataField="x2412Reverse" HeaderText="x2412Reverse" SortExpression="x2412Reverse" Visible="False" />
                            <asp:BoundField DataField="x2412Process" HeaderText="x2412Process" SortExpression="x2412Process" Visible="False" />
                            <asp:BoundField DataField="x2412Early" HeaderText="x2412Early" SortExpression="x2412Early" Visible="False" />
                            <asp:BoundField DataField="x2412Term" HeaderText="x2412Term" SortExpression="x2412Term" Visible="False" />
                            <asp:BoundField DataField="x2412Cancel" HeaderText="x2412Cancel" SortExpression="x2412Cancel" Visible="False" />
                            <asp:BoundField DataField="x2412Renewal" HeaderText="x2412Renewal" SortExpression="x2412Renewal" Visible="False" />
                            <asp:BoundField DataField="x2413Print" HeaderText="x2413Print" SortExpression="x2413Print" Visible="False" />
                            <asp:BoundField DataField="x2413ChSt" HeaderText="x2413ChSt" SortExpression="x2413ChSt" Visible="False" />
                            <asp:BoundField DataField="x2413Update" HeaderText="x2413Update" SortExpression="x2413Update" Visible="False" />
                            <asp:BoundField DataField="x2413" HeaderText="x2413" SortExpression="x2413" Visible="False" />
                            <asp:BoundField DataField="x2413Add" HeaderText="x2413Add" SortExpression="x2413Add" Visible="False" />
                            <asp:BoundField DataField="x2413Edit" HeaderText="x2413Edit" SortExpression="x2413Edit" Visible="False" />
                            <asp:BoundField DataField="x2413Delete" HeaderText="x2413Delete" SortExpression="x2413Delete" Visible="False" />
                            <asp:BoundField DataField="x2413Reverse" HeaderText="x2413Reverse" SortExpression="x2413Reverse" Visible="False" />
                            <asp:BoundField DataField="x2401" HeaderText="x2401" SortExpression="x2401" Visible="False" />
                            <asp:BoundField DataField="x2416" HeaderText="x2416" SortExpression="x2416" Visible="False" />
                            <asp:BoundField DataField="x2416Add" HeaderText="x2416Add" SortExpression="x2416Add" Visible="False" />
                            <asp:BoundField DataField="x2416Edit" HeaderText="x2416Edit" SortExpression="x2416Edit" Visible="False" />
                            <asp:BoundField DataField="x2416Delete" HeaderText="x2416Delete" SortExpression="x2416Delete" Visible="False" />
                            <asp:BoundField DataField="x2416Print" HeaderText="x2416Print" SortExpression="x2416Print" Visible="False" />
                            <asp:BoundField DataField="x2416ChSt" HeaderText="x2416ChSt" SortExpression="x2416ChSt" Visible="False" />
                            <asp:BoundField DataField="x2416ToQuote" HeaderText="x2416ToQuote" SortExpression="x2416ToQuote" Visible="False" />
                            <asp:BoundField DataField="x2416JobOrder" HeaderText="x2416JobOrder" SortExpression="x2416JobOrder" Visible="False" />
                            <asp:BoundField DataField="x2416KIV" HeaderText="x2416KIV" SortExpression="x2416KIV" Visible="False" />
                            <asp:BoundField DataField="x2416Noted" HeaderText="x2416Noted" SortExpression="x2416Noted" Visible="False" />
                            <asp:BoundField DataField="x2416Attended" HeaderText="x2416Attended" SortExpression="x2416Attended" Visible="False" />
                            <asp:BoundField DataField="x2416Voided" HeaderText="x2416Voided" SortExpression="x2416Voided" Visible="False" />
                            <asp:BoundField DataField="x2416Improvement" HeaderText="x2416Improvement" SortExpression="x2416Improvement" Visible="False" />
                            <asp:BoundField DataField="x2416SMS" HeaderText="x2416SMS" SortExpression="x2416SMS" Visible="False" />
                            <asp:BoundField DataField="x2416ActionBy" HeaderText="x2416ActionBy" SortExpression="x2416ActionBy" Visible="False" />
                            <asp:BoundField DataField="x2416EditActionNotes" HeaderText="x2416EditActionNotes" SortExpression="x2416EditActionNotes" Visible="False" />
                            <asp:BoundField DataField="x2416ViewAll" HeaderText="x2416ViewAll" SortExpression="x2416ViewAll" Visible="False" />
                            <asp:BoundField DataField="x2413ViewAll" HeaderText="x2413ViewAll" SortExpression="x2413ViewAll" Visible="False" />
                            <asp:BoundField DataField="x2414ViewAll" HeaderText="x2414ViewAll" SortExpression="x2414ViewAll" Visible="False" />
                            <asp:BoundField DataField="x2417" HeaderText="x2417" SortExpression="x2417" Visible="False" />
                            <asp:BoundField DataField="x2417Add" HeaderText="x2417Add" SortExpression="x2417Add" Visible="False" />
                            <asp:BoundField DataField="x2417Edit" HeaderText="x2417Edit" SortExpression="x2417Edit" Visible="False" />
                            <asp:BoundField DataField="x2417Print" HeaderText="x2417Print" SortExpression="x2417Print" Visible="False" />
                            <asp:BoundField DataField="x2417ChSt" HeaderText="x2417ChSt" SortExpression="x2417ChSt" Visible="False" />
                            <asp:BoundField DataField="x2412ServiceEButton" HeaderText="x2412ServiceEButton" SortExpression="x2412ServiceEButton" Visible="False" />
                           
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
           <tr style="text-align:center;width:100%">
                <td style="text-align:center;padding-left:20px;width:100%">
<asp:TabContainer ID="tb1" runat="server" ActiveTabIndex="7" Font-Names="Calibri" Height="100%" Width="100%" CssClass="ajax__tab_xp" OnClientActiveTabChanged="TabChanged" AutoPostBack="True">
    <asp:TabPanel runat="server" Width="100%" HeaderText=" General & Billing Info" ID="TabPanel1">
        <HeaderTemplate>Group Authority Information</HeaderTemplate>
        <ContentTemplate>
            <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                <tr><td style="width:40%;text-align:left;">
                    <asp:Button ID="btnADD" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" /><asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="true" /><asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm()"/><asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="False" /></td><td style="text-align: right"></td></tr><tr><td><br /></td></tr><tr><td class="CellFormatADM">GroupAccess<asp:Label ID="Label23" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td><td class="CellTextBoxADM"><asp:TextBox ID="txtGroupAuthority" runat="server" MaxLength="25" Height="16px" Width="60%"></asp:TextBox></td></tr><tr><td class="CellFormatADM">Description</td><td class="CellTextBoxADM"><asp:TextBox ID="txtComments" runat="server" MaxLength="100" Height="16px" Width="90%"></asp:TextBox></td></tr><%--<tr><td class="CellFormatADM">Access</td><td class="CellTextBoxADM"><asp:CheckBox ID="chkAccess" runat="server" /></td></tr>--%><tr><td colspan="2" style="text-align:right"><asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" /><asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table>

        </ContentTemplate>

    </asp:TabPanel>
     <asp:TabPanel runat="server" Width="100%" HeaderText="Staff Information" ID="TabPanel2" TabIndex="1">
         <HeaderTemplate>Staff Information</HeaderTemplate>
         <ContentTemplate>
         <table class="Centered" style="width:80%;text-align:center;padding-left:10px;padding-top:10px;">
             <tr><td class="CellFormat">GroupAccess</td><td colspan="1" class="CellTextBox">
                 <asp:Label ID="lblGroupAuthority" runat="server" MaxLength="50" Height="16px" Width="99%" BackColor="#CCCCCC"></asp:Label></td></tr></table>
         <br />
                  
         <asp:GridView ID="GridView2" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="StaffId" DataSourceID="SqlDataSource2">
             <Columns>
                 <asp:BoundField DataField="Active" HeaderText="Active" />
                 <asp:BoundField DataField="StaffId" HeaderText="StaffId" ReadOnly="True" SortExpression="StaffId" >
             <ItemStyle HorizontalAlign="Left" />
             </asp:BoundField>
             <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" >
             <ItemStyle HorizontalAlign="Left" />
             </asp:BoundField>
             <asp:BoundField DataField="Nric" HeaderText="Nric" SortExpression="Nric" /><asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" /><asp:BoundField DataField="DateJoin" HeaderText="DateJoin" SortExpression="DateJoin" DataFormatString="{0:dd/MM/yyyy}" /><asp:BoundField DataField="DateLeft" HeaderText="DateLeft" SortExpression="DateLeft" DataFormatString="{0:dd/MM/yyyy}" /><asp:BoundField DataField="Appointment" HeaderText="Appointment" SortExpression="Appointment" /><asp:BoundField DataField="PayrollID" HeaderText="PayrollID" SortExpression="PayrollID" /></Columns><FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" /><RowStyle BackColor="#E4E4E4" ForeColor="#8C4510" Font-Names="Calibri" /><SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#E4E4E4" /><SortedAscendingHeaderStyle BackColor="#000066" />
             <SortedDescendingCellStyle BackColor="#E4E4E4" /><SortedDescendingHeaderStyle BackColor="#000066" /></asp:GridView></ContentTemplate></asp:TabPanel>

       <asp:TabPanel runat="server" Width="100%" HeaderText="Setup Access Details" ID="TabPanel5" TabIndex="2">
           <HeaderTemplate>Setup Access Details</HeaderTemplate><ContentTemplate><br />
               <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                   <tr><td style="width:40%;text-align:left;">
                       <asp:Button ID="btnEditSetupAccess" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" /></td></tr><tr><td><br /></td></tr><tr><td><table class="Centered" style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat">GroupAccess</td><td colspan="1" class="CellTextBox"><asp:Label ID="lblGroupAuthority3" runat="server" MaxLength="50" Height="16px" Width="80%" BackColor="#CCCCCC"></asp:Label></td></tr><tr><td><br /></td></tr><tr><td class="CellFormat"><asp:CheckBox ID="chkSetupSelectAll" runat="server" Font-Bold="True" Text="Select All" /></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">UserStaff</td><td class="CellTextBox">
           <asp:CheckBoxList ID="chkUserStaffList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%">
               <asp:ListItem>Access</asp:ListItem>
               <asp:ListItem>Add</asp:ListItem>
               <asp:ListItem>Edit</asp:ListItem>
               <asp:ListItem>Delete</asp:ListItem>
               <asp:ListItem>ChangeStatus</asp:ListItem>
               <asp:ListItem Value ="GroupAccess" Text="User Credentials"> </asp:ListItem>
               <asp:ListItem>Print</asp:ListItem>

           </asp:CheckBoxList></td></tr></table></td></tr></table><br />
       
            <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"> 
              <tr><td style="text-align:left;width:100%;">
                <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                  <%--  <tr><td style="text-align:left;width:100%;">
                    <table border="0" style="width:62%;text-align:center;padding-left:10px;padding-top:10px;">--%>
                        <tr><td class="CellFormat" style="padding-right:50px;">CompanySetup</td>
                            <td class="CellTextBox"><asp:CheckBoxList ID="chkCompanySetup" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem>Edit</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br />
          
           <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"> <tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">CompanyGroup</td><td class="CellTextBox"><asp:CheckBoxList ID="chkCompanyGroupList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br />
           
           <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
               <tr><td style="text-align:left;width:100%;">
               <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">    <tr><td class="CellFormat" style="padding-right:50px;">City</td><td class="CellTextBox"><asp:CheckBoxList ID="chkCityList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br />
               <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                      <tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                   <tr><td class="CellFormat" style="padding-right:50px;">State</td><td class="CellTextBox"><asp:CheckBoxList ID="chkStateList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br />
                <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;">
               <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Country</td><td class="CellTextBox"><asp:CheckBoxList ID="chkCountryList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br />
               <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">ScheduleType</td><td class="CellTextBox"><asp:CheckBoxList ID="chkSchTypeList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Target</td><td class="CellTextBox"><asp:CheckBoxList ID="chkTargetList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">ServiceFrequency</td><td class="CellTextBox"><asp:CheckBoxList ID="chkSvcFreqList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">InvoiceFrequency</td><td class="CellTextBox"><asp:CheckBoxList ID="chkInvFreqList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Vehicle</td><td class="CellTextBox"><asp:CheckBoxList ID="chkVehicleList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Team</td><td class="CellTextBox"><asp:CheckBoxList ID="chkTeamList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">LocationGroup</td><td class="CellTextBox"><asp:CheckBoxList ID="chkLocGroupList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">PostalToLocation</td><td class="CellTextBox"><asp:CheckBoxList ID="chkPostalList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">TerminationCode</td><td class="CellTextBox"><asp:CheckBoxList ID="chkTermCodeList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">UserAccess</td><td class="CellTextBox"><asp:CheckBoxList ID="chkUserAccessList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">ContractGroupCategory</td><td class="CellTextBox"><asp:CheckBoxList ID="chkContractGroupCategoryList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">ContractGroup</td><td class="CellTextBox"><asp:CheckBoxList ID="chkDepartmentList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">ServiceMaster</td><td class="CellTextBox"><asp:CheckBoxList ID="chkServiceMasterList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Industry</td><td class="CellTextBox"><asp:CheckBoxList ID="chkIndustryList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">EmailSetup</td><td class="CellTextBox"><asp:CheckBoxList ID="chkEmailSetupList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Holiday</td><td class="CellTextBox"><asp:CheckBoxList ID="chkHolidayList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">UOM</td><td class="CellTextBox"><asp:CheckBoxList ID="chkUOMList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">ChartOfAccounts</td><td class="CellTextBox"><asp:CheckBoxList ID="chkCOAList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">TaxRate</td><td class="CellTextBox"><asp:CheckBoxList ID="chkTaxRateList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">BillingCode</td><td class="CellTextBox"><asp:CheckBoxList ID="chkBillingCodeList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">MassChange</td><td class="CellTextBox"><asp:CheckBoxList ID="chkMassChangeList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem>  </asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">EventLog</td><td class="CellTextBox"><asp:CheckBoxList ID="chkEventLogList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Mobile Setup</td><td class="CellTextBox"><asp:CheckBoxList ID="chkMobileList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Currency</td><td class="CellTextBox"><asp:CheckBoxList ID="chkCurrencyList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Terms</td><td class="CellTextBox"><asp:CheckBoxList ID="chkTermsList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Market Segment</td><td class="CellTextBox"><asp:CheckBoxList ID="chkMarketSegmentList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Service Type</td><td class="CellTextBox"><asp:CheckBoxList ID="chkServiceTypeList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br />
        

           <table class="Centered" style="border: 1px solid #808080; display:none; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px; display:none"><tr><td class="CellFormat"  style="padding-right:50px; display:none;">Service Module</td><td class="CellTextBox"><asp:CheckBoxList ID="chkServiceModuleList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br />
        
           
              <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">ContactsModuleSetup</td><td class="CellTextBox"><asp:CheckBoxList ID="chkContactsModuleSetup" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="55%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Edit">Edit</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">LockServiceRecord</td><td class="CellTextBox"><asp:CheckBoxList ID="chkLockServiceRecord" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Chemicals</td><td class="CellTextBox"><asp:CheckBoxList ID="chkChemicalsList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Bank</td><td class="CellTextBox"><asp:CheckBoxList ID="chkBankList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Notes Template</td><td class="CellTextBox"><asp:CheckBoxList ID="chkNotesTemplateList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Settlement Type</td><td class="CellTextBox"><asp:CheckBoxList ID="chkSettlementTypeList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Service Action</td><td class="CellTextBox"><asp:CheckBoxList ID="chkServiceActionList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br />
           <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Period</td><td class="CellTextBox"><asp:CheckBoxList ID="chkPeriodList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br />
           <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Location</td><td class="CellTextBox"><asp:CheckBoxList ID="chkLocationList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br />
           <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Batch Email</td><td class="CellTextBox"><asp:CheckBoxList ID="ChkBatchEmailList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Send Email</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br />

          <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Setup-OPS Module</td><td class="CellTextBox"><asp:CheckBoxList ID="chkOpsModuleList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add" Enabled="False">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem Enabled="False">Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br />
          <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Setup-AR Module</td><td class="CellTextBox"><asp:CheckBoxList ID="chkARModuleList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add" Enabled="False">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem Enabled="False">Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br />
          <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">SMS Setup</td><td class="CellTextBox"><asp:CheckBoxList ID="chkSMSSetupList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add" >Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem >Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br />
          <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Login Log</td><td class="CellTextBox"><asp:CheckBoxList ID="chkLoginLog" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add" >Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem >Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br />
          <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Document Type</td><td class="CellTextBox"><asp:CheckBoxList ID="chkDocumentType" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add" >Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem >Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br />
      
          <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Customer Portal</td><td class="CellTextBox"><asp:CheckBoxList ID="chkCustmerPortal" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add" >Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem >Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br />
        
             <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                 <tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Stock Item</td><td class="CellTextBox"><asp:CheckBoxList ID="chkStockItem" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add" >Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem >Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table><br />
             
           
              <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                 <tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Device Type</td><td class="CellTextBox"><asp:CheckBoxList ID="chkDeviceType" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add" >Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem >Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table>
           
           <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                 <tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Device Event Threshold</td><td class="CellTextBox"><asp:CheckBoxList ID="chkDeviceEventThreshold" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add" >Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem >Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table>
           
        
              <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                 <tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Pest Master</td><td class="CellTextBox"><asp:CheckBoxList ID="chkPestMaster" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add" >Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem >Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table>
        
           
              <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                 <tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Level of Infestation</td><td class="CellTextBox"><asp:CheckBoxList ID="chkLevelofInfestatation" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add" >Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem >Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table>
        
           
              <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                 <tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Gender </td><td class="CellTextBox"><asp:CheckBoxList ID="chkPestGender" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add" >Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem >Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table>
        
           
              <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                 <tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Life Stage</td><td class="CellTextBox"><asp:CheckBoxList ID="chkPestLifeStage" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add" >Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem >Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table>
        
           
              <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                 <tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Species  </td><td class="CellTextBox"><asp:CheckBoxList ID="chkPestSpecies" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add" >Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem >Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table>


              <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                 <tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Trap Type</td><td class="CellTextBox"><asp:CheckBoxList ID="chkPestTrapType" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add" >Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem >Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table>
        
           
              <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                 <tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Hold Code Type</td><td class="CellTextBox"><asp:CheckBoxList ID="chkHoldCodeList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add" >Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem >Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table>
        
              <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                 <tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Batch Contract Price Change</td><td class="CellTextBox"><asp:CheckBoxList ID="chkBatchContractPriceChange" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table>
        
              <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                 <tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Log Details </td><td class="CellTextBox"><asp:CheckBoxList ID="chkSetupLogDetails" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table>
        

                <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                 <tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Contract Code</td><td class="CellTextBox"><asp:CheckBoxList ID="chkContractCodeList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add" >Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem >Delete</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table>
        
               
                   <tr>
                       <td colspan="1" style="text-align:right">
                           <br />
                           <asp:Button ID="btnSaveSetupAccess" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClick="btnSaveSetupAccess_Click" OnClientClick="currentdatetime()" Text="SAVE" Width="100px" />
                           <asp:Button ID="btnCancelSetupAccess" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="100px" />
                       </td>
                   </tr>
        
               
              </td></tr></table></ContentTemplate></asp:TabPanel>

       <asp:TabPanel runat="server" Width="100%" HeaderText=" Contact Access Details" ID="TabPanel4" TabIndex="3"><HeaderTemplate>Contact Access Details</HeaderTemplate><ContentTemplate><br /><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td style="width:40%;text-align:left;"><asp:Button ID="btnEditContactAccess" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" /></td></tr><tr><td><br /></td></tr><tr><td><table class="Centered" style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat">GroupAccess</td><td colspan="1" class="CellTextBox"><asp:Label ID="lblGroupAccess" runat="server" MaxLength="50" Height="16px" Width="80%" BackColor="#CCCCCC"></asp:Label></td></tr><tr><td><br /></td></tr><tr><td class="CellFormat">
           <asp:CheckBox ID="chkContactSelectAll" runat="server" Font-Bold="True" Text="Select All" /></td></tr></table><br />
           
           <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
               <tr><td style="text-align:left;width:100%;">
                   <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                       <tr><td class="CellFormat" style="padding-right:50px;">Company</td><td class="CellTextBox"><asp:CheckBoxList ID="chkCompanyList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%">
                           <asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem>
                           <asp:ListItem>EditAccount</asp:ListItem><asp:ListItem>Delete</asp:ListItem>
                           <asp:ListItem>ChangeStatus</asp:ListItem><asp:ListItem>Trans</asp:ListItem>
                           <asp:ListItem>Notes</asp:ListItem>
                           <asp:ListItem>ViewAll</asp:ListItem>
                           <asp:ListItem>EditBilling</asp:ListItem> 
                           <asp:ListItem>SpecificLocation</asp:ListItem>
                           <asp:ListItem>EditContractGroup</asp:ListItem> 
                            <asp:ListItem>ChangeAccount</asp:ListItem> 
                             <asp:ListItem>UpdateServiceContact</asp:ListItem> 
                       </asp:CheckBoxList></td></tr></table></td></tr></table><br />
           
           <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
               <tr><td style="text-align:left;width:100%;">
                   <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                       <tr><td class="CellFormat" style="padding-right:50px;">Person</td><td class="CellTextBox">
                           <asp:CheckBoxList ID="chkPersonList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%">
                               <asp:ListItem>Access</asp:ListItem>
                               <asp:ListItem Value="Add">Add</asp:ListItem>
                               <asp:ListItem>Edit</asp:ListItem>
                               <asp:ListItem>Delete</asp:ListItem>
                               <asp:ListItem>Trans</asp:ListItem>
                               <asp:ListItem>Notes</asp:ListItem>
                               <asp:ListItem>EditBilling</asp:ListItem>
                               <asp:ListItem>SpecificLocation</asp:ListItem>
                               <asp:ListItem>EditContractGroup</asp:ListItem>
                                 <asp:ListItem>ChangeAccount</asp:ListItem> 
                                     <asp:ListItem>UpdateServiceContact</asp:ListItem> 
                           </asp:CheckBoxList></td></tr></table></td></tr></table><br /></td></tr><tr><td colspan="1" style="text-align:right"><br /><asp:Button ID="btnSaveContactAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" /><asp:Button ID="btnCancelContactAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table></ContentTemplate></asp:TabPanel>
  
       <asp:TabPanel runat="server" Width="100%" HeaderText=" Tools Access Details" ID="TabPanel9" TabIndex="4"><HeaderTemplate>Tools Access Details</HeaderTemplate><ContentTemplate><br />
           
           <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td style="width:40%;text-align:left;">
               <asp:Button ID="btnEditToolsAccess" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" /></td></tr>
               <tr><td><br /></td></tr>
               <tr><td><table class="Centered" style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                   <tr><td class="CellFormat">GroupAccess</td><td colspan="1" class="CellTextBox"><asp:Label ID="lblGroupAuthority7" runat="server" MaxLength="50" Height="16px" Width="80%" BackColor="#CCCCCC"></asp:Label></td></tr><tr><td><br /></td></tr>
           
           <tr style="display:none"><td class="CellFormat"><asp:CheckBox ID="chkToolsSelectAll" runat="server" Font-Bold="True" Text="Select All" /></td></tr></table><br />
           
           <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
               
               <tr>
                   
                   <td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">
           Address Verification</td><td class="CellTextBox">
               
               <asp:CheckBoxList ID="chkToolsList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem> </asp:CheckBoxList></td></tr></table></td>

               </tr>

           </table><br />
           
           <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
               
                 <tr>
                   
                   <td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">
           Floor Plan</td><td class="CellTextBox">
               
               <asp:CheckBoxList ID="chkFloorPlanList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem> </asp:CheckBoxList></td></tr></table></td>

               </tr>

           </table><br />

                    <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
               
               <tr>
                   
                   <td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">
           Excel Data Import</td><td class="CellTextBox">
               
               <asp:CheckBoxList ID="chkExcelImportList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem> </asp:CheckBoxList></td></tr></table></td>

               </tr>

           </table><br />
                   </td></tr>
           
           <tr><td colspan="1" style="text-align:right"><br /><asp:Button ID="btnSaveToolsAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" /><asp:Button ID="btnCancelToolsAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table></ContentTemplate></asp:TabPanel>
       <asp:TabPanel runat="server" Width="100%" HeaderText=" Asset Access Details" ID="TabPanel10" TabIndex="5">
           <HeaderTemplate>Asset Access Details</HeaderTemplate>
           <ContentTemplate><br />
               <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                   <tr><td style="width:40%;text-align:left;">
                       <asp:Button ID="btnEditAssetAccess" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" /></td></tr>
                   <tr><td><br /></td></tr><tr><td>
                       <table class="Centered" style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                           <tr><td class="CellFormat">GroupAccess</td>
                               <td colspan="1" class="CellTextBox">
                                   <asp:Label ID="lblGroupAuthorityAsset" runat="server" MaxLength="50" Height="16px" Width="80%" BackColor="#CCCCCC"></asp:Label></td></tr>
                           <tr><td><br /></td></tr><tr><td class="CellFormat">
           <asp:CheckBox ID="chkAssetSelectAll" runat="server" Font-Bold="True" Text="Select All" /></td></tr></table><br />
           
           <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
               <tr><td style="text-align:left;width:100%;">
                   <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                       <tr><td class="CellFormat" style="padding-right:50px;">Asset</td><td class="CellTextBox">
                           <asp:CheckBoxList ID="chkAssetList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%">
                           <asp:ListItem>Access</asp:ListItem>
                           <asp:ListItem Value="Add">Add</asp:ListItem>
                           <asp:ListItem>Edit</asp:ListItem>
                           <asp:ListItem>Delete</asp:ListItem>
                              <asp:ListItem>Print</asp:ListItem>                        
                           <asp:ListItem>Movement</asp:ListItem>
                         
                       </asp:CheckBoxList></td></tr></table></td></tr></table><br />

            <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
               <tr><td style="text-align:left;width:100%;">
                   <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                       <tr><td class="CellFormat" style="padding-right:50px;">Supplier</td><td class="CellTextBox">
                           <asp:CheckBoxList ID="chkAssetSupplierList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%">
                           <asp:ListItem>Access</asp:ListItem>
                           <asp:ListItem Value="Add">Add</asp:ListItem>
                           <asp:ListItem>Edit</asp:ListItem>
                           <asp:ListItem>Delete</asp:ListItem>
                                                 
                       </asp:CheckBoxList></td></tr></table></td></tr></table><br />

                        <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
               <tr><td style="text-align:left;width:100%;">
                   <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                       <tr><td class="CellFormat" style="padding-right:50px;">Brand</td><td class="CellTextBox">
                           <asp:CheckBoxList ID="chkAssetBrandList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%">
                           <asp:ListItem>Access</asp:ListItem>
                           <asp:ListItem Value="Add">Add</asp:ListItem>
                           <asp:ListItem>Edit</asp:ListItem>
                           <asp:ListItem>Delete</asp:ListItem>
                                                      
                       </asp:CheckBoxList></td></tr></table></td></tr></table><br />

                        <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
               <tr><td style="text-align:left;width:100%;">
                   <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                       <tr><td class="CellFormat" style="padding-right:50px;">AssetClass</td><td class="CellTextBox">
                           <asp:CheckBoxList ID="chkAssetClassList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%">
                           <asp:ListItem>Access</asp:ListItem>
                           <asp:ListItem Value="Add">Add</asp:ListItem>
                           <asp:ListItem>Edit</asp:ListItem>
                           <asp:ListItem>Delete</asp:ListItem>
                                                      
                       </asp:CheckBoxList></td></tr></table></td></tr></table><br />

                        <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
               <tr><td style="text-align:left;width:100%;">
                   <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                       <tr><td class="CellFormat" style="padding-right:50px;">AssetColor</td><td class="CellTextBox">
                           <asp:CheckBoxList ID="chkAssetColorList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%">
                           <asp:ListItem>Access</asp:ListItem>
                           <asp:ListItem Value="Add">Add</asp:ListItem>
                           <asp:ListItem>Edit</asp:ListItem>
                           <asp:ListItem>Delete</asp:ListItem>
                                                      
                       </asp:CheckBoxList></td></tr></table></td></tr></table><br />

                        <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
               <tr><td style="text-align:left;width:100%;">
                   <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                       <tr><td class="CellFormat" style="padding-right:50px;">AssetGroup</td><td class="CellTextBox">
                           <asp:CheckBoxList ID="chkAssetGroupList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%">
                           <asp:ListItem>Access</asp:ListItem>
                           <asp:ListItem Value="Add">Add</asp:ListItem>
                           <asp:ListItem>Edit</asp:ListItem>
                           <asp:ListItem>Delete</asp:ListItem>
                                                      
                       </asp:CheckBoxList></td></tr></table></td></tr></table><br />

                        <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
               <tr><td style="text-align:left;width:100%;">
                   <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                       <tr><td class="CellFormat" style="padding-right:50px;">AssetModel</td><td class="CellTextBox">
                           <asp:CheckBoxList ID="chkAssetModelList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%">
                           <asp:ListItem>Access</asp:ListItem>
                           <asp:ListItem Value="Add">Add</asp:ListItem>
                           <asp:ListItem>Edit</asp:ListItem>
                           <asp:ListItem>Delete</asp:ListItem>
                                                      
                       </asp:CheckBoxList></td></tr></table></td></tr></table><br />

                        <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
               <tr><td style="text-align:left;width:100%;">
                   <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                       <tr><td class="CellFormat" style="padding-right:50px;">AssetStatus</td><td class="CellTextBox">
                           <asp:CheckBoxList ID="chkAssetStatusList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%">
                           <asp:ListItem>Access</asp:ListItem>
                           <asp:ListItem Value="Add">Add</asp:ListItem>
                           <asp:ListItem>Edit</asp:ListItem>
                           <asp:ListItem>Delete</asp:ListItem>
                                                      
                       </asp:CheckBoxList></td></tr></table></td></tr></table><br />

                        <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
               <tr><td style="text-align:left;width:100%;">
                   <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                       <tr><td class="CellFormat" style="padding-right:50px;">AssetMovementType</td><td class="CellTextBox">
                           <asp:CheckBoxList ID="chkAssetMovementTypeList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%">
                           <asp:ListItem>Access</asp:ListItem>
                           <asp:ListItem Value="Add">Add</asp:ListItem>
                           <asp:ListItem>Edit</asp:ListItem>
                           <asp:ListItem>Delete</asp:ListItem>
                                                      
                       </asp:CheckBoxList></td></tr></table></td></tr></table><br />


                 <tr><td colspan="1" style="text-align:right"><br />
                     <asp:Button ID="btnSaveAssetAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" />
                     <asp:Button ID="btnCancelAssetAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table>

           </ContentTemplate>

       </asp:TabPanel>
  


     <asp:TabPanel runat="server" Width="100%" HeaderText="Service Access Details" ID="TabPanel3" Height="100%" TabIndex="6"><HeaderTemplate>Contract & Service Access Details</HeaderTemplate><ContentTemplate><br /><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td style="width:40%;text-align:left;"><asp:Button ID="btnEditSvcAccess" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" /></td></tr><tr><td><br /></td></tr><tr><td><table class="Centered" style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
         <tr><td class="CellFormat">GroupAccess</td
             ><td colspan="1" class="CellTextBox">
                 <asp:Label ID="lblGroupAuthority1" runat="server" MaxLength="50" Height="16px" Width="80%" BackColor="#CCCCCC"></asp:Label></td></tr>
         <tr><td><br /></td></tr>
         <tr><td class="CellFormat">
             <asp:CheckBox ID="chkSvcSelectAll" runat="server" Font-Bold="True" Text="Select All" /></td></tr></table><br />
         
         <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
             <tr><td style="text-align:left;width:100%;">
                 <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                     <tr><td class="CellFormat" style="padding-right:50px;">Service</td><td class="CellTextBox">
                         <asp:CheckBoxList ID="chkServiceList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem Value="Copy">Copy</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem><asp:ListItem>Print</asp:ListItem><asp:ListItem>ChangeStatus</asp:ListItem><asp:ListItem>Update</asp:ListItem><asp:ListItem>Reverse</asp:ListItem><asp:ListItem>ViewAll</asp:ListItem><asp:ListItem>AssignServiceRecord</asp:ListItem><asp:ListItem>ReCalculate</asp:ListItem>
             <asp:ListItem>ExportToExcel</asp:ListItem>
              <asp:ListItem>FileAccess</asp:ListItem>
                  <asp:ListItem>FileUpload</asp:ListItem>
                  <asp:ListItem>FileDelete</asp:ListItem>
                  <asp:ListItem>PestCount</asp:ListItem>
                <asp:ListItem>VoidService</asp:ListItem>
                <asp:ListItem>ManualContratPOWONo</asp:ListItem>
                               <asp:ListItem>EditBilledAmtBillNo</asp:ListItem>
             </asp:CheckBoxList>
                                                                                                                                                                                                                                                                                                                                                                          </td></tr></table></td></tr></table><br />
         
         <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Contract</td>
             
             <td class="CellTextBox">
                
                 <asp:CheckBoxList ID="chkContractList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%">
                     <asp:ListItem>Access</asp:ListItem>
                 <asp:ListItem>Add</asp:ListItem>
                 <asp:ListItem>Copy</asp:ListItem>
                 <asp:ListItem>Edit</asp:ListItem>
                 <asp:ListItem>Delete</asp:ListItem><asp:ListItem>Print</asp:ListItem>
                 <asp:ListItem>ChangeStatus</asp:ListItem><asp:ListItem>Update</asp:ListItem>
                 <asp:ListItem>Reverse</asp:ListItem><asp:ListItem>Process</asp:ListItem>
                 <asp:ListItem>EarlyComplete</asp:ListItem><asp:ListItem>TerminationByCust</asp:ListItem>
                 <asp:ListItem>CancelByCust</asp:ListItem><asp:ListItem>RenewalStatus</asp:ListItem>
                 <asp:ListItem>EditAgreeValue</asp:ListItem><asp:ListItem>EditPortfolioValue</asp:ListItem>
                 <asp:ListItem>EditOurRef</asp:ListItem>
                     <asp:ListItem>EditManualContractNo</asp:ListItem>
                 <asp:ListItem>EditPONo</asp:ListItem>
                  <asp:ListItem>EditNotes</asp:ListItem>
                  <asp:ListItem>SOR</asp:ListItem>
                  <asp:ListItem>SORAdd</asp:ListItem>
                  <asp:ListItem>SOREdit</asp:ListItem>
                  <asp:ListItem>SORDelete</asp:ListItem>
                                     
                   <asp:ListItem>AddBackdateContractSameMonthOnly</asp:ListItem>
                    <asp:ListItem>AddBackdateContractIndefinite</asp:ListItem>

               
                  <asp:ListItem>TerminationSameMonthOnly</asp:ListItem>
                 <asp:ListItem>TerminationBackdate</asp:ListItem>
                  <asp:ListItem>TerminationFutureDate</asp:ListItem>

                  <asp:ListItem>FileAccess</asp:ListItem>
                  <asp:ListItem>FileUpload</asp:ListItem>
                  <asp:ListItem>FileDelete</asp:ListItem>
                  <asp:ListItem>EditAutoRenewal</asp:ListItem>
                  <asp:ListItem>EditBillingFrequency</asp:ListItem>
                <asp:ListItem>RegenerateSchedule</asp:ListItem>
                     <asp:ListItem>ExtendEndDate</asp:ListItem>
                      <asp:ListItem>Distribution</asp:ListItem>
                     <asp:ListItem>VoidContract</asp:ListItem>
                      <asp:ListItem>WarrantyDates</asp:ListItem>
                          <asp:ListItem>Revision</asp:ListItem>
        <asp:ListItem Value ="AgreementTypeContractCode" Text="Agreement Type & Contract Code"></asp:ListItem>

                    
                    </asp:CheckBoxList></td></tr></table></td></tr></table>

        

                           </td></tr>
         
         <tr><td colspan="1" style="text-align:right"><asp:Button ID="btnSaveSvcAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" /><asp:Button ID="btnCancelSvcAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table>
                 </ContentTemplate>

     </asp:TabPanel>
  
      <asp:TabPanel runat="server" Width="100%" HeaderText="AR Access Details" ID="TabPanel6" Height="100%" TabIndex="7"><HeaderTemplate>AR Access Details</HeaderTemplate><ContentTemplate><br />
          
          <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td style="width:40%;text-align:left;"><asp:Button ID="btnEditARAccess" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" /></td></tr><tr><td><br /></td></tr><tr><td><table class="Centered" style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat">GroupAccess</td><td colspan="1" class="CellTextBox"><asp:Label ID="lblGroupAuthority4" runat="server" MaxLength="50" Height="16px" Width="80%" BackColor="#CCCCCC"></asp:Label></td></tr><tr><td><br /></td></tr><tr><td class="CellFormat"><asp:CheckBox ID="chkARSelectAll" runat="server" Font-Bold="True" Text="Select All" /></td></tr></table><br />
         
              
            <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;">
            <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Sales Order</td><td class="CellTextBox"><asp:CheckBoxList ID="chkSalesOrderList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem><asp:ListItem>Print</asp:ListItem><asp:ListItem>Post</asp:ListItem><asp:ListItem>Reverse</asp:ListItem><asp:ListItem>Multi-Print</asp:ListItem><asp:ListItem Value="EditInvEditAccountName">Edit Account Name</asp:ListItem><asp:ListItem Value="EditInvEditBillingDetail">Edit Contact Person & Billing Details</asp:ListItem><asp:ListItem Value="EditInvEditOurRef">Edit Our Reference</asp:ListItem><asp:ListItem Value="EditInvEditSalesman">Edit Salesman</asp:ListItem>  <asp:ListItem Value="EditInvEditRemarks">Edit Remarks</asp:ListItem> <asp:ListItem Value="PrintInvoice">Print Sales Order </asp:ListItem> <asp:ListItem Value="ExportSalesOrder">Export Sales Order </asp:ListItem> <asp:ListItem Value="ViewSalesOrder">View Sales Order </asp:ListItem> <asp:ListItem Value="EmailSalesOrder">Email SalesOrder</asp:ListItem> </asp:CheckBoxList></td></tr></table></td></tr></table><br />  
       

                   
              <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;">
          
          <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Invoice</td><td class="CellTextBox">
              <asp:CheckBoxList ID="chkInvoiceList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%">
                  <asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem>
                  <asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem><asp:ListItem>Print</asp:ListItem>
                  <asp:ListItem>Post</asp:ListItem><asp:ListItem>Reverse</asp:ListItem> 
                  <asp:ListItem Value="SubmitEInvoice">Submit EInvoice</asp:ListItem>
                  <asp:ListItem Value="CancelEInvoice">Cancel EInvoice</asp:ListItem>
                  <asp:ListItem>Quick Receipt</asp:ListItem>
                  <asp:ListItem>Multi-Print</asp:ListItem><asp:ListItem Value="EditInvEditAccountName">Edit Account Name</asp:ListItem>
                  <asp:ListItem Value="EditInvEditBillingDetail">Edit Contact Person & Billing Details</asp:ListItem>
                  <asp:ListItem Value="EditInvEditOurRef">Edit Our Reference</asp:ListItem>
                  <asp:ListItem Value="EditInvEditSalesman">Edit Salesman</asp:ListItem>  
                  <asp:ListItem Value="EditInvEditRemarks">Edit Remarks</asp:ListItem> 
                  <asp:ListItem Value="FileUpload">File Upload </asp:ListItem>
                  <asp:ListItem Value="PrintInvoice">Print Invoice </asp:ListItem> 
                  <asp:ListItem Value="ExportInvoice">Export Invoice </asp:ListItem> 
                  <asp:ListItem Value="ViewInvoice">View Invoice </asp:ListItem> 
                  <asp:ListItem Value="EmailInvoice">Email Invoice</asp:ListItem> 
                  <asp:ListItem Value="DeSelectContractGroup">DeSelect Contrat Group</asp:ListItem> 
                   <asp:ListItem Value="InvoiceFormat1">View InvoiceFormat1</asp:ListItem> 
                   <asp:ListItem Value="InvoiceFormat2">View InvoiceFormat2</asp:ListItem> 
                    <asp:ListItem Value="InvoiceFormat3">View InvoiceFormat3</asp:ListItem> 
                    <asp:ListItem Value="InvoiceFormat4">View InvoiceFormat4</asp:ListItem> 
                    <asp:ListItem Value="InvoiceFormat5">View InvoiceFormat5</asp:ListItem> 

                   <asp:ListItem Value="InvoiceFormat6">View InvoiceFormat6</asp:ListItem> 
                   <asp:ListItem Value="InvoiceFormat7">View InvoiceFormat7</asp:ListItem> 
                    <asp:ListItem Value="InvoiceFormat8">View InvoiceFormat8</asp:ListItem> 
                    <asp:ListItem Value="InvoiceFormat9">View InvoiceFormat9</asp:ListItem> 
                    <asp:ListItem Value="InvoiceFormat10">View InvoiceFormat10</asp:ListItem> 

                   <asp:ListItem Value="InvoiceFormat11">View InvoiceFormat11</asp:ListItem> 
                   <asp:ListItem Value="InvoiceFormat12">View InvoiceFormat12</asp:ListItem> 
                    <asp:ListItem Value="InvoiceFormat13">View InvoiceFormat13</asp:ListItem> 
                    <asp:ListItem Value="InvoiceFormat14">View InvoiceFormat14</asp:ListItem> 
           

              </asp:CheckBoxList></td></tr></table></td></tr></table><br />  
       
                 <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;">
          
          <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Consolidated Invoice</td><td class="CellTextBox">
              <asp:CheckBoxList ID="chkConsolidatedInvoiceList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%">
                  <asp:ListItem>Access</asp:ListItem>
                  <asp:ListItem Value="Add">Add</asp:ListItem>
                  <asp:ListItem>Edit</asp:ListItem>
                  <asp:ListItem>Print</asp:ListItem>
                  <asp:ListItem>Void</asp:ListItem>
                   <asp:ListItem Value="SubmitInvoice">Submit ConsolidatedInvoice</asp:ListItem>
                  <asp:ListItem Value="CancelInvoice">Cancel ConsolidatedInvoice</asp:ListItem>
         
              </asp:CheckBoxList></td></tr></table></td></tr></table><br />  


                 <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;">
          
          <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Batch Invoice</td><td class="CellTextBox">
              <asp:CheckBoxList ID="chkBatchInvoiceList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%">
                  <asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem>
                  <asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem>
                  <asp:ListItem>Print</asp:ListItem><asp:ListItem>Post</asp:ListItem>
                  <asp:ListItem>Reverse</asp:ListItem>
                  <asp:ListItem>Multi-Print</asp:ListItem>
                  <asp:ListItem Value="DeSelectContractGroup">DeSelect Contrat Group</asp:ListItem> 

              </asp:CheckBoxList></td></tr></table></td></tr></table><br />  


              
         <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;">
              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">CreditNote</td><td class="CellTextBox">
                  <asp:CheckBoxList ID="chkCreditNoteList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%">
                      <asp:ListItem>Access</asp:ListItem>
                      <asp:ListItem Value="Add">Add</asp:ListItem>
                      <asp:ListItem>Edit</asp:ListItem>
                      <asp:ListItem>Delete</asp:ListItem>
                      <asp:ListItem>Print</asp:ListItem>
                      <asp:ListItem>Post</asp:ListItem>
                      <asp:ListItem>Reverse</asp:ListItem>
                       <asp:ListItem Value="SubmitECNDN">Submit ECN/DN</asp:ListItem>
                  <asp:ListItem Value="CancelECNDN">Cancel ECN/DN</asp:ListItem>
                      <asp:ListItem Value="EditCNEditAccountName">Edit Account Name</asp:ListItem>
                      <asp:ListItem Value="EditCNEditBillingDetail">Edit Contact Person & Billing Details</asp:ListItem>
                       <asp:ListItem Value="FileUpload">File Upload </asp:ListItem>
                  </asp:CheckBoxList></td></tr></table></td></tr></table><br />
          <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Receipt</td><td class="CellTextBox">
              <asp:CheckBoxList ID="chkReceiptList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>Access</asp:ListItem><asp:ListItem Value="Add">Add</asp:ListItem><asp:ListItem>Edit</asp:ListItem><asp:ListItem>Delete</asp:ListItem><asp:ListItem>Print</asp:ListItem>
              <asp:ListItem>Post</asp:ListItem>
              <asp:ListItem>Reverse</asp:ListItem>
                <asp:ListItem>ChequeReturn</asp:ListItem>
                   <asp:ListItem Value="FileUpload">File Upload </asp:ListItem>
                </asp:CheckBoxList></td></tr></table></td></tr></table><br />
          <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Adjustment</td><td class="CellTextBox">
              <asp:CheckBoxList ID="chkAdjustmentList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%">
                  <asp:ListItem>Access</asp:ListItem>
                  <asp:ListItem Value="Add">Add</asp:ListItem>
                  <asp:ListItem>Edit</asp:ListItem>
                  <asp:ListItem>Delete</asp:ListItem>
                  <asp:ListItem>Print</asp:ListItem>
                  <asp:ListItem>Post</asp:ListItem>
                  <asp:ListItem>Reverse</asp:ListItem>
                   <asp:ListItem Value="FileUpload">File Upload </asp:ListItem>
              </asp:CheckBoxList></td></tr></table></td></tr></table><br /></td></tr><tr><td colspan="1" style="text-align:right"><asp:Button ID="btnSaveARAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" /><asp:Button ID="btnCancelARAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table>
             </ContentTemplate>

      </asp:TabPanel>

       <asp:TabPanel runat="server" Width="100%" HeaderText=" Report Access Details" ID="TabPanel7" TabIndex="8">
           <HeaderTemplate>Report Access Details</HeaderTemplate>
           <ContentTemplate>
               <br /><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td style="width:40%;text-align:left;"><asp:Button ID="btnEditReportAccess" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" /></td></tr><tr><td><br /></td></tr><tr><td><table class="Centered" style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat">GroupAccess</td><td colspan="1" class="CellTextBox"><asp:Label ID="lblGroupAuthority5" runat="server" MaxLength="50" Height="16px" Width="80%" BackColor="#CCCCCC"></asp:Label></td></tr><tr><td><br /></td></tr><tr><td class="CellFormat"><asp:CheckBox ID="chkReportSelectAll" runat="server" Font-Bold="True" Text="Select All" /></td></tr></table><br /><table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;"><tr><td style="text-align:left;width:100%;"><table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;"><tr><td class="CellFormat" style="padding-right:50px;">Reports</td><td class="CellTextBox"><asp:CheckBoxList ID="chkReportsList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="100%"><asp:ListItem>ServiceRecord</asp:ListItem><asp:ListItem>ServiceContract</asp:ListItem><asp:ListItem>Management</asp:ListItem><asp:ListItem>Revenue</asp:ListItem><asp:ListItem>Portfolio</asp:ListItem><asp:ListItem>JobOrder</asp:ListItem><asp:ListItem>ARReports</asp:ListItem></asp:CheckBoxList></td></tr></table></td></tr></table></td></tr><tr><td colspan="1" style="text-align:right"><br /><asp:Button ID="btnSaveReportAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" /><asp:Button ID="btnCancelReportAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table>

           </ContentTemplate>

       </asp:TabPanel>
 
          <asp:TabPanel runat="server" Width="100%" HeaderText="Location Access Details" Visible="true" ID="TabPanel8" TabIndex="9"><HeaderTemplate>Location Access Details</HeaderTemplate><ContentTemplate><br />
              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                  <tr>
                      <td style="width:20%;text-align:left;"><asp:Button ID="btnAddAccessLocation" runat="server" Font-Bold="True" Text="ADD" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" />
                      <asp:Button ID="btnEditAccessLocation" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" />
                      <asp:Button ID="btnDeleteAccessLocation" runat="server" Font-Bold="True" Text="DELETE" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" OnClientClick = "Confirm()" />
                 </td>
                           </tr>
                  <tr><td><br /></td></tr><tr><td><table class="Centered" style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                  <tr><td class="CellFormat">GroupAccess</td><td colspan="1" class="CellTextBox"><asp:Label ID="lblGroupAuthority6" runat="server" MaxLength="50" Height="18px" Width="80%" BackColor="#CCCCCC"></asp:Label></td></tr>
                  <tr><td><br /></td><td colspan="1" class="CellTextBox">&nbsp;</td></tr>
                  <tr>
                      <td>&nbsp;</td>
                      <td class="CellTextBox" colspan="1">&nbsp;</td>
                  </tr>
                  <tr>
                      <td>&nbsp;</td>
                      <td class="CellTextBox" colspan="1">

                          <asp:GridView ID="GridView3" runat="server" Width="50%" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource3">
                              
                              <Columns>
                                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False">
                <ControlStyle Width="100px" />
                <ItemStyle Width="100px" />
                </asp:CommandField>
                                    <asp:BoundField DataField="rcno">
                                    <ControlStyle CssClass="dummybutton" />
                                    <HeaderStyle CssClass="dummybutton" />
                                    <ItemStyle CssClass="dummybutton" />
                                    </asp:BoundField>
                                  <asp:BoundField DataField="LocationID" HeaderText="Location ID" ReadOnly="True" SortExpression="LocationID" >
                              <ItemStyle HorizontalAlign="Left" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" >
                              <ItemStyle HorizontalAlign="Left" />
                              </asp:BoundField>
                              </Columns>
                              <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" /><RowStyle BackColor="#E4E4E4" ForeColor="#8C4510" Font-Names="Calibri" /><SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#E4E4E4" /><SortedAscendingHeaderStyle BackColor="#000066" /><SortedDescendingCellStyle BackColor="#E4E4E4" /><SortedDescendingHeaderStyle BackColor="#000066" />
                             </asp:GridView>

                      </td>
                  </tr>
                  <tr>
                      <td>&nbsp;</td>
                      <td class="CellTextBox" colspan="1">
                          <asp:TextBox ID="txtRcnoLocationAccess" runat="server" BackColor="#CCCCCC" CssClass="dummybutton" Height="16px" MaxLength="100" Width="45%"></asp:TextBox>
                      </td>
                  </tr>
                  <tr><td class="CellFormat">Location</td><td colspan="1" class="CellTextBox">
                      <asp:DropDownList ID="ddlLocationID" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="chzn-select" Height="25px" TabIndex="25" Width="45.5%">
                          <asp:ListItem Text="--SELECT--" Value="-1" />
                      </asp:DropDownList>
                      </td></tr>
                  <tr>
                      <td class="CellFormat">Location Name</td>
                      <td class="CellTextBox" colspan="1">
                          <asp:TextBox ID="txtLocationDescription" runat="server" Height="16px" MaxLength="100" Width="45%" Enabled="False"></asp:TextBox>
                      </td>
                  </tr>
                  </table><br />
                      
                  </td></tr><tr><td colspan="1" style="text-align:right"><br /><asp:Button ID="btnSaveLocationAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" /><asp:Button ID="btnCancelLocationAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table></ContentTemplate></asp:TabPanel>
      <asp:TabPanel runat="server" Width="100%" HeaderText="AssetGroup Access Details" Visible="true" ID="TabPanel11" TabIndex="10"><HeaderTemplate>AssetGroup Access Details</HeaderTemplate><ContentTemplate><br />
              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                  <tr>
                      <td style="width:20%;text-align:left;"><asp:Button ID="btnAddAssetGroupAccess" runat="server" Font-Bold="True" Text="ADD" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" />
                      <asp:Button ID="btnEditAssetGroupAccess" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" />
                      <asp:Button ID="btnDeleteAssetGroupAccess" runat="server" Font-Bold="True" Text="DELETE" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" OnClientClick = "Confirm()" />
                 </td>
                           </tr>
                  <tr><td><br /></td></tr><tr><td><table class="Centered" style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                  <tr><td class="CellFormat">GroupAccess</td><td colspan="1" class="CellTextBox"><asp:Label ID="lblGroupAuthority8" runat="server" MaxLength="50" Height="18px" Width="80%" BackColor="#CCCCCC"></asp:Label></td></tr>
                  <tr><td><br /></td><td colspan="1" class="CellTextBox">&nbsp;</td></tr>
                  <tr>
                      <td>&nbsp;</td>
                      <td class="CellTextBox" colspan="1">&nbsp;</td>
                  </tr>
                  <tr>
                      <td>&nbsp;</td>
                      <td class="CellTextBox" colspan="1">

                          <asp:GridView ID="grdAssetGroup" runat="server" Width="50%" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource4">
                              
                              <Columns>
                                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False">
                <ControlStyle Width="100px" />
                <ItemStyle Width="100px" />
                </asp:CommandField>
                                    <asp:BoundField DataField="rcno">
                                    <ControlStyle CssClass="dummybutton" />
                                    <HeaderStyle CssClass="dummybutton" />
                                    <ItemStyle CssClass="dummybutton" />
                                    </asp:BoundField>
                                  <asp:BoundField DataField="AssetGroup" HeaderText="AssetGroup" ReadOnly="True" SortExpression="AssetGroup" >
                              <ItemStyle HorizontalAlign="Left" />
                              </asp:BoundField>
                             
                              </Columns>
                              <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" /><RowStyle BackColor="#E4E4E4" ForeColor="#8C4510" Font-Names="Calibri" /><SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#E4E4E4" /><SortedAscendingHeaderStyle BackColor="#000066" /><SortedDescendingCellStyle BackColor="#E4E4E4" /><SortedDescendingHeaderStyle BackColor="#000066" />

                          </asp:GridView>

                      </td>
                  </tr>
                  <tr>
                      <td>&nbsp;</td>
                      <td class="CellTextBox" colspan="1">
                          <asp:TextBox ID="txtRcNoAssetGroupAccess" runat="server" BackColor="#CCCCCC" CssClass="dummybutton" Height="16px" MaxLength="100" Width="45%"></asp:TextBox>
                      </td>
                  </tr>
                      
                  <tr><td class="CellFormat">AssetGroup</td><td colspan="1" class="CellTextBox">
                     <asp:DropDownList ID="ddlAssetGroup" CssClass="chzn-select" runat="server" DataSourceID="SqlDSGroup" DataTextField="AssetGroup" DataValueField="AssetGroup" Width="45.5%" AppendDataBoundItems="True">
                            <asp:ListItem Text="--SELECT--" Value="-1" />   
                            </asp:DropDownList>
                      </td></tr>
               
                  </table><br />
                      
                  </td></tr><tr><td colspan="1" style="text-align:right"><br /><asp:Button ID="btnSaveAssetGroupAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" /><asp:Button ID="btnCancelAssetGroupAccess" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table></ContentTemplate></asp:TabPanel>
   
    
     </asp:TabContainer>
                    
                    </td>
               </tr>
         </table>
          </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblGroupAccess order by GroupAccess"></asp:SqlDataSource>
               <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                  <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDSGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT AssetGroup FROM tblassetgroup ORDER BY AssetGroup"></asp:SqlDataSource>
          <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
         
            <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
     <asp:TextBox ID="txtRcNo2" runat="server" Visible="False"></asp:TextBox>
      
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
           <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
     <asp:TextBox ID="txtModeSvcAccess" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtModeContactAccess" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtModeARAccess" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtModeReportAccess" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="txtModeSetupAccess" runat="server" Visible="FALSE"></asp:TextBox>
                <asp:TextBox ID="txtModeToolsAccess" runat="server" Visible="FALSE"></asp:TextBox>
              <asp:TextBox ID="txtModeAssetAccess" runat="server" Visible="FALSE"></asp:TextBox>
               <asp:TextBox ID="txtModeLocationAccess" runat="server" Visible="FALSE"></asp:TextBox>
<asp:TextBox ID="txtDisplayRecordsLocationwise" runat="server" Visible="FALSE"></asp:TextBox>
              </ContentTemplate>
       <%--  <Triggers>
             <asp:PostBackTrigger ControlID ="btnAddAccessLocation" />
         </Triggers>--%>
         </asp:UpdatePanel>
</asp:Content>

