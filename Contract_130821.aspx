<%@ Page Title="Contract" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" EnableEventValidation="false" CodeFile="Contract_130821.aspx.vb" Inherits="Contract_130821" Culture="en-GB" MaintainScrollPositionOnPostback="true" ValidateRequest="false" %>


<%@ Register Assembly="Ajaxified" Namespace="Ajaxified" TagPrefix="Ajaxified" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <%--  <div class="ldBar" data-stroke="data:ldbar/res,gradient(0,1,#f99,#ff9)"></div>--%>

       <style type="text/css">
    .CellFormat{
        font-size:15px;
        font-weight:bold;
        font-family:Calibri;
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
        width:40%;
        /*table-layout:fixed;
        overflow:hidden;*/
          /*border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;*/
    }
  
      .CellFormat1{
        font-size:15px;
        font-weight:bold;
        font-family:Calibri;
        color:black;
        text-align:left;
        width:11%;
        /*table-layout:fixed;
        overflow:hidden;*/
          border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;
    }

       .CellFormat2{
        font-size:5px;
       
        font-family:Calibri;
        color:black;
        text-align:left;
        width:2px;
       
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
           .auto-style1 {
               width: 15%;
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
              <%--  <div class="ldBar" data-stroke="data:ldbar/res,gradient(0,1,#f99,#ff9)"></div>--%>
            </ProgressTemplate>
        </asp:UpdateProgress>
     <asp:UpdatePanel ID="updPanelContract1" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
    
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
            <ControlBundles>
                <asp:ControlBundle Name="CalendarExtender_Bundle" />
                <asp:controlBundle name="ModalPopupExtender_Bundle"/>  
                <asp:controlBundle name="MaskedEditExtender_Bundle"/>
                <asp:controlBundle name="TabContainer_Bundle"/>     
                  <asp:controlBundle name="ComboBox_Bundle"/>               
            </ControlBundles>
        </asp:ToolkitScriptManager>
     

    <asp:Button ID="dummy" runat="server" Text="" cssclass="dummybutton" />

     <div>
   
     <div style="text-align:center">
         
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">CONTRACT</h3>
          

         <table  style="width:100%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0%; "  >
        <tr >

                <td colspan="13"  style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      
                      <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="13"   style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
            <tr>
                 <td colspan="13"   style="width:100%;text-align:left;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';"> 
                     <asp:Label ID="lblAccountIdContact" runat="server" Visible="false" Text="Account Id: " style="width:40%;text-align:left;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';" ></asp:Label>
                      <asp:Label ID="lblAccountIdContact1" runat="server" Text=""  ></asp:Label>  &nbsp;  &nbsp;  

                     <asp:Label ID="lblAccountNameContact" runat="server" Visible="false" Text="Name: " style="width:40%;text-align:left;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';" ></asp:Label>
                      <asp:Label ID="lblAccountNameContact1" runat="server" Text=""  ></asp:Label>  &nbsp;  &nbsp;   
              <asp:Label ID="lblAccountIdContactLocation" runat="server" Visible="false" Text="Location Id: " style="width:40%;text-align:left;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';" ></asp:Label>
                      <asp:Label ID="lblAccountIdContactLocation1" runat="server" Text=""  ></asp:Label> &nbsp;  &nbsp;  
        <asp:Label ID="lblAccountIdServiceAddress" runat="server" Visible="false" Text="Service Address: " style="width:40%;text-align:left;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';" ></asp:Label>
                      <asp:Label ID="lblAccountIdServiceAddress1" runat="server" Text=""  ></asp:Label>
                             <asp:Label ID="txtMode" runat="server" Text="" Visible="true" CssClass="dummybutton" ></asp:Label>
                     
                      </td> 
            </tr>

              <tr>
                <td style="width:7%;text-align:center;"> 
                   <asp:Button ID="btnADD" runat="server" Font-Bold="True" Text="ADD" Width="95%"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="TRUE" OnClientClick="return currentdatetimecontract()" Height="30px"/>
                 
                      </td>
                  
                  <td style="width:7%;text-align:center;">
                      <asp:Button ID="btnCopy" runat="server" Font-Bold="True" Text="COPY" Width="95%"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="TRUE" />
                    </td>
                     
                   <td style="width:7%;text-align:center;">
                       <asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="95%" OnClientClick="RefreshSubmit();"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="TRUE" />
                    </td>
                   <td style="width:7%;text-align:center;">
                      <asp:Button ID="btnRevision" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="REVISE" Width="95%" Visible="true" OnClientClick = "statuschangerevise()" />
                    </td>
                      

                  <td style="width:7%;text-align:center;">
                    <asp:Button ID="btnFilter" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SEARCH" Width="95%" Visible="true"/>
                    </td>
                  <td style="width:7%;text-align:center;">
                      <asp:Button ID="btnReset" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="RESET" Visible="true" Width="95%" />
                      </td>
                  <td style="width:7%;text-align:center;">
                    <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="95%" Visible="true" />
                    </td>
                  <td style="width:7%;text-align:center;">
                       <asp:Button ID="btnChangeStatus" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CH. ST." Visible="true" Width="95%"  />
                     </td>
                  
                    <td style="width:7%;text-align:center;">
                          <asp:Button ID="btnContractRenewal" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="RENEW or EXTEND" Width="97%" Visible="true" />
              
                    </td>

                   <td style="width:8%;text-align:center;">
                        <asp:Button ID="btnServiceSchedule" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="return checkstatus()" Text="SCHEDULE" Visible="true" Width="94%" />
               
                    </td>

                   <td style="width:8%;text-align:center;">
                         <asp:Button ID="btnServiceRecords" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="SERVICE" Visible="true" Width="95%" />
                 
                    </td>
                    <td style="width:11%;text-align:center;">
                         <asp:Button ID="btnTransactions" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="TRANSACTIONS" Visible="true" Width="95%" />
                 
                    </td>
                   <td style="width:7%;text-align:center;">
                                <asp:Button ID="btnQuit" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="QUIT" Width="95%" />
              </td>

                   <%-- <td style="width:8%;text-align:center;">
                             <asp:Button ID="btnNotes" runat="server"  CssClass="dummybutton" BackColor="#CFC6C0"  Font-Bold="True" Text="NOTES" Width="95%" Height="30px" Visible="TRUE" />
      
                   --%>
                  <td style="width:5%;text-align:center;">
                       &nbsp;</td> </td>
                
            </tr>

            <tr>
                 <td style="width:5%;text-align:center;">
                <asp:Button ID="btnEarlyCompletion" runat="server" Font-Bold="True" Text="Early Completion" Width="95%"  CssClass="button" BackColor="#CFC6C0" Visible="false" OnClientClick="return DoStatusCheck()" />
                </td><td style="width:5%;text-align:center;">
                <asp:Button ID="btnTerminate" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Terminate" Width="95%" Visible="false" OnClientClick="return DoStatusCheckT()"  />
                    </td><td style="width:5%;text-align:center;">
                <asp:Button ID="btnCancelByCompany" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel By Company" Width="95%" Visible="false"  OnClientClick="return DoStatusCheckC()" />
                </td><td style="width:5%;text-align:center;">
                          <asp:Button ID="btnInvoice" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="INVOICES" Width="95%" Visible="false" />

                    </td><td style="width:5%;text-align:center;">
                     <asp:Button ID="btnDelete" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" OnClientClick="Confirm(); currentdatetime()" Text="DELETE" Visible="False" Width="95%" />
                </td><td style="width:5%;text-align:center;">
                <asp:Button ID="btnContractDetail" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Visible="false"  Text="Details" Width="95%"  />
                </td><td style="width:5%;text-align:center;">
                </td><td style="width:5%;text-align:center;">
              
                <asp:Label ID="Label40" Style="color: white" runat="server" Visible="false" Text="Label"></asp:Label>    
                     <%-- <asp:Label ID="Label50" Style="color: white" runat="server" Visible="false" Text="Label"></asp:Label>    --%>
             
                 </td>
                
            </tr>
    </table>
      

             <table border="0" id="tablesearch" runat="server" style="border: 1px solid #CC3300; text-align:right; width:90%; border-radius: 25px;padding: 2px; width:100%; height:60px; background-color: #F3F3F3;">
            <tr>
                <td style="text-align:left;width:100%;">
                   
                    <table border="0" style="font-family: calibri; font-size: 15px; font-weight: bold; color: #000000;width:100%;padding-left:5px;">
                                                 <tr>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri;  color:black; text-align:right; width:7% ">
                                    Contract No.                
                            </td>
                                <td style="font-size:15px;font-weight:bold;font-family:Calibri;  color:black; text-align:left; width:15% ">
                                 <asp:TextBox ID="txtContractnoSearch" runat="server" style="text-align:left;padding-left:5px;" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled" AutoPostBack="True" ></asp:TextBox>         
                            </td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:8% ">
                                    Service Address
                            </td>
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; width:20% ">
                                 <asp:TextBox ID="txtServiceAddressSearch" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled" style="text-align:left;padding-left:5px;"></asp:TextBox>         
                                     
                            </td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:8% ">
                                 Contract Group
                            </td>

                            
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; width:12% ">
                           
                           <asp:DropDownList ID="ddlContractGrpSearch" runat="server" AppendDataBoundItems="True" Height="20px" Width="95%"><asp:ListItem>--SELECT--</asp:ListItem></asp:DropDownList>

                           </td>

                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:right; color:black; width:5% ">
                    
                                Category
                    
                           </td>

                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; width:8%">
                    
                                <asp:DropDownList ID="ddlCategorySearch" runat="server" AppendDataBoundItems="True" Height="20px" Width="90%">
                                    <asp:ListItem>--SELECT--</asp:ListItem>
                                </asp:DropDownList>
                    
                           </td>
                              <td colspan="1" style="text-align:left">
                                       <asp:Button ID="btnQuickSearch" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Search" Width="50%" />
                                 </td>
                        </tr>
                          <tr>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri;  color:black; text-align:right; width:6% ">
                                    Location Id
                            </td>
                                       <td style="font-size:15px;font-weight:bold;font-family:Calibri;  color:black; text-align:left; width:15% ">
                     
                                   <asp:TextBox ID="txtAccountIdSearch" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled" style="text-align:left;padding-left:5px;" AutoPostBack="True" ></asp:TextBox>         
                                   
                            <asp:ImageButton ID="ImageButton2" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"  Height="22px" Width="24px" ImageAlign="Top"     />                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
                                                 
                            
                                                      
                            </td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                    Client Name
                                 
                            </td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; width:20% ">
                                 <asp:TextBox ID="txtClientNameSearch" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled" style="text-align:left;padding-left:5px;" AutoPostBack="True"></asp:TextBox>         
                    
                                 <asp:ImageButton ID="ImageButton3" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                    
                            </td>

                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:8% ">
                                 Salesman
                            </td>

                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; width:12%">
                                 <asp:DropDownList ID="ddlSalesmanSearch" runat="server" AppendDataBoundItems="True" Height="20px" Width="95%"><asp:ListItem>--SELECT--</asp:ListItem></asp:DropDownList>
                            </td>

                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:right; color:black; width:5% ">
                                    Status
                            </td>
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; width:10%">
                                  <asp:TextBox ID="txtSearch1Status" runat="server" Width="70%" ReadOnly="FALSE"></asp:TextBox>
                                 <asp:ImageButton ID="btnSearch1Status" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg" ImageAlign="Top"
                                    Height="22px" Width="24px" />               
                            
                                                      
                            </td>
                            


                              <td> 
                                       <asp:Button ID="btnQuickReset" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Reset" Width="50%" OnClientClick="ClearSearch()" />
                                   </td>
                        </tr>
                                                 <tr>
                                                     <td style="font-size:15px;font-weight:bold;font-family:Calibri;  color:black; text-align:right; width:6% ">
                                                         <asp:Label ID="lblBranch1" runat="server" Text="Branch"></asp:Label>
                                                     </td>
                                                     <td style="font-size:15px;font-weight:bold;font-family:Calibri;  color:black; text-align:left; width:15% ">
                                                         <cc1:dropdowncheckboxes ID="ddlLocationSearch" runat="server" Width="13%" UseSelectAllNode = "true" AddJQueryReference="true" UseButtons="false" style="top: 0px; left: 0px" >
                                                         <Style2 SelectBoxWidth="85.5%" DropDownBoxBoxWidth="85.5%" DropDownBoxBoxHeight="120" />
                    
                                                </cc1:dropdowncheckboxes>

                                                     </td>
                                                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">&nbsp;</td>
                                                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; " colspan="3">
                                                         
                                                     </td>
                                                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:right; color:black; width:5% ">&nbsp;</td>
                                                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; width:10%">&nbsp;</td>
                                                     <td>&nbsp;</td>
                                                 </tr>
                    </table>
                      </td>
                    <td style="text-align:right;width:45%;display:inline;vertical-align:middle;padding-top:10px;">
                 
                </td>
            </tr>
        </table>


      <table style="text-align:right;width:100%">
            <tr>
                <td style="text-align:left;width:20%"><asp:Label ID="Label3" runat="server" Text="View Records" CssClass="CellFormat"></asp:Label><asp:DropDownList ID="ddlView" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True">10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList>

                </td>
                <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:center;width:80%" >
                    <asp:Label ID="lblIsInActiveAccountId" runat="server" ForeColor="Maroon" Visible="False"></asp:Label>
                 

                </td>
                </tr>
          </table>


         <table  style="width:100%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0% " >
         
             
                            <tr style="text-align:center;">
                                  <td colspan="9" style="width:100%;text-align:center">
                                      
                                        <asp:Panel ID="pnlGrid" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="100%" ScrollBars="Auto" style="text-align:center; width:1350px; margin-left:auto; margin-right:auto;"    Visible="true" Width="1330px">
          
                                          <asp:GridView ID="GridView1" Width="100%" Font-Size="15px" runat="server" OnRowDataBound = "OnRowDataBound" OnSelectedIndexChanged = "OnSelectedIndexChanged" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SQLDSContract" ForeColor="#333333" GridLines="Vertical"> 
                                              <AlternatingRowStyle BackColor="White"/>
                                                <Columns>
                                                  <asp:CommandField ShowHeader="True" ShowSelectButton="True" Visible="False">
                                                  <ControlStyle Width="4%" />
                                                  <ItemStyle Width="4%" Wrap="False" HorizontalAlign="Left" />
                                                  </asp:CommandField>
                                                  <asp:BoundField DataField="Rcno" HeaderText="Rcno" InsertVisible="False" ReadOnly="True" SortExpression="Rcno" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                  <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                                      </ItemTemplate>
                                                  </asp:TemplateField>
                                                  <asp:BoundField DataField="Status" HeaderText="ST" SortExpression="Status" >
                                                    <ItemStyle Width="2%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="RenewalSt" HeaderText="RS" SortExpression="RenewalSt" >
                                                    <ItemStyle Width="2%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="NotedSt" HeaderText="NS" SortExpression="NotedSt" Visible="False" />
                                                  <asp:BoundField DataField="GSt" HeaderText="GS" SortExpression="GSt" >
                                                    <ItemStyle Width="2%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="ContractNo" HeaderText="Contract No" SortExpression="ContractNo">
                                                    <ControlStyle Width="6%" />
                                                  <ItemStyle Wrap="False" Width="6%" />
                                                  </asp:BoundField>
                                                    <asp:BoundField DataField="Location" HeaderText="Branch" SortExpression="Location" />
                                                  <asp:BoundField DataField="ContractDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Contract Date" SortExpression="ContractDate" >
                                                    <ItemStyle Width="5%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ContractGroup" HeaderText="Contract Group" SortExpression="ContractGroup" >
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AccountId" HeaderText="Account Id" SortExpression="AccountId">
                                                    <ControlStyle Width="6%" />
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="CustName" HeaderText="Client Name" SortExpression="CustName">
                                                    <ControlStyle Width="20%" />
                                                  <ItemStyle HorizontalAlign="Left" Wrap="False" Width="18%" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="ServiceAddress" HeaderText="Service Address" SortExpression="ServiceAddress" >
                                                 
                                                    <ControlStyle Width="18%" />
                                                 
                                                    <ItemStyle Wrap="False" Width="18%" HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                 
                                                    <asp:BoundField DataField="FixedContinuous" HeaderText="Duration Type" />
                                                 
                                                  <asp:BoundField DataField="AgreeValue" HeaderText="Agreed Value" SortExpression="AgreeValue" >
                                                    <ControlStyle Width="8%" />
                                                    <ItemStyle HorizontalAlign="Right" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AmtBalance" HeaderText="Balance Value" SortExpression="AmtBalance" />
                                                 
                                                  <asp:BoundField DataField="InChargeId" HeaderText="In-Charge Id" SortExpression="InChargeId">
                                                    <ControlStyle Width="10%" />
                                                  <ItemStyle Wrap="False" Width="10%" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="StartDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Start Date" SortExpression="StartDate" >
                                                    <ControlStyle Width="5%" />
                                                    <ItemStyle Width="5%" Wrap="False" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="EndDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="End Date" >
                                                    <ControlStyle Width="5%" />
                                                    <ItemStyle Width="5%" Wrap="False" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="ActualEnd" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Actual End" Visible="False" >
                                                    <ControlStyle Width="5%" />
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ServiceStart" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Service Start">
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ServiceEnd" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Service End">
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Salesman" HeaderText="Salesman">
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="Scheduler" HeaderText="Scheduler" SortExpression="Scheduler" >
                                                    <ControlStyle Width="7%" />
                                                    <ItemStyle Width="7%" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CategoryID" HeaderText="Category ID" SortExpression="CategoryID" >
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="OurReference" HeaderText="Our Reference">
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="YourReference" HeaderText="Your Reference">
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RenewalContractNo" HeaderText="RenewalContractNo">
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RenewalDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Renewal Date">
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="OContractNo" HeaderText="Original Contract">
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Notes" HeaderText="Notes">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Comments" HeaderText="Service Instruction">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" >
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" >
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="Edited By" SortExpression="LastModifiedBy" >
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="LastModifiedOn" HeaderText="Edited On" SortExpression="LastModifiedOn" >
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>

                                                    
                                              <asp:TemplateField ><ItemTemplate> <asp:Button ID="btnEditHistory" runat="server" OnClick="btnEditHistory_Click" Text="History" CssClass="righttextbox" Height="25px" Visible="true" OnClientClick="currentdatetime()" ImageAlign="Top"   Width="80px"   />
                                               </ItemTemplate></asp:TemplateField>


                                              </Columns>
                                                   <EditRowStyle BackColor="#2461BF" />
                                                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                    <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
                                                    <PagerStyle ForeColor="White" HorizontalAlign="Left" BackColor="#507CD1" />
                                                    <RowStyle BackColor="#EFF3FB" Font-Names="Calibri" />
                                                    <SelectedRowStyle BackColor="#AEE4FF" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />


                                      </asp:GridView>


                                              </asp:Panel>
                                  </td>
                              </tr>
             
                 <tr>
                 <td>
                      <asp:SqlDataSource ID="SQLDSContract" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                </asp:SqlDataSource>
                  </td> 

                     <td>

                         &nbsp;</td>

                 <td>
                     <asp:SqlDataSource ID="SqlDSScheduleType" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                 </td>
                 <td>
                    <asp:SqlDataSource ID="SQLDSContractClientId" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                                      <SelectParameters>
                                          <asp:ControlParameter ControlID="txtclientid" Name="@accountid" PropertyName="Text" />
                                      </SelectParameters>
                                                
                                  </asp:SqlDataSource>
                     </td>     
                        <td>
                    <asp:SqlDataSource ID="SQLDSContractClientIdLocation" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                                      <SelectParameters>
                                          <asp:ControlParameter ControlID="txtclientid" Name="@accountid" PropertyName="Text" />
                                          <asp:ControlParameter ControlID="lblAccountIdContactLocation1" Name="@LocationId" PropertyName="Text" />
                                      </SelectParameters>
                                                
                                  </asp:SqlDataSource>
                     </td>     

                               <td>
                    <asp:SqlDataSource ID="SqlDSTerminationCode" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT CONCAT(Code, ' - ', Description) AS TC FROM tblterminationcode 
where Status = 'Y' ORDER BY Code">
                                                
                                  </asp:SqlDataSource>

  <asp:SqlDataSource ID="sqlDSContractCode" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT CONCAT(Code, ' : ', Description) AS CC FROM tblcontractcode 
where Status = 'Y' ORDER BY Code">
                                                
                                  </asp:SqlDataSource>
                     </td>     
                <td><asp:TextBox ID="txtclientid" runat="server" BorderStyle="None" ForeColor="White"></asp:TextBox></td>
                           </tr>
                                  <tr>
                                      <asp:TextBox ID="txt" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtDDLText" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtPopupType" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtcontractfrom" runat="server" Visible="False"></asp:TextBox>
                                       <asp:TextBox ID="txtRowCount" runat="server" Visible="False"></asp:TextBox>
                                      
                                      <asp:TextBox ID="txtModeRenew" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtRcnoRenew" runat="server" Visible="False"></asp:TextBox>
                                       <asp:TextBox ID="txtSelectedRow" runat="server" Visible="False"></asp:TextBox>
                                       <asp:TextBox ID="txtDisplayRecordsLocationwise" runat="server" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtIndustrySession" runat="server" Visible="False"></asp:TextBox>
                                       <asp:TextBox ID="txtMarketSegmentSession" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtContractGroupSession" runat="server" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtCompanyGroupSession" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtGroupAuthority" runat="server" Visible="False"></asp:TextBox> 
                                        <asp:TextBox ID="txtSalesmanSession" runat="server" Visible="False"></asp:TextBox>
                                        <asp:TextBox ID="txtBillingAdressSession" runat="server" Visible="False"></asp:TextBox>
                                     <asp:TextBox ID="txtLocationSession" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtIncludeinPortfolio" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                                  

                                       <asp:TextBox ID="txtBackDateContractSetup" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtBackDateContractTerminationSetup" runat="server" width="1px" Enabled="false" BorderStyle="None"  CssClass="dummybutton"></asp:TextBox>
                                  
                                     <asp:TextBox ID="txtBackDateContract" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtBackDateContractTermination" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                        
                                      <asp:TextBox ID="txtBackDateContractSameMonthOnly" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtBackDateContractTerminationSameMonthOnly" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton" ></asp:TextBox>
                        
                                        <asp:TextBox ID="txtFutureDateContractTermination" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                                                
                                      <asp:TextBox ID="txtFileAccess" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtFileUpload" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                                      <asp:TextBox ID="txtFileDelete" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                                     <asp:TextBox ID="txtContractRevisionTerminationCode" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                                     <asp:TextBox ID="txtPrefixDocNoContract" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                                       <asp:TextBox ID="txtbtnAutoRenew" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                                       <asp:TextBox ID="txtTotServicerec" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                                       <asp:TextBox ID="txtFixedContinuousVisible" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                                       <asp:TextBox ID="txtIsPopupChSt" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                                <asp:TextBox ID="txtEditAgreeValuePercChange" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                                <asp:TextBox ID="txtAgreeValueToUpdate" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                             
                                  <asp:TextBox ID="txtTeamIDMandatory" runat="server" width="1px"  BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                                <asp:TextBox ID="txtServiceByMandatory" runat="server" width="1px"  BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                               <asp:TextBox ID="txtRegenerateSchedule" runat="server" width="1px"  BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                               <asp:TextBox ID="lblAccountIdLocation1" runat="server" width="1px"  BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                             <asp:TextBox ID="txtExtendContractEndDate" runat="server" width="1px"  BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                              <asp:TextBox ID="txtAgreeValueOriginalForReviseandRenewal" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                              <asp:TextBox ID="txtActualEndChStForRevise" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                              <asp:TextBox ID="txtActualEndChStForReNew" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                            
                                        <asp:TextBox ID="txtRcnoDetailLog" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                              <asp:TextBox ID="txtFutureDated" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                              <asp:TextBox ID="txtServiceStarted" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                             
                              <asp:TextBox ID="txtDefaultTaxCode" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                              <asp:TextBox ID="txtGSTRatePct" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                              <asp:TextBox ID="txtAllowTerminationBeforeLastService" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                              <asp:TextBox ID="txtOpenService" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                              <asp:TextBox ID="txtIsTerminationExists" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                              <asp:TextBox ID="txtPriceIncreaseLimit" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                              <asp:TextBox ID="txtPriceDecreaseLimit" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                              <asp:TextBox ID="txtReviseTerminatedContract" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                            
                                      
                              <asp:TextBox ID="txtRevisionNewContract" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                              <asp:TextBox ID="txtRenewalTerminationContract" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                              <asp:TextBox ID="txtRenewalNewContract" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
                                       
                               </tr>

        

             <tr style="text-align:center;width:100%">
                <td colspan="9" style="text-align:left;padding-left:5px;">

<asp:TabContainer ID="tb1" runat="server" ActiveTabIndex="0" Font-Names="Calibri" Height="100%" Width="90%" CssClass="ajax__tab_xp" AutoPostBack="True">
    <asp:TabPanel  runat="server"   HeaderText=" General & Billing Info" ID="TabPanel1"><HeaderTemplate>General Info &amp; Summary</HeaderTemplate><ContentTemplate><table border="0" style="width:75%; margin:auto"></table><br /><asp:SqlDataSource ID="SqlDSContractNo" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT ContractNo, ProspectId, StaffId, Status, ContactType, CustCode, CustName, CustAddr, InChargeId, EntryDate, ContractDate, Contact, Telephone, Fax, AgreeValue, Duration, DurationMs, StartDate, EndDate, ActualEnd, ServiceNo, ServiceBal, ServiceAmt, HourNo, HourBal, HourAmt, CallNo, CallBal, CallAmt, ResponseHours, CancelCharges, CompensatePct, CompensateMax, WeekDayFrom1, WeekDayFrom2, WeekDayFrom3, WeekDayTo1, WeekDayTo2, WeekDayTo3, WeekEndFrom1, WeekEndFrom2, WeekEndFrom3, WeekEndTo1, WeekEndTo2, WeekEndTo3, DayVisitRate1, DayVisitRate2, DayVisitRate3, DayHourRate1, DayHourRate2, DayHourRate3, DayTransport1, DayTransport2, DayTransport3, EndVisitRate1, EndVisitRate2, EndVisitRate3, EndHourRate1, EndHourRate2, EndHourRate3, EndTransport1, EndTransport2, EndTransport3, Notes, Rcno, Comments, ActualStaff, ServiceNoActual, HourNoActual, CallNoActual, MinDuration, OContractNo, RenewalSt, RenewalDate, UnitNo, UnitBal, UnitAmt, UnitNoActual, NotedSt, NotedDate, settle, ActualServHrs, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, YourReference, ContractGroup, PrintBody, RenewalContractNo, ServDayMethod, ServDay, GSt, ServiceStart, ServiceEnd, ServiceFrequence, TimeIn, TimeOut, WarrantyStart, WarrantyEnd, ContractValue, PerServiceValue, Disc_Percent, DiscAmt, BillingFrequency, DayService1, DayService2, DayService3, DayService4, Support, ScheduleType, TeamID, WebCreateDeviceID, WebCreateSource, WebFlowFrom, WebFlowTo, WebEditSource, WebDeleteStatus, WebLastEditDevice, Postal, LocateGrp, Scheduler, ContactPersonMobile, SalesMan, OurReference, GSTNos, ServiceDescription, PrintingRemarks, Rev, MainContractNo, AmtCompleted, AmtBalance, AllocatedSvcTime, Remarks, QuotePrice, QuoteUnitMS, CompanyGroup, SalesGRP, AmtBalance FROM tblcontract WHERE (ContractNo = @contractno)"><FilterParameters><asp:ControlParameter ControlID="txtcontractno" Name="ContractNo" PropertyName="Text" /></FilterParameters><SelectParameters><asp:ControlParameter ControlID="txtContractNo" Name="@contractno" PropertyName="Text" /></SelectParameters></asp:SqlDataSource>
        
        <asp:Panel ID="pnlPopUpClient" runat="server" BackColor="White" Width="98%" Height="80%" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center" ScrollBars="Horizontal">
            
            <table border="0" style="margin-left:auto; margin-right:auto; "><tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Select Customer</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlClientClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr><tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri; color:black;text-align:center;padding-left:20px;">&#160;Search Name &#160;&#160; <asp:ImageButton ID="btnPopUpClientReset" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/resetbutton.jpg" Width="24px"></asp:ImageButton><asp:TextBox ID="txtPopUpClient" runat="server" Height="16px" MaxLength="50" Width="400px" ForeColor="Gray" onblur = "WaterMarkClient(this, event);" onfocus = "WaterMarkClient(this, event);" AutoPostBack="True">Search Here for AccountID or Client Name or Contact Person</asp:TextBox><asp:ImageButton ID="btnPopUpClientSearch" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" OnClick="btnPopUpClientSearch_Click" Width="24px" ></asp:ImageButton></td><td><asp:TextBox ID="txtPopupClientSearch" runat="server" Visible="False"></asp:TextBox></td></tr></table><div style="text-align:center; padding-left: 10px; padding-bottom: 5px;"><div class="AlphabetPager"><asp:Repeater ID="rptClientAlphabets" runat="server" Visible="False"><ItemTemplate><asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("Value")%>' OnClick="ClientAlphabet_Click" ForeColor="Black" /></ItemTemplate></asp:Repeater></div><br /><asp:GridView ID="gvClient" runat="server" DataSourceID="SqlDSClient" OnRowDataBound = "OnRowDataBoundgClient" OnSelectedIndexChanged = "OnSelectedIndexChangedgClient" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
         CellPadding="2" GridLines="None" Font-Size="15px" Width="99%">
        
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" visible="False"><ControlStyle Width="4%" />
             <HeaderStyle HorizontalAlign="Left" /><ItemStyle Width="4%" /></asp:CommandField>
            <asp:BoundField DataField="AccountID" SortExpression="AccountID" ><ControlStyle Width="7%" CssClass="dummybutton" /><HeaderStyle Wrap="False" HorizontalAlign="Left" CssClass="dummybutton" /><ItemStyle Width="7%" CssClass="dummybutton" /></asp:BoundField>
            <asp:BoundField DataField="LocationID" HeaderText="Location ID" ><HeaderStyle HorizontalAlign="Left" /><ItemStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="ID" SortExpression="ID" HeaderText="ID"><ControlStyle Width="8%" /><HeaderStyle Width="100px" HorizontalAlign="Left" /><ItemStyle Width="8%" Wrap="False" /></asp:BoundField><asp:BoundField DataField="Name" HeaderText="Client Name" SortExpression="Name"><ControlStyle Width="30%" /><HeaderStyle HorizontalAlign="Left" /><ItemStyle Width="30%" Wrap="False" /></asp:BoundField><asp:BoundField DataField="ContractGroup" HeaderText="Contract Group" SortExpression="ContractGroup"><HeaderStyle HorizontalAlign="Left" /><ItemStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="ContactPerson" HeaderText="Contact Person" SortExpression="ContactPerson"><ControlStyle Width="30%" CssClass="dummybutton" /><HeaderStyle Wrap="False" HorizontalAlign="Left" CssClass="dummybutton" /><ItemStyle Width="30%" Wrap="False" CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="Address1" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="Mobile" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="Email" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="LocateGRP" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="CompanyGroup" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="AddBlock" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="AddNos" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="AddFloor" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="AddUnit" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="Fax" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="Mobile" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="Telephone" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="Salesman" HeaderText="Salesman" ><ControlStyle Width="9%" CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle Width="9%" CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="Industry" HeaderText="Industry" ><ControlStyle Width="19%" CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle Width="19%" CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="billaddress1"><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="billstreet"><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="billbuilding"><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="billcity"><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="billpostal"><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="ServiceAddress" HeaderText="Service Address" ><ItemStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="AddStreet" HeaderText="Street" >
            <ItemStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="AddBuilding" HeaderText="Building" ><ItemStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="AddCountry" HeaderText="Country" ><ItemStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="AddPostal" HeaderText="Postal" ><ItemStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="CompanyGroupD"><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField>
            <asp:BoundField DataField="Location" HeaderText="Branch" />
            <asp:ButtonField ButtonType="Button"  Text="Edit History"   />
        </Columns><EditRowStyle BackColor="#999999" /><FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" /><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#E9E7E2" /><SortedAscendingHeaderStyle BackColor="#506C8C" /><SortedDescendingCellStyle BackColor="#FFFDF8" /><SortedDescendingHeaderStyle BackColor="#6F8DAE" /></asp:GridView><asp:SqlDataSource ID="SqlDSClient" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"><FilterParameters><asp:ControlParameter Name="ContType" ControlID="ddlContactType" PropertyName="SelectedValue" Type="String" /></FilterParameters><SelectParameters><asp:ControlParameter ControlID="txtContType4" Name="@contType4" PropertyName="Text" /></SelectParameters></asp:SqlDataSource></div></asp:Panel><table border="0" style="width:75%; margin:auto">
        
             <tr style="width:95%">
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ; text-align:right;">&nbsp;</td>
            <td style="font-size:15px;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:60% ">
                
                <asp:CheckBox ID="chkAutoRenew" runat="server" Enabled="False" Text="Auto Renew" />
                
                
                 &nbsp;<asp:ImageButton ID="btnAutoRenew" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" />
                
                
                 </td>
        </tr>
                    
                                
                    <tr style="width:95%">
                        <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ; text-align:right;">&nbsp;</td>
                        <td style="font-size:15px;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:60% ">
                            <asp:CheckBox ID="chkSignedServiceAgreement" runat="server" Enabled="False" />
                            <asp:Label ID="txtSignedAgreementReference" runat="server" BackColor="#CCCCCC" Enabled="False" Text="Label" Width="37%"></asp:Label>
                        </td>
             </tr>
                    
                                
                    <tr style="width:95%">
            <td colspan="2" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">Client Details </td>
        </tr>
                   
                     <tr style="width: 95%;"><td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 18%; text-align: right;">
            <asp:Label ID="lblBranch2" runat="server" Text="Branch"></asp:Label></td><td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; padding-left: 1%; width: 72%">
                <asp:DropDownList ID="txtLocation" runat="server" AppendDataBoundItems="True" Width="25%">
                    <asp:ListItem>--SELECT--</asp:ListItem>
                </asp:DropDownList>
            </td></tr> 
                    
                    <tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right;">Status</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72%">
                        <asp:TextBox ID="txtStatus" runat="server"   AutoCompleteType="Disabled"  Height="16px" Width="25%"></asp:TextBox>
                       </td></tr>
             <tr style="width:95%">
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right;">Contract No. </td>
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72%">
                     <asp:TextBox ID="txtContractNo" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" MaxLength="50" ReadOnly="True" Width="40%"></asp:TextBox>
                 </td>
             </tr>
             <tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Contract Date <asp:Label ID="Label4" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% "><asp:UpdatePanel ID="UpdatePanel3" runat="server"><ContentTemplate><asp:TextBox ID="txtContractDate" runat="server" Height="16px" Width="40%" AutoCompleteType="Disabled" onchange="PopulateOtherDays()" AutoPostBack="True"></asp:TextBox><asp:CalendarExtender ID="calConDate" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtContractDate" TargetControlID="txtContractDate" Enabled="True" /></ContentTemplate></asp:UpdatePanel></td></tr>
                    
                    <tr style="width:95%; vertical-align:top">
                        <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; vertical-align:top ">Agreement Type<asp:Label ID="Label61" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td>
                        <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72%; vertical-align:top;  ">
                            <asp:DropDownList ID="ddlAgreementType" runat="server" AppendDataBoundItems="True" Height="20px" Width="40.5%" style="vertical-align:top;"   ><asp:ListItem>--SELECT--</asp:ListItem></asp:DropDownList>
                            <asp:Label ID="lblAgreentTypeRemarks" runat="server" Font-Size="14px" ForeColor="Red" Height="24px" Width="58%"  ></asp:Label> </td></tr>
                    
                    <tr style="width:95%; vertical-align:top">
                        <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; vertical-align:top ">Contract Code<asp:Label ID="Label103" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72%; vertical-align:top;  ">
                            <asp:DropDownList ID="ddlContractCode" runat="server" AppendDataBoundItems="True" DataSourceID="sqlDSContractCode" DataTextField="CC" DataValueField="CC" Width="40.5%">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
             </tr>
                    
                    <tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Account Type <asp:Label ID="Label5" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% "><asp:DropDownList ID="ddlContactType" runat="server" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="40.5%"><asp:ListItem Selected="True" Value="COMPANY">CORPORATE</asp:ListItem><asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem></asp:DropDownList></td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Account Id <asp:Label ID="Label6" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% "><asp:TextBox ID="txtAccountId" runat="server" Height="16px" Width="40%" AutoCompleteType="Disabled" MaxLength="200" AutoPostBack="True" ></asp:TextBox><asp:ImageButton ID="btnClient" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"  Height="22px" Width="24px" ImageAlign="Top"                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
                                     OnClientClick="ConfirmSelection()" /><asp:ModalPopupExtender ID="mdlPopUpClient" runat="server" CancelControlID="btnPnlClientClose" PopupControlID="pnlPopUpClient" TargetControlID="btnDummyClient" BackgroundCssClass="modalBackground" Enabled="True" DynamicServicePath="" ></asp:ModalPopupExtender></td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Account Name <asp:Label ID="Label7" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% "><asp:TextBox ID="txtCustName" runat="server" Height="16px" Width="40%" AutoCompleteType="Disabled" MaxLength="200"></asp:TextBox></td></tr>
                    
                    <tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">
      
                                           Manual Contract No. </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% ">
                            <asp:TextBox ID="txtYourRef" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="25" Width="40%"></asp:TextBox>
                            <asp:ImageButton ID="btnClient0" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" /></td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ; text-align:right;">PO No. </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% ">
            <asp:TextBox ID="txtPONo" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="100" Width="40%"></asp:TextBox>
            <asp:ImageButton ID="btnClient1" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" /></td></td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ; text-align:right;">Other Reference</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% ">
            <asp:TextBox ID="txtOurRef" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" Width="40%"></asp:TextBox>
            <asp:ImageButton ID="btnClient2" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" /></td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Billing Address</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% "><asp:TextBox ID="txtBillingAddress" runat="server" Font-Names="calibri" Font-Size="15px" AutoCompleteType="Disabled" Height="60px" Width="40%" TextMode="MultiLine"></asp:TextBox></td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Service Address</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% "><asp:TextBox ID="txtServiceAddressCons" runat="server" Font-Names="calibri" Font-Size="15px" AutoCompleteType="Disabled" Height="60px" Width="40%" TextMode="MultiLine"></asp:TextBox></td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; "><asp:TextBox ID="txtAddress" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="25" Visible="False" Width="10%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% "><asp:DropDownList ID="ddlLocateGrp" runat="server" Width="10%" AppendDataBoundItems="True" Height="20px" Visible="False"><asp:ListItem>--SELECT--</asp:ListItem></asp:DropDownList>
                
                <asp:TextBox ID="txtOfficeAddress" runat="server" AutoCompleteType="Disabled" Height="16px" Width="10%" CssClass="dummybutton" ></asp:TextBox><asp:TextBox ID="txtPostal" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="20" Width="10%" CssClass="dummybutton"></asp:TextBox></td></tr><table border="0" style="width:75%; margin:auto"><tr style="width:95%"><td colspan="6" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000;  background-color: #C0C0C0;">Contract Details </td><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right;">Company Group <asp:Label ID="Label12" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:40%"><asp:DropDownList ID="ddlCompanyGrp" runat="server" AppendDataBoundItems="True" Height="20px" Width="40.5%"><asp:ListItem>--SELECT--</asp:ListItem></asp:DropDownList><asp:ImageButton ID="btnClient7" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" /></td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Salesman <asp:Label ID="Label8" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:70% "><asp:DropDownList ID="ddlSalesman" runat="server" AppendDataBoundItems="True" Height="20px" Width="40.5%"><asp:ListItem>--SELECT--</asp:ListItem></asp:DropDownList><asp:ImageButton ID="btnClient6" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" /></td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Scheduler <asp:Label ID="Label9" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:70% "><asp:DropDownList ID="ddlScheduler" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="20px" Width="40.5%"><asp:ListItem>--SELECT--</asp:ListItem></asp:DropDownList><asp:ImageButton ID="btnClient5" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" /></td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Contract Group<asp:Label ID="Label56" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:70% "><asp:DropDownList ID="ddlContractGrp" runat="server" AppendDataBoundItems="True" Height="20px" Width="40.5%" AutoPostBack="True"><asp:ListItem>--SELECT--</asp:ListItem></asp:DropDownList></td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right;">Category ID</td><td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:70% "><asp:TextBox ID="txtCategoryID" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="30" Width="40%"></asp:TextBox></td></tr>
                 <tr style="width:95%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right;">&nbsp;</td>
                     <td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:70% ">
                         <asp:CheckBox ID="chkExcludeFromBatchPriceChange" runat="server" Enabled="False" Text="Exclude From Batch Price Change" />
                         &nbsp;
                         <asp:ImageButton ID="btnClient10" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" />
                     </td>
                 </tr>
                 <tr style="width:95%; display:none"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; "></td><td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:20% "><asp:TextBox ID="txtConDetVal" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="getNetAmount()" style="text-align:right" Width="40%"></asp:TextBox></td></tr><tr style="width:95%; display:none;"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color: black; padding-left:1%; width:15%; text-align:right; ">&nbsp;</td><td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:20% "><asp:TextBox ID="txtDisPercent" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="getNetAmount()" style="text-align:right; padding-right:1%;" Width="16%"></asp:TextBox>&nbsp; &nbsp; &nbsp; <asp:TextBox ID="txtDisAmt" runat="server" AutoCompleteType="Disabled" CausesValidation="True" Height="16px" onchange="getNetAmount()" style="text-align:right;  padding-left:3%;" Width="15%"></asp:TextBox></td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">
                 &nbsp;</td><td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:20% ">
                     <asp:CheckBox ID="chkGSTInclusive" runat="server" Enabled="False" Text="GST Inclusive" AutoPostBack="True" Visible="False" />
                     &nbsp;
                     <asp:Button ID="btnGSTInclusive" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CALCULATE AGREED VALUE W/O GST" Width="38%" />
                     
                 </td></tr>
                    
                    <tr style="width:95%">
                        <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Agreed Value </td>
                        <td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:20% ">
                            <asp:TextBox ID="txtAgreeVal" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="getValuePerMonth()" style="text-align:right" Width="40%"></asp:TextBox>
                            <asp:ImageButton ID="btnClient8" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" />
                            &nbsp;<asp:Button ID="btnHistory" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="HISTORY" Width="20%" />
                        </td>
                 </tr>
                    
                    <tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; "> 
                        <asp:Label ID="lblDurationType" runat="server" Text="Duration Type"></asp:Label>
                        </td><td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% "><asp:Panel ID="pnlFixContinuous" runat="server" BorderColor="#993300" BorderStyle="Solid" BorderWidth="1px" Font-Size="15px" Height="19%" HorizontalAlign="Left" Width="40%"><asp:RadioButtonList ID="rbtFixedContinuous" runat="server" Font-Size="15px" ForeColor="Black" onchange="CalculateDates()" RepeatDirection="Horizontal" ValidationGroup="ContTypeFixedContinuous" Width="70%" BorderStyle="None"><asp:ListItem Text="FIXED" Value="F"></asp:ListItem><asp:ListItem Text="CONTINUOUS" Value="C"></asp:ListItem></asp:RadioButtonList></asp:Panel></td></tr>
             
                    <tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Duration <asp:Label ID="Label54" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% "><asp:TextBox ID="txtDuration" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="CalculateDates()" Width="40%"></asp:TextBox></td></tr>
                    
                    <tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; "><asp:CheckBox ID="chkInactive" runat="server" Text=" Inactive" CssClass="dummybutton" /></td><td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% "><asp:Panel ID="Panel2" runat="server" BorderColor="#993300" BorderStyle="Solid" BorderWidth="1px" Font-Size="15px" Height="19%" HorizontalAlign="Center" Width="40%"><asp:RadioButtonList ID="rbtLstDuration" runat="server" Font-Size="15px" ForeColor="Black" onchange="CalculateDates()" RepeatDirection="Horizontal" ValidationGroup="durationtype" Width="80%"><asp:ListItem Text="DAYS" Value="DAYS"></asp:ListItem><asp:ListItem Text="WEEKS" Value="WEEKS"></asp:ListItem><asp:ListItem Text="MONTHS" Value="MONTHS"></asp:ListItem><asp:ListItem Text="YEARS" Value="YEARS"></asp:ListItem></asp:RadioButtonList></asp:Panel></td></tr>
              
                    <tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Value / Month </td><td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:20% "><asp:TextBox ID="txtValPerMnth" runat="server" AutoCompleteType="Disabled" Height="16px" style="text-align:right" Width="40%"></asp:TextBox></td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Contract Start </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:UpdatePanel ID="UpdatePanel2" runat="server"><ContentTemplate><asp:TextBox ID="txtContractStart" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="CalculateContractDates()" Width="90%" AutoPostBack="True"></asp:TextBox><asp:CalendarExtender ID="calConStart" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtContractStart" TargetControlID="txtContractStart"></asp:CalendarExtender></ContentTemplate></asp:UpdatePanel></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">End </td>
                    
                    <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtContractEnd" runat="server" AutoCompleteType="Disabled" Height="16px"  Width="100%"></asp:TextBox>
                  
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% ">
                     <asp:ImageButton ID="btnExtendContractEndDate" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" />
                 </td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Service Start </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:UpdatePanel runat="server"><ContentTemplate><asp:TextBox ID="txtServStart" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="ValidateDatesService()" Width="90%" AutoPostBack="True"></asp:TextBox>
                     <asp:CalendarExtender ID="calServStart" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtServStart" TargetControlID="txtServStart"></asp:CalendarExtender></ContentTemplate></asp:UpdatePanel></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% "><asp:TextBox ID="txtServStartDay" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="90%" Enabled="False"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">End </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtServEnd" runat="server" AutoCompleteType="Disabled" Height="16px"  Width="100%"></asp:TextBox><asp:CalendarExtender ID="calServEnd" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtServEnd" TargetControlID="txtServEnd"></asp:CalendarExtender></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% "><asp:TextBox ID="TxtServEndDay" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="90%" Enabled="False"></asp:TextBox></td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Actual Service Start</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtActualServiceStart" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="16px" Width="92%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:13%; text-align:right; ">
                 Actual End</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtActualEnd" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="16px" Width="100%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "></td></tr>
                 <tr style="width:95%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">End of Last Schedule&nbsp;</td>
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% ">
                         <asp:TextBox ID="txtEndofLastSchedule" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" onchange="CalculateContractDates()" Width="90%"></asp:TextBox>
                         <asp:CalendarExtender ID="txtEndofLastSchedule_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtContractStart" TargetControlID="txtEndofLastSchedule">
                         </asp:CalendarExtender>
                     </td>
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% ">&nbsp;</td>
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:13%; text-align:right; ">Date of Last Service</td>
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% ">
                         <asp:TextBox ID="txtLastServiceDate" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="16px" Width="100%"></asp:TextBox>
                     </td>
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% ">&nbsp;</td>
                 </tr>
                 <tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ; text-align:right;">Contract Notes </td><td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% "><asp:TextBox ID="txtContractNotes" runat="server" AutoCompleteType="Disabled" Font-Names="calibri" Font-Size="15px" Height="40px" MaxLength="500" TextMode="MultiLine" Width="99%"></asp:TextBox></td><td style="vertical-align:top"><asp:ImageButton ID="btnClient3" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" /></td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ; text-align:right;">On-Hold Date</td><td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% ">
                 <asp:TextBox ID="txtOnHoldDate" runat="server" AutoCompleteType="Disabled" Height="16px" Width="40%"></asp:TextBox>
                 </td></tr>
      
                 <tr style="width:95%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ; text-align:right;">On-Hold Code</td>
                     <td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% ">
                         <asp:TextBox ID="txtOnHoldCode" runat="server" AutoCompleteType="Disabled" Height="16px" Width="40%"></asp:TextBox>
                     </td>
                 </tr>
                 <tr style="width:95%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ; text-align:right;">On-Hold Description</td>
                     <td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% ">
                         <asp:TextBox ID="txtOnHoldDescription" runat="server" AutoCompleteType="Disabled" Height="16px" Width="40%"></asp:TextBox>
                     </td>
                 </tr>
                 <tr style="width:95%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ; text-align:right;">On-Hold Comments</td>
                     <td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% ">
                         <asp:TextBox ID="txtOnHoldComments" runat="server" AutoCompleteType="Disabled" Font-Names="calibri" Font-Size="15px" Height="40px" MaxLength="500" TextMode="MultiLine" Width="99%"></asp:TextBox>
                     </td>
                 </tr>
                 <tr style="width:95%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ; text-align:right;">Termination Type</td>
                     <td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% ">
                         <asp:TextBox ID="txtTerminationType" runat="server" AutoCompleteType="Disabled" Height="16px" Width="40%"></asp:TextBox>
                     </td>
                 </tr>
      
                 <tr style="width:95%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ; text-align:right;">Termination Code</td>
                     <td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% ">
                         <asp:TextBox ID="txtTerminationCode" runat="server" AutoCompleteType="Disabled" Height="16px" Width="40%"></asp:TextBox>
                     </td>
                 </tr>
      
                <tr style="width:95%">
            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ; text-align:right;">Termination Description</td>
            <td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% ">
                <asp:TextBox ID="txtTerminationDesc" runat="server" AutoCompleteType="Disabled" Height="16px" Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ; text-align:right;">Termination Comments</td><td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% "><asp:TextBox ID="txtTerminationDescription" runat="server" AutoCompleteType="Disabled" Font-Names="calibri" Font-Size="15px" Height="40px" MaxLength="500" TextMode="MultiLine" Width="99%"></asp:TextBox></td></tr>
    </table><br />
              
                          
                    <table border="0" style="width:75%; margin:auto"><tr style="width:95%"><td colspan="2" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000;  background-color: #C0C0C0;">Industry Segment </td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right;">Industry<asp:Label ID="Label57" runat="server" Font-Size="14px" ForeColor="Red" Text="*" Visible="False"></asp:Label></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:40%"><asp:TextBox ID="ddlIndustry" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="20" Width="40%"></asp:TextBox></td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right;">Market Segment ID<asp:Label ID="Label60" runat="server" Font-Size="14px" ForeColor="Red" Text="*" Visible="False"></asp:Label></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:40%"><asp:TextBox ID="ddlMarketSegmentID" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="20" Width="40%"></asp:TextBox></td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right;">Service TypeID<asp:Label ID="Label58" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:40%"><asp:DropDownList ID="ddlServiceTypeID" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSServiceTypeID" DataTextField="ServiceType" DataValueField="ServiceType" Width="40.5%" Height="25px" ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList><asp:SqlDataSource ID="SqlDSServiceTypeID" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT ServiceTypeID,description,CONCAT(SERVICETYPEID,' - ',DESCRIPTION) AS SERVICETYPE FROM tblindustrysegmentservice ORDER BY ServiceTypeID"></asp:SqlDataSource></td></tr>
        
        <tr style="display: none; width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right;">Portfolio(Y/N)</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:40%"><asp:DropDownList ID="ddlPortfolioYN" runat="server" CssClass="chzn-select" Width="40.5%" Height="25px" onchange ="calculateportfoliovalues()" ><asp:ListItem Text="YES" Value="1" /><asp:ListItem Text="NO" Value="0" /></asp:DropDownList></td></tr>
        
        <tr style="display: none; width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right;">Portfolio Value</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:40%"><asp:TextBox ID="txtPortfolioValue" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="getNetAmount()" style="text-align:right" Width="40%"></asp:TextBox></td></tr></table><br />
                    
          
                    
                              <table border="0" style="width:75%; margin:auto">
                                  
            <tr style="width:95%"><td colspan="7" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000;  background-color: #C0C0C0;">Service Summary </td></tr>
            <tr style="width:95%">
                <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right;"></td>
                <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:center; color:black; width:12%;  ">Agree </td>
              
                  <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:center; color:black; width:12%;  ">Scheduled </td>
                  <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:center; color:black; width:12%;  ">Cancelled / Terminated </td>
              
                <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:center; color:black;  width:12%;  ">Completed </td>
                <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:center; color:black;  width:12%;  ">Balance </td>
                <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%"><asp:HiddenField ID="hScrollTop" runat="server"></asp:HiddenField></td>
           </tr>

            <tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right;">Scheduled Service </td>
                <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; width:12%;"> </td>
                <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%"><asp:TextBox ID="txtServiceNo" runat="server" AutoCompleteType="Disabled" Height="16px" style="text-align:right" Width="98%"></asp:TextBox></td>
                <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%;"><asp:TextBox ID="txtServiceNoCT" runat="server"   AutoCompleteType="Disabled" Height="16px" Width="98%" style="text-align:right" ></asp:TextBox> </td>
                <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%"><asp:TextBox ID="txtServiceNoActual" runat="server" AutoCompleteType="Disabled" Height="16px" style="text-align:right" Width="98%"></asp:TextBox></td>
                <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%"><asp:TextBox ID="txtServiceBal" runat="server"   AutoCompleteType="Disabled" Height="16px" Width="95%" style="text-align:right"></asp:TextBox></td>
                <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%"></td>

            </tr>
            <tr style="width:95%">
                 <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Service Value </td>
                 <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:right; padding-left:1%; width:12%;"><asp:TextBox ID="txtAgreeValServiceSummary" runat="server"   AutoCompleteType="Disabled" Height="16px" Width="98%" style="text-align:right" ></asp:TextBox> </td>
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12% "><asp:TextBox ID="txtServiceAmt" runat="server"   AutoCompleteType="Disabled" Height="16px" Width="98%" style="text-align:right" ></asp:TextBox></td>
                 <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%;  "><asp:TextBox ID="txtServiceAmtCT" runat="server"   AutoCompleteType="Disabled" Height="16px" Width="98%" style="text-align:right" ></asp:TextBox> </td>
                 <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%"><asp:TextBox ID="txtServiceAmtActual" runat="server" AutoCompleteType="Disabled" Height="16px" style="text-align:right" Width="98%"></asp:TextBox></td>
                 <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%"><asp:TextBox ID="txtServiceAmtBal" runat="server"   AutoCompleteType="Disabled" Height="16px" Width="98%" style="text-align:right" ></asp:TextBox></td>
                 <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%"></td>
           </tr>
            
            <tr style="width:95%">
                <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Area</td>
                <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; width:12%;  "> </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12% "><asp:TextBox ID="txtTotalArea" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="CalculateWarrantyDates()" Width="98%" style="text-align:right"></asp:TextBox></td>
                 <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%;  "><asp:TextBox ID="txtCTArea" runat="server"   AutoCompleteType="Disabled" Height="16px" Width="98%" style="text-align:right" ></asp:TextBox> </td>
                  <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%"><asp:TextBox ID="txtCompletedArea" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="CalculateWarrantyDates()" Width="98%" style="text-align:right"></asp:TextBox></td>
                <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%; text-align:left; "><asp:TextBox ID="txtBalanceArea" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="CalculateWarrantyDates()" Width="98%" style="text-align:right"></asp:TextBox></td>
                <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%">&nbsp;</td>

            </tr>
            <tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Bait Station</td>
                 <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; width:12%;  "> </td>
                  <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12% "><asp:TextBox ID="txtBaitStationTotal" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="CalculateWarrantyDates()" style="text-align:right" Width="98%"></asp:TextBox></td>
                 <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%;  "><asp:TextBox ID="txtBaitStationCT" runat="server"   AutoCompleteType="Disabled" Height="16px" Width="98%" style="text-align:right" ></asp:TextBox> </td>
                  <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%"><asp:TextBox ID="txtBaitStationInstalled" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="CalculateWarrantyDates()" style="text-align:right" Width="98%"></asp:TextBox></td>
                <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%; text-align:left; "><asp:TextBox ID="txtBaitStationBalance" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="CalculateWarrantyDates()" style="text-align:right" Width="98%"></asp:TextBox></td>
               
       
                
                <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%">
                  <asp:Button ID="btnRecalculate" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Recalculate" Width="98%" /></td>
                   
                </td>

            </tr>

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             </table><br />
                    <table border="0" style="width:75%; margin:auto; display:none""><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%;  "><asp:CheckBox ID="ChkWithWarranty" runat="server" Text="With Warranty" onchange="EnableDisableWarranty()" /></td></tr></table><table border="0" style="width:75%; margin:auto; display:none"><tr style="width:95%"><td colspan="6" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">Warranty Details </td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right;">Warranty Duration </td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15%"><asp:TextBox ID="txtWarrDurtion" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="CalculateWarrantyDates()" Width="90%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtContType1" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" style="text-align:right" Width="1%"></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%"></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15%"></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15%"></td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18% ; text-align:right;">Warranty Start </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtWarrStart" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="ValidateDatesWarrnty()" Width="90%"></asp:TextBox><asp:CalendarExtender ID="calWarrStart" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtWarrStart" TargetControlID="txtWarrStart"></asp:CalendarExtender></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtContType2" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" style="text-align:right" Width="1%"></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">End </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtWarrEnd" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="ValidateDatesWarranty()" Width="90%"></asp:TextBox><asp:CalendarExtender ID="calWarrEnd" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtWarrEnd" TargetControlID="txtWarrEnd"></asp:CalendarExtender></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "></td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18% ; text-align:right;">Warranty Billing Frequency</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:DropDownList ID="ddlWarrantyFrequency" runat="server" AppendDataBoundItems="True" Width="93%"><asp:ListItem>--SELECT--</asp:ListItem><asp:ListItem>WEEKLY</asp:ListItem><asp:ListItem>MONTHLY</asp:ListItem><asp:ListItem>YEARLY</asp:ListItem></asp:DropDownList></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% ">&nbsp;</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">&nbsp;</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% ">&nbsp;</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% ">&nbsp;</td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18% ; text-align:right;">Warranty Billing Amount</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtWarrantyBillingAmount" runat="server" AutoCompleteType="Disabled" Height="16px" style="text-align:right" Width="90%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% ">&nbsp;</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">&nbsp;</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% ">&nbsp;</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% ">&nbsp;</td></tr><tr style="width:95%; display:none" ><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18% ; text-align:right; display:none">&nbsp;</td><td colspan="2"   style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% "><asp:CheckBox ID="ChkRequireInspection" runat="server" Text="Require Inspection" onchange="EnableDisableInspection()"  /></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%"></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15%"></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15%"></td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right;">Inspection Frequency </td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15%"><asp:DropDownList ID="ddlInspectionFrequency" runat="server" AppendDataBoundItems="True" Width="94%"><asp:ListItem>--SELECT--</asp:ListItem><asp:ListItem>WEEKLY</asp:ListItem><asp:ListItem>MNOTHLY</asp:ListItem><asp:ListItem>YEARLY</asp:ListItem></asp:DropDownList></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtContType3" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" style="text-align:right" Width="1%"></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12%"></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15%"></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15%"></td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18% ; text-align:right;">Inspection Start </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="TxtInspectionStart" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="ValidateDatesWarrnty()" Width="90%"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="TxtInspectionStart" TargetControlID="TxtInspectionStart"></asp:CalendarExtender></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtContType4" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" style="text-align:right" Width="1%"></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12% ; text-align:right;">End </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="TxtInspectionEnd" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="ValidateDatesWarranty()" Width="90%"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender18" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="TxtInspectionEnd" TargetControlID="TxtInspectionEnd"></asp:CalendarExtender></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "></td></tr></table><br /><table border="0" style="width:75%; margin:auto"><tr style="width:95%"><td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000;  background-color: #C0C0C0;">Renewal Details </td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right;">Previous Contract No. </td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:30%"><asp:TextBox ID="txtOContract" runat="server" Height="16px" Width="80%" Enabled="False"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12% "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "></td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Renewal Date </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:30% "><asp:TextBox ID="txtRenewalDate" runat="server" AutoCompleteType="Disabled" Height="16px" Width="80%" Enabled="False"></asp:TextBox><asp:CalendarExtender ID="cal" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtRenewalDate" TargetControlID="txtRenewalDate" Enabled="True" /></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12% "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtMainContractNo" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Enabled="False" Height="16px" Visible="False" Width="25%"></asp:TextBox></td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Renewal Contract No. </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:30% "><asp:TextBox ID="txtRenewed" runat="server" AutoCompleteType="Disabled" Height="16px" Width="80%" Enabled="False"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:12% "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtPrevContract" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Enabled="False" Height="16px" Visible="False" Width="25%"></asp:TextBox></td></tr></table><br /><table border="0" style="width:75%; margin:auto"><tr style="width:95%"><td colspan="2" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000;  background-color: #C0C0C0;">Billing Option </td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right;">Billing Frequency <asp:Label ID="Label2" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15%"><asp:DropDownList ID="ddlBillingFreq" runat="server"  AppendDataBoundItems="True" Width="40.5%"><asp:ListItem>--SELECT--</asp:ListItem></asp:DropDownList>&nbsp;<asp:ImageButton ID="btnClient4" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" /></td></tr><tr style="width:95%;display:none"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right;">&nbsp;</td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15%"><asp:TextBox ID="txtBillingAmount" runat="server" AutoCompleteType="Disabled" Height="16px"  style="text-align:right" Width="40%"></asp:TextBox></td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right; ">Computation Method </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% "><asp:Panel ID="Panel3" runat="server" BorderColor="#993300" BorderStyle="Solid" BorderWidth="1px" Height="19%" Width="40%" Font-Size="14px"><asp:RadioButtonList ID="rbtComputationMethod" runat="server" Font-Size="15px" ForeColor="Black" RepeatDirection="Horizontal" Width="95%"><asp:ListItem Text="Fixed Contract Amount" Value="Fixed"></asp:ListItem><asp:ListItem Text="Monthly" Value="Monthly"></asp:ListItem></asp:RadioButtonList></asp:Panel></td></tr></table><table border="0" style="width:75%; margin:auto; display:none""><tr style="width:95%"><td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 18%; text-align: right;">Retention (%)</td><td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; padding-left: 1%; width: 72%"><asp:TextBox ID="txtRetentionPerc" runat="server" AutoCompleteType="Disabled" Height="16px" onchange="getRetentionAmount()" style="text-align: right" Width="40%"></asp:TextBox>&nbsp;&nbsp;<asp:CheckBox ID="chkGenerateCreditNote" runat="server" ForeColor="#003366" Text="Generate Credit Note?" /></td><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right; ">Retention Amount</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72%;  "><asp:TextBox ID="txtRetentionValue" runat="server" AutoCompleteType="Disabled" Height="16px" style="text-align:right" Width="40%" BackColor="#CCCCCC"></asp:TextBox>&nbsp;&nbsp;&nbsp; </td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right; ">Retention Release Date</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72% "><asp:TextBox ID="txtRetentionReleaseDate" runat="server" AutoCompleteType="Disabled" Height="16px" Width="40%"></asp:TextBox><asp:CalendarExtender ID="txtRetentionReleaseDate_CalendarExtender" runat="server" Enabled="True" Format="MM/dd/yyyy" PopupButtonID="txtRetentionReleaseDate" TargetControlID="txtRetentionReleaseDate"></asp:CalendarExtender></td></tr><tr style="width: 95%"><td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 18%; text-align: right;">Project</td><td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; padding-left: 1%; width: 72%"><asp:DropDownList ID="ddlProjectCode" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="ProjectCode" DataValueField="ProjectCode" Height="25px" TabIndex="25" Width="40%"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td></tr>
        </table><br /><table border="0" style="width:75%; margin:auto"><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right;">Remarks</td><td   style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72%"><asp:TextBox ID="txtRemarks" runat="server" AutoCompleteType="Disabled" Height="40px" TextMode="MultiLine" Width="60%" Font-Names="calibri" Font-Size="15px" MaxLength="4000"></asp:TextBox>
            <asp:ImageButton ID="btnClient9" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" />
            </td></tr>
            
            <tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right;">Last Bill Date </td><td   style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72%"><asp:TextBox ID="txtLastBillDate" runat="server"   AutoCompleteType="Disabled" Enabled="False" Height="16px" Width="25%"></asp:TextBox></td></tr>
                 <tr style="width: 95%">
                     <td style="text-align: left; font-size: 15px; font-weight: bold; font-family: Calibri; text-align: right; padding-left: 1%; width: 18%; color: black;">Last Billed Amount</td>
                     <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; padding-left: 1%; width: 72%">
                         <asp:TextBox ID="txtLastBillAmount" runat="server" AutoCompleteType="Disabled" Enabled="False" Height="16px" Width="25%"></asp:TextBox>
                     </td>
                 </tr>
                 <tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right;">&nbsp;</td><td   style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:72%"></td></tr>
            
           
            
            <tr style="width:95%"><td colspan="2" style="text-align:right;"><asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="10%" OnClientClick="return DoValidation();"/><asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="10%" /></td></tr></table><table  style="width:75%; margin:auto"><tr style="width:95%"><td  style="width:6%;"><asp:TextBox ID="txtResponse" runat="server" Visible="False"  AutoCompleteType="Disabled" Height="16px" Width="1%"></asp:TextBox></td><td  style="width:6%;"><asp:TextBox ID="txtCancelCharges" runat="server" Visible="False"  AutoCompleteType="Disabled" Height="16px" Width="1%"></asp:TextBox></td><td  style="width:6%;"><asp:TextBox ID="txtAmtCompleted" runat="server" Visible="False"  AutoCompleteType="Disabled" Height="16px" Width="1%" style="text-align: right"></asp:TextBox></td><td  style="width:6%;"><asp:TextBox ID="txtQuoteUnit" runat="server"  AutoCompleteType="Disabled" Height="16px" Width="1%" Visible="False" MaxLength="10"></asp:TextBox></td><td  style="width:6%;"><asp:TextBox ID="txtAccCode" runat="server" Visible="False"  AutoCompleteType="Disabled" Height="16px" Width="1%"></asp:TextBox></td><td  style="width:6%;"><asp:TextBox ID="txtGstNo" runat="server" Visible="False"  AutoCompleteType="Disabled" Height="16px" Width="1%" MaxLength="20"></asp:TextBox></td><td  style="width:6%;"><asp:TextBox ID="txtQuotePrice" runat="server" Width="1%" AutoCompleteType="Disabled" Height="16px" Visible="False"></asp:TextBox></td><td  style="width:6%;"><asp:TextBox ID="txtTeamSelection" runat="server"  AutoCompleteType="Disabled" Height="16px" Width="55%" BorderStyle="None" ForeColor="#003300" style="text-align: right"></asp:TextBox></td><td  style="width:6%;"><asp:TextBox ID="txtMinDuration" runat="server"  AutoCompleteType="Disabled" Height="16px" Width="1%" Visible="False"></asp:TextBox></td><td  style="width:6%;"><asp:TextBox ID="txtNS" runat="server" Visible="False"  AutoCompleteType="Disabled" Height="16px" Width="1%" BackColor="#FFC6DA" Enabled="False"></asp:TextBox></td><td  style="width:6%;"><asp:TextBox ID="txtOriginalAccountId" runat="server"  AutoCompleteType="Disabled" Height="16px" Width="43%" BorderStyle="None" ForeColor="White" MaxLength="10"></asp:TextBox></td><td  style="width:6%;"><asp:TextBox ID="txtRs" runat="server" Visible="False"  AutoCompleteType="Cellular" BackColor="#FFC6DA" Height="16px"  Width="1%" Enabled="False"></asp:TextBox></td><td  style="width:6%;"><asp:TextBox ID="txtClient" runat="server" AutoCompleteType="Disabled" Height="16px" Width="36%" BorderStyle="None" ReadOnly="True" Visible="False"></asp:TextBox></td><td  style="width:6%;"><asp:TextBox ID="txtHourAmt" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="1%" style="text-align:right"></asp:TextBox></td><td style="width:6%"><asp:TextBox ID="txtCallAmt" runat="server" Visible="False"  AutoCompleteType="Disabled" Height="16px" Width="1%" style="text-align: right"></asp:TextBox></td><td style="width:6%"><asp:TextBox ID="txtUnitBal" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="1%" style="text-align: right" BackColor="#FFC6DA"></asp:TextBox></td><td style="width:6%"><asp:TextBox ID="txtCallNoActual" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="1%" style="text-align: right" BackColor="#FFC6DA"></asp:TextBox></td><td style="width:6%"><asp:TextBox ID="txtCallNo" runat="server" Visible="False"  AutoCompleteType="Disabled" Height="16px" Width="1%" style="text-align: right"></asp:TextBox></td><td style="width:6%"><asp:TextBox ID="txtUnitAmt" runat="server" Width="1%" Visible="False" AutoCompleteType="Disabled" Height="16px" style="text-align: right"></asp:TextBox></td><td  style="width:6%;"><asp:Panel ID="Panel1" runat="server" BorderColor="#993300" BorderStyle="Solid" BorderWidth="1px" Font-Size="Smaller" Height="19%" Visible="False" Width="99%"><asp:RadioButtonList ID="rbtnLSettle" runat="server" AppendDataBoundItems="True" Font-Size="12px" RepeatDirection="Horizontal" style="width:94%; "><asp:ListItem Text="Under Contract" Value="UC"></asp:ListItem><asp:ListItem Text="Offset Contract" Value="OC"></asp:ListItem><asp:ListItem Text="Contract Billing" Value="CB"></asp:ListItem></asp:RadioButtonList></asp:Panel></td><td style="width:6%"><asp:TextBox ID="txtRcnoDetail" runat="server" Visible="False" Width="1%"></asp:TextBox></td><td style="width:6%"><asp:TextBox ID="txtUnitNo" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" style="text-align: right" Visible="False"></asp:TextBox></td><td style="width:6%"><asp:TextBox ID="txtCompensateMax" runat="server" Width="1%" Visible="False" AutoCompleteType="Disabled" Height="16px" style="text-align:right"></asp:TextBox></td><td><asp:TextBox ID="txtCNNo" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" style="text-align:right" ForeColor="White" BorderStyle="None"></asp:TextBox></td><td style="width:6%"><asp:TextBox ID="txtCompensatePct" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="1%" BackColor="#FFC6DA" style="text-align: right"></asp:TextBox></td><td style="width:6%"><asp:TextBox ID="txtUnitNoActual" runat="server" Visible="False"  AutoCompleteType="Disabled" Height="16px" Width="1%" style="text-align: right" BackColor="#FFC6DA"></asp:TextBox></td><td style="width:6%"><asp:TextBox ID="txtGS" runat="server" Visible="False"  AutoCompleteType="Disabled" Height="16px" Width="1%" BackColor="#FFC6DA" Enabled="False"></asp:TextBox></td><td style="width:6%"><asp:TextBox ID="txtCallBal" runat="server" Visible="False"  AutoCompleteType="Disabled" Height="16px" Width="1%" style="text-align:right" BackColor="#FFC6DA"></asp:TextBox></td><td style="width:6%"><asp:TextBox ID="txtPerServVal" runat="server" AutoCompleteType="Disabled" Height="16px" style="text-align:right" Width="35%" Visible="False"></asp:TextBox></td><td style="width:6%"><asp:TextBox ID="txtHourNo" runat="server" AutoCompleteType="Disabled" Height="16px" style="text-align: right" Width="45%" Visible="False"></asp:TextBox></td><td  style="width:6%;"><asp:TextBox ID="txtAmtBalance" runat="server"  AutoCompleteType="Disabled" Height="16px" Width="1%" BackColor="#FFC6DA" Visible="False" style="text-align: right" ></asp:TextBox></td><td  style="width:6%;"><asp:TextBox runat="server" AutoCompleteType="Disabled" Height="16px" Width="20%" ID="txtNoofMonth" BorderStyle="None" ForeColor="White" BackColor="White" ClientIDMode="AutoID" Enabled="False"></asp:TextBox></td><td  style="width:6%;"><asp:TextBox ID="txtLimit" runat="server" AutoCompleteType="Disabled" Height="16px" Width="20%" BorderStyle="None" BackColor="White" ClientIDMode="AutoID" Enabled="False" ForeColor="White"></asp:TextBox></td><td  style="width:6%;"><asp:TextBox ID="txtTotContVal" runat="server" AutoCompleteType="Disabled" Height="16px"  Width="25%" BorderStyle="None" ForeColor="White" style="text-align: right"></asp:TextBox></td><td style="width: 6%"><asp:TextBox ID="txtHourBal" runat="server" AutoCompleteType="Disabled" BackColor="#FFC6DA" Height="16px" style="text-align: right" Visible="False" Width="90%"></asp:TextBox></td><td style="width:6%"><asp:TextBox ID="txtProspectId" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="10%"></asp:TextBox></td><td style="width:6%"><asp:TextBox ID="txtHourNoActual" runat="server"  Visible="False" AutoCompleteType="Disabled" Height="16px" Width="90%" style="text-align:right" BackColor="#FFC6DA"></asp:TextBox></td><td style="width:6%"><asp:TextBox ID="txtCNPeriod" runat="server" AutoCompleteType="Disabled" Height="16px" style="text-align: right" Width="1%" BorderStyle="None" ForeColor="White"></asp:TextBox></td><td  style="width:6%;"><asp:TextBox ID="txtRcnoCN" runat="server" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" Width="1%" ForeColor="White" style="text-align: right"></asp:TextBox></td></tr></table><div style="text-align:center"><asp:LinkButton ID="btnTop" runat="server" Font-Names="Calibri" Font-Bold="True" ForeColor="Brown">Go to Top</asp:LinkButton></div></div>  

   
</ContentTemplate></asp:TabPanel>

            <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Service Details"><HeaderTemplate><asp:Label ID="lblServiceDetailsCount" runat="server" Font-Size="11px" Text="Service Details"></asp:Label></HeaderTemplate><ContentTemplate><table border="0" style="width:75%; margin:auto"><tr style="width:95%"><td colspan="4" style="text-align:center;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';"><asp:Label ID="txtSvcMode" runat="server" CssClass="dummybutton" ></asp:Label></td></tr><tr style="width:95%"><td style="width:10%;text-align:left;"><asp:Button ID="btnSvcAdd" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="90%" OnClientClick="RefreshSubmit()" /></td><td style="width:10%;text-align:left;"><asp:Button ID="btnSvcEdit" runat="server" Font-Bold="True" Text="EDIT" Width="90%"  CssClass="roundbutton1" BackColor="#CFC6C0" OnClientClick="RefreshSubmit()" /></td><td style="width:10%;text-align:left;"><asp:Button ID="btnSvcDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="90%" OnClientClick = "Confirm()"/></td><td colspan="1" style="width:70%;text-align:right;vertical-align: middle"></td></tr></table><br /><table border="0" style="width:75%; margin:auto"><tr style="width:95%"><td style="width:100%;text-align:center; "><asp:Panel ID="Panel4" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1px" Height="100%" ScrollBars="Vertical" Width="100%"><asp:GridView ID="grvContractDetail" runat="server" AllowPaging="True" AllowSorting="True" OnRowDataBound = "OnRowDataBound2" OnSelectedIndexChanged = "OnSelectedIndexChanged2" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="ContractNo" DataSourceID="SqlDSContractDet" Font-Size="15px"  HorizontalAlign="Center" ForeColor="#333333" GridLines="Vertical"><AlternatingRowStyle BackColor="White" /><Columns><asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False"><ControlStyle Width="50px" /><ItemStyle Width="3%" Wrap="False" /></asp:CommandField><asp:BoundField DataField="LocationId" HeaderText="Location Id" SortExpression="LocationId" ><ControlStyle Width="13%" /><ItemStyle Width="13%" /></asp:BoundField><asp:BoundField DataField="Frequency" HeaderText="Frequency" SortExpression="Frequency" ><ItemStyle Width="10%" /></asp:BoundField><asp:BoundField DataField="ServiceID" HeaderText="ServiceID" ><ItemStyle Width="10%" /></asp:BoundField><asp:BoundField DataField="ServiceDesc" HeaderText="Service Description" SortExpression="ServiceDesc" ><ItemStyle Width="20%" /></asp:BoundField><asp:BoundField DataField="TargetDesc" HeaderText="Target Description" SortExpression="TargetDesc" ><ItemStyle Width="19%" /></asp:BoundField><asp:BoundField DataField="Value" HeaderText="Per Service Value" SortExpression="Value" ><ItemStyle Width="5%" /></asp:BoundField><asp:BoundField DataField="NoService" HeaderText="No. Service" SortExpression="NoService" ><ItemStyle Width="5%" /></asp:BoundField><asp:BoundField DataField="NoOfSvcInterval" HeaderText="No. of Svc Interval" SortExpression="NoOfSvcInterval" ><ItemStyle Width="10%" /></asp:BoundField><asp:BoundField DataField="NoOfInterval" HeaderText="No. of Interval" SortExpression="NoOfInterval" ><ItemStyle Width="5%" /></asp:BoundField><asp:BoundField DataField="NoServiceCompleted" HeaderText="No. of Service Completed" SortExpression="NoServiceCompleted" Visible="False" ><ItemStyle Width="5%" /></asp:BoundField><asp:BoundField DataField="Rcno" HeaderText="Rcno" InsertVisible="False" ReadOnly="True" SortExpression="Rcno" Visible="False" /><asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False"><EditItemTemplate><asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:BoundField DataField="ContractNo" HeaderText="ContractNo" SortExpression="ContractNo" Visible="False" /></Columns><EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" /><HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#507CD1" /><RowStyle BackColor="#EFF3FB" Font-Names="Calibri" /><SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle BackColor="#E9EBEF" /><SortedDescendingHeaderStyle BackColor="#4870BE" /></asp:GridView></asp:Panel></td></tr><tr><asp:SqlDataSource ID="SqlDSContractDet" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"><SelectParameters><asp:ControlParameter ControlID="lblContractNo" Name="@ContractNo" PropertyName="Text" /></SelectParameters></asp:SqlDataSource></tr></table><br /><table border="0" style="width:75%; margin:auto"><tr style="width:95%"><td colspan="6" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">Service Details </td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Contract No. </td><td colspan="2" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15%"><asp:Label ID="lblContractNo" runat="server" BackColor="#CCCCCC" Height="18px" MaxLength="100" ReadOnly="True" Width="100%" ForeColor="Black"></asp:Label></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% "></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "></td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Account ID </td><td colspan="2" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:Label ID="lblAccountID" runat="server" BackColor="#CCCCCC" Height="18px" MaxLength="100" ReadOnly="True" Width="100%" ForeColor="Black"></asp:Label></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% "></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "></td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Account Name </td><td colspan="2" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:Label ID="lblName" runat="server" BackColor="#CCCCCC" Height="32px" MaxLength="100" ReadOnly="True" Width="100%" ForeColor="Black"></asp:Label></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% "></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "></td></tr><tr><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Service Start </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% "><asp:TextBox ID="txtServStartSvc" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" ForeColor="Black" Height="16px" onchange="ValidateDatesService()" Width="98%" BorderStyle="None"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% "><asp:TextBox ID="txtServStartDaySvc" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" ForeColor="Black" Height="16px" Width="100%" BorderStyle="None"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">End </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtServEndSvc" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" ForeColor="Black" Height="16px" onchange="ValidateDatesService()" Width="90%" BorderStyle="None"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% "><asp:TextBox ID="txtServEndDaySvc" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" ForeColor="Black" Height="16px" Width="85%" BorderStyle="None"></asp:TextBox></td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Schedule Type <asp:Label ID="Label13" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td colspan="2" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:DropDownList ID="ddlScheduleType" runat="server"  AppendDataBoundItems="True" Height="20px" Width="100%"><asp:ListItem>--SELECT--</asp:ListItem ></asp:DropDownList></td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Preferred Time </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtServTimeIn" runat="server" AutoCompleteType="Disabled" Height="16px" Width="98%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% "><asp:TextBox ID="txtServTimeOut" runat="server" AutoCompleteType="Disabled" Height="16px"  style="padding-right:0%" Width="98%"></asp:TextBox><asp:MaskedEditExtender ID="txtServTimeIn_MaskedEditExtender" runat="server" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtServTimeIn"></asp:MaskedEditExtender><asp:MaskedEditExtender ID="txtServTimeOut_MaskedEditExtender" runat="server" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtServTimeOut"></asp:MaskedEditExtender></td></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtSelectedIndex" runat="server" AutoCompleteType="Disabled" Height="16px"  style="padding-right: 0%" Visible="False" Width="20%"></asp:TextBox><asp:MaskedEditExtender ID="txtSelectedIndex_MaskedEditExtender" runat="server" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtSelectedIndex"></asp:MaskedEditExtender></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% "></td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Allocated Time<asp:Label ID="Label63" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtAllocTime" runat="server" AutoCompleteType="Disabled" Height="16px" Width="98%"></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% ">Mins</td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">&nbsp;</td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Assign Team<asp:Label ID="Label72" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                &nbsp;&nbsp;</td><td colspan="2"  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% ">
                <asp:TextBox ID="txtTeam" runat="server" AutoCompleteType="Disabled" Height="16px" Width="100%" AutoPostBack="True"></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% "><asp:ImageButton ID="BtnTeam" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" /></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; ">&nbsp;</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% ">&nbsp;</td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Team In-Charge </td><td colspan="2" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtTeamIncharge" runat="server" AutoCompleteType="Disabled" Height="16px" Width="100%" MaxLength="25"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "></td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri;  text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Service By<asp:Label ID="Label73" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                &nbsp;&nbsp;</td><td colspan="2" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtServiceBy" runat="server" AutoCompleteType="Disabled" Height="16px" Width="100%" MaxLength="25"></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% "></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "></td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri;  text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Supervisor </td><td colspan="2" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtSupervisor" runat="server" AutoCompleteType="Disabled" Height="16px" Width="100%" MaxLength="25"></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15% "></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:12%; text-align:right; "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "></td></tr><tr style="width:95%"><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Service Instruction </td><td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtServInst" runat="server" AutoCompleteType="Disabled" Height="55px" TextMode="MultiLine" Width="95%" Font-Names="Calibri" Font-Size="15px" MaxLength="500" ></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:15% "><asp:TextBox ID="txtAccountIdSelection" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox></td></tr></table><br /><table border="0" style="width:75%; margin:auto"><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">Service Locations </td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% "><asp:UpdatePanel ID="updpnlLocation" runat="server" UpdateMode="Conditional"><ContentTemplate>
                <asp:GridView ID="grvServiceLocation" runat="server" AllowSorting="True" 
                                                          AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
                                                          GridLines="None" Height="17px" Font-Size="15px"
                                                          onpageindexchanging="grvServiceLocation_PageIndexChanging" 
                                                          onrowdatabound="grvServiceLocation_RowDataBound" 
                                                          OnRowDeleting="grvServiceLocation_RowDeleting" ShowFooter="True" Style="text-align: left" Width="60%"><Columns><asp:TemplateField HeaderText="Location Id"><ItemTemplate><asp:TextBox ID="ddlLocationIdGV" runat="server" Height="19px" width="130px">
                    </asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText=""><ItemTemplate><asp:ImageButton ID="BtnLocation" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" /><asp:ModalPopupExtender ID="imgBtnService_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlServiceClose" PopupControlID="pnlPopUpLocation" TargetControlID="BtnLocation"></asp:ModalPopupExtender></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Service Name"><ItemTemplate><asp:TextBox ID="txtServiceNameGV" runat="server" Enabled="false" Height="15px" ReadOnly="true" Width="250px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Service Address"><ItemTemplate><asp:TextBox ID="txtServiceAddressGV" runat="server" Enabled="false" Height="15px" ReadOnly="true" Width="300px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Zone"><ItemTemplate><asp:TextBox ID="txtZoneGV" runat="server" Enabled="false" Height="15px" ReadOnly="true" Width="80px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Location Code"><ItemTemplate><asp:TextBox ID="txtLocationGroupGV" runat="server" Enabled="false" Height="15px" ReadOnly="true" Width="85px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Branch"><ItemTemplate><asp:TextBox ID="txtBranchGV" runat="server" Enabled="false" Height="15px" ReadOnly="true" Width="85px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                            
                                                                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtContractNoGV" runat="server" Height="15px" Visible="false" Width="5px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtAccountIdGV" runat="server" Height="15px" Visible="false" Width="5px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoGV" runat="server" Visible="false" Height="15px" Width="15px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:CommandField ButtonType="Image"  DeleteImageUrl="~/Images/delete_icon_color.gif" ShowDeleteButton="False"><FooterStyle VerticalAlign="Top" /><ItemStyle Height="15px" /></asp:CommandField><asp:TemplateField><FooterStyle HorizontalAlign="Left" /><FooterTemplate><asp:Button ID="btnAddDetailLoc" runat="server" OnClick="btnAddDetailLoc_Click" Text="Add New Row" ValidationGroup="VGroup" Visible="false" /></FooterTemplate><ItemStyle ForeColor="#507CD1" /></asp:TemplateField></Columns><FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" /><RowStyle BackColor="#EFF3FB" Height="17px" Font-Names="Calibri" /><EditRowStyle BackColor="#2461BF" /><SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /><PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><AlternatingRowStyle BackColor="White" /></asp:GridView></ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="grvServiceLocation" EventName="SelectedIndexChanged" /></Triggers></asp:UpdatePanel>&nbsp;</td></tr></table><br /><asp:Panel ID="pnlPopUpLocation" runat="server" BackColor="White" Width="1250px" Height="600px" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left" ScrollBars="Auto" style="text-align:center; margin-left:auto; margin-right:auto;"><table style="width:1100px; margin-left:auto; margin-right:auto; "><tr><td style="text-align: center;"><h4 style="color: #000000">Select Location</h4></td><td style="width: 1%; text-align: right;"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr></table><div style="text-align: center; padding-left: 5px; padding-bottom: 5px;"><br /><asp:GridView ID="gvLocation" runat="server" DataSourceID="SqlDSCompanyLocation" OnRowDataBound = "OnRowDataBoundgLoc" OnSelectedIndexChanged = "OnSelectedIndexChangedgLoc" ForeColor="#333333" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="1100px"  Font-Size="15px" PageSize="20"><AlternatingRowStyle BackColor="White" ForeColor="#284775" /><Columns><asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False"><ControlStyle Width="45px" /><ItemStyle Width="45px" /></asp:CommandField><asp:BoundField DataField="LocationID" HeaderText="LocationID" SortExpression="LocationID" ><ControlStyle Width="70px" /><ItemStyle Width="70px" Wrap="False" /></asp:BoundField>
                    <asp:BoundField DataField="ContractGroup" HeaderText="Contract Group" />
                    <asp:BoundField DataField="ServiceName" HeaderText="Service Name" SortExpression="ServiceName" ><HeaderStyle HorizontalAlign="Left" /><ItemStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="Address1" HeaderText="Address" ><HeaderStyle HorizontalAlign="Left" /><ItemStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="AddStreet" HeaderText="Street" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField><asp:BoundField DataField="AddBuilding" HeaderText="Building" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField><asp:BoundField DataField="AddCity" HeaderText="City" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField><asp:BoundField DataField="LocateGRP" HeaderText="Zone" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField><asp:BoundField DataField="ContactPerson" ><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField><asp:BoundField DataField="ContactPerson2"  ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField ><asp:BoundField DataField="Contact1Position" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField ><asp:BoundField DataField="Contact2Position" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField ><asp:BoundField DataField="Telephone" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField ><asp:BoundField DataField="Telephone2" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField ><asp:BoundField DataField="Fax" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField ><asp:BoundField DataField="Contact2Fax" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField ><asp:BoundField DataField="Contact2Tel" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField ><asp:BoundField DataField="Contact2Tel2" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField ><asp:BoundField DataField="Mobile" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField ><asp:BoundField DataField="Contact2Mobile" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField ><asp:BoundField DataField="Email" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField ><asp:BoundField DataField="Contact2Email" ><ControlStyle CssClass="dummybutton" /><HeaderStyle CssClass="dummybutton" /><ItemStyle CssClass="dummybutton" /></asp:BoundField ><asp:BoundField DataField="ServiceLocationGroup" HeaderText="Location Code"><HeaderStyle HorizontalAlign="Left" /></asp:BoundField><asp:BoundField DataField="AddPostal" HeaderText="Postal" ><HeaderStyle HorizontalAlign="Left" /></asp:BoundField>
                    <asp:BoundField DataField="Location" HeaderText="Branch" />
                </Columns><EditRowStyle BackColor="#999999" /><FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Names="Calibri" HorizontalAlign="Left" /><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#E9E7E2" /><SortedAscendingHeaderStyle BackColor="#506C8C" /><SortedDescendingCellStyle BackColor="#FFFDF8" /><SortedDescendingHeaderStyle BackColor="#6F8DAE" /></asp:GridView><asp:SqlDataSource ID="SqlDSCompanyLocation" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"><SelectParameters><asp:ControlParameter ControlID="txtAccountIdSelection" Name="@Accountid " PropertyName="Text" /><asp:ControlParameter ControlID="txtContractGroupSelected" Name="@ContractGroup" PropertyName="Text" Type="String" /></SelectParameters></asp:SqlDataSource><asp:SqlDataSource ID="SqlDSPersonLocation" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"><SelectParameters><asp:ControlParameter ControlID="txtAccountIdSelection" Name="@Accountid" PropertyName="Text" /><asp:ControlParameter ControlID="txtContractGroupSelected" Name="@ContractGroup" PropertyName="Text" /></SelectParameters></asp:SqlDataSource></div></asp:Panel><asp:UpdatePanel ID="updnLocationDet1" runat="server" UpdateMode="Conditional"><ContentTemplate><table border="0" style="width:75%; margin:auto"><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right; ">Contact Person-1 </td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:.5%; width:30%; text-align:right; "><asp:TextBox ID="txtContactPerson" runat="server"  Width="98%" AutoCompleteType="Disabled" MaxLength="100"></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Position </td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:.5%; width:30%; text-align:right; "><asp:TextBox ID="txtPosition" runat="server" Height="16px" Width="98%" AutoCompleteType="Disabled" MaxLength="100"></asp:TextBox></td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right; ">Contact No. </td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:.5%; width:30%; text-align:right; "><asp:TextBox ID="txtTelephone" runat="server" Height="16px" Width="98%" AutoCompleteType="Disabled" MaxLength="50"></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Fax </td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:.5%; width:30%; text-align:right; "><asp:TextBox ID="txtFax" runat="server" Height="16px" Width="98%" AutoCompleteType="Disabled" MaxLength="50"></asp:TextBox></td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right; ">Contact No.-2 </td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:.5%; width:30%; text-align:right; "><asp:TextBox ID="txtTelephone2" runat="server" Height="16px" Width="98%" AutoCompleteType="Disabled" MaxLength="50"></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Mobile </td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:.5%; width:30%; text-align:right; "><asp:TextBox ID="txtConPerMobile" runat="server"  AutoCompleteType="Disabled" Width="98%" MaxLength="50"></asp:TextBox></td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right; ">Email </td><td colspan="3"  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:.5%; width:30%; text-align:right; "><asp:TextBox ID="txtEmail1" runat="server"  AutoCompleteType="Disabled" Width="99%" MaxLength="100"></asp:TextBox></td></tr></table></ContentTemplate></asp:UpdatePanel><br /><asp:UpdatePanel ID="updnLocationDet2" runat="server" UpdateMode="Conditional"><ContentTemplate><table border="0" style="width:75%; margin:auto"><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right; ">Contact Person-2 </td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:.5%; width:30%; text-align:right; "><asp:TextBox ID="txtContactPerson2" runat="server"  Width="98%" AutoCompleteType="Disabled" MaxLength="100"></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Position </td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:.5%; width:30%; text-align:right; "><asp:TextBox ID="txtPosition2" runat="server" Height="16px" Width="98%" AutoCompleteType="Disabled" MaxLength="100"></asp:TextBox></td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right; ">Contact No. </td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:.5%; width:30%; text-align:right; "><asp:TextBox ID="txtCP2Telephone" runat="server" Height="16px" Width="98%" AutoCompleteType="Disabled" MaxLength="50"></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Fax </td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:.5%; width:30%; text-align:right; "><asp:TextBox ID="txtFax2" runat="server"  AutoCompleteType="Disabled" Width="98%" MaxLength="50"></asp:TextBox></td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right; ">Contact No.-2 </td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:.5%; width:30%; text-align:right; "><asp:TextBox ID="txtCP2Telephone2" runat="server" Height="16px" Width="98%" AutoCompleteType="Disabled" MaxLength="50"></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">Mobile </td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:.5%; width:30%; text-align:right; "><asp:TextBox ID="txtConPerMobile2" runat="server" Height="16px" Width="98%" AutoCompleteType="Disabled" MaxLength="50"></asp:TextBox></td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:18%; text-align:right; ">Email </td><td colspan="3"  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:.5%; width:100%; text-align:right; "><asp:TextBox ID="txtEmail2" runat="server"  AutoCompleteType="Disabled" Width="99%" MaxLength="100"></asp:TextBox></td></tr></table></ContentTemplate></asp:UpdatePanel><br /><table border="0" style="width:75%; margin:auto"><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">Services </td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% "><asp:UpdatePanel ID="updpnlService" runat="server" UpdateMode="Conditional"><ContentTemplate><asp:GridView ID="grvServices" runat="server" AllowSorting="True" 
                                                          AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
                                                          GridLines="None" Height="17px" 
                                                          onpageindexchanging="grvServices_PageIndexChanging" 
                                                          onrowdatabound="grvServices_RowDataBound" 
                                                          OnRowDeleting="grvServices_RowDeleting" ShowFooter="True" Style="text-align: left" Width="50%"><Columns><asp:TemplateField HeaderText="Service Id"><ItemTemplate><asp:DropDownList ID="ddlServiceIdGV" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataTextField="Descrip1" DataValueField="Descrip1" Height="19px" onselectedindexchanged="ddlServiceIdGV_SelectedIndexChanged" width="140px"><asp:ListItem Text="--SELECT--" Value="--Select--" /></asp:DropDownList></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Service Description"><ItemTemplate><asp:TextBox ID="txtServiceDescriptionGV" runat="server" Enabled="false" Height="15px" ReadOnly="true" Width="150px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Service Frequency"><ItemTemplate><asp:DropDownList ID="ddlServiceFrequencyGV" runat="server" AppendDataBoundItems="True" Enabled="true"   AutoPostBack="True"  DataTextField="Frequency" DataValueField="Frequency" Height="19px"  Width="185px" onselectedindexchanged="ddlServiceFrequencyGV_SelectedIndexChanged"><asp:ListItem Text="--SELECT--" Value="--Select--" /></asp:DropDownList></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Price Per Service"><ItemTemplate><asp:TextBox ID="txtPricePerServiceGV" runat="server" Enabled="true" Visible="true"  Height="15px" ReadOnly="false" Width="75px" Style="text-align:right" ></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Total Svcs."><ItemTemplate><asp:TextBox ID="txtTotalServicesGV" runat="server" Enabled="false" Height="15px" Style="text-align:center" ReadOnly="true" Width="70px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Completed Svcs."><ItemTemplate><asp:TextBox ID="txtCompletedServicesGV" runat="server" Enabled="false" Height="15px" ReadOnly="true" Width="75px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Bal. Svcs."><ItemTemplate><asp:TextBox ID="txtBalanceServicesGV" runat="server" Enabled="false" Height="15px" ReadOnly="true" Width="60px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText=""><ItemTemplate><asp:DropDownList ID="txtUOMGV" runat="server" AppendDataBoundItems="True" Visible="false"  Enabled="true" Height="19px" ReadOnly="false" Width="75px" Style="text-align:right" ><asp:ListItem Text="--SELECT--" Value="--Select--" /></asp:DropDownList></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtAreaGV" runat="server" Enabled="true" Height="15px" Visible="false"  ReadOnly="True" Width="60px" Style="text-align:right" ></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtNoofIntervalGV" runat="server" Height="15px" Visible="false" Width="2px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtNoofSvcIntervalGV" runat="server" Height="15px" Visible="false" Width="2px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtContractNoGV" runat="server" Height="15px" Visible="false" Width="2px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtAccountIdGV" runat="server" Height="15px" Visible="false" Width="2px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoGV" runat="server" Visible="false" Height="15px" Width="2px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtMonthByWhichMonthGV" runat="server" Height="15px" Visible="false" Width="2px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtDOWByWhichWeekGV" runat="server" Height="15px" Visible="false" Width="2px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:CommandField ButtonType="Image"  DeleteImageUrl="~/Images/delete_icon_color.gif" ShowDeleteButton="False"><FooterStyle VerticalAlign="Top" /><ItemStyle Height="15px" /></asp:CommandField><asp:TemplateField><FooterStyle HorizontalAlign="Left" /><FooterTemplate><asp:Button ID="btnAddDetailServices" runat="server" OnClick="btnAddDetailServices_Click" Text="Add" ValidationGroup="VGroup" Visible="false" /></FooterTemplate><ItemStyle ForeColor="#507CD1" /></asp:TemplateField></Columns><FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" /><RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" /><SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /><PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" /></asp:GridView></ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="grvServices" EventName="SelectedIndexChanged" /></Triggers></asp:UpdatePanel>&nbsp;</td></tr></table><br /><table border="0" style="width:75%; margin:auto"><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">Service Schedules </td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% "><asp:UpdatePanel ID="updpnlFreq" runat="server"><ContentTemplate><asp:GridView ID="grvFreqDetails" runat="server"
                                                AllowSorting="True" AutoGenerateColumns="False" CellPadding="1" 
                                                GridLines="None" CssClass="table_head_bdr "
                                                ShowFooter="True" Style="text-align: left" Width="45%" Height="20px"
                                                onrowdatabound="grvFreqDetails_RowDataBound" 
                                                OnRowDeleting="grvFreqDetails_RowDeleting"  ><Columns><asp:TemplateField HeaderText="Seq. No."><ItemTemplate><asp:TextBox ID="txtSeqNoGV" runat="server" Enabled="false" Height="15px" ReadOnly="true" Style="text-align: center" Width="60px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Freq MTD"><ItemTemplate><asp:DropDownList ID="ddlFreqMTDGV" runat="server" AutoPostBack="True" 
                                                                DataTextField="FreMTD" DataValueField="FreMTD" Height="20px" AppendDataBoundItems="True"
                                                                 width="100px" onselectedindexchanged="ddlFreqMTDGV_SelectedIndexChanged"><asp:ListItem Text="--SELECT--" Value="-1" /><asp:ListItem>DAY</asp:ListItem><asp:ListItem>DOW</asp:ListItem><asp:ListItem>MONTH</asp:ListItem><asp:ListItem>DATE</asp:ListItem></asp:DropDownList></ItemTemplate></asp:TemplateField><asp:TemplateField  HeaderText="Month No."><ItemTemplate><asp:TextBox ID="txtMonthNoGV" runat="server" 
                                                                Enabled="false" Height="15px" AutoPostBack="True" Style="text-align:center" OnTextChanged="txtMonthNoGV_TextChanged" Width="70px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField  HeaderText="Day No."><ItemTemplate><asp:TextBox ID="txtDayNoGV" runat="server" 
                                                                Enabled="false" Height="15px" AutoPostBack="true" Style="text-align:center"  OnTextChanged="txtDayNoGV_TextChanged"  Width="65px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField  HeaderText="Week No."><ItemTemplate><asp:TextBox ID="txtWeekNoGV" runat="server" AutoPostBack="True" Style="text-align:center"  OnTextChanged="txtWeekNoGV_TextChanged"
                                                                Enabled="false" Height="15px"   Width="75px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Day of Week"><ItemTemplate><asp:DropDownList ID="ddlWeekDOWGV" runat="server" AutoPostBack="True" 
                                                                DataTextField="WeekDOW" DataValueField="WeekDOW" Height="20px" onselectedindexchanged="ddlWeekDOWGV_SelectedIndexChanged" AppendDataBoundItems="True"
                                                                 width="115px" ><asp:ListItem Text="--SELECT--" Value="-1" /><asp:ListItem>MONDAY</asp:ListItem><asp:ListItem>TUESDAY</asp:ListItem><asp:ListItem>WEDNESDAY</asp:ListItem><asp:ListItem>THURSDAY</asp:ListItem><asp:ListItem>FRIDAY</asp:ListItem><asp:ListItem>SATURDAY</asp:ListItem><asp:ListItem style="color:red">SUNDAY</asp:ListItem></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Location" Visible="False"><ItemTemplate><asp:DropDownList ID="ddlLocationGV" runat="server" AutoPostBack="True" Visible="false" 
                                                                DataTextField="Location" DataValueField="Location" Height="20px" AppendDataBoundItems="True"
                                                                 width="150px" ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Branch ID" Visible="False"><ItemTemplate><asp:DropDownList ID="ddlBranchIDGV" runat="server" AutoPostBack="True" Visible="false"
                                                                DataTextField="BranchID" DataValueField="BranchID" Height="20px" AppendDataBoundItems="True"
                                                                 width="150px" ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtContractNoGVF" runat="server" Height="15px" Visible="false" Width="5px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtSourceSQLIDGVF" runat="server" Visible="false" Height="15px" Width="15px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoGVF"  runat="server" Visible="false" Height="15px" Width="15px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:CommandField ButtonType="Image"  Visible="False"    
                                                        DeleteImageUrl="~/Images/delete_icon_color.gif" 
                                                        ShowDeleteButton="True"><ItemStyle Height="15px" /></asp:CommandField><asp:TemplateField><FooterStyle HorizontalAlign="Left" /><FooterTemplate><asp:Button ID="btnAddDetail" runat="server" OnClick="btnAddDetail_Click" Text="Add New Row" ValidationGroup="VGroup" Visible="false" /></FooterTemplate><ItemStyle ForeColor="#507CD1" /></asp:TemplateField></Columns><FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" 
                                                    Height="5px" /><RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" /><SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /><PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" /></asp:GridView></ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="grvFreqDetails" EventName="SelectedIndexChanged" /></Triggers></asp:UpdatePanel></td></tr></table><br /><table border="0" style="width:75%; margin:auto"><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">Target Pest </td></tr><tr style="width:95%"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% "><asp:UpdatePanel ID="updpnlTarget" runat="server"><ContentTemplate><asp:GridView ID="grvTargetDetails" runat="server" AllowSorting="True" 
                                                          AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
                                                          GridLines="None" Height="17px" 
                                                          onpageindexchanging="grvTargetDetails_PageIndexChanging" 
                                                          onrowdatabound="grvTargetDetails_RowDataBound" 
                                                          OnRowDeleting="grvTargetDetails_RowDeleting" ShowFooter="True" Style="text-align: left" Width="50%"><Columns><asp:TemplateField HeaderText="Target Description"><ItemTemplate><asp:DropDownList ID="ddlTargetDescGV" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataTextField="Descrip1" DataValueField="Descrip1" Height="19px" onselectedindexchanged="ddlTargetDescGV_SelectedIndexChanged" width="250px"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Target ID"><ItemTemplate><asp:TextBox ID="txtTargtIdGV" runat="server" Enabled="false" Height="15px"  Style="text-align:center" ReadOnly="true" Width="75px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtContractNoGV" runat="server" Height="15px" Visible="false" Width="5px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtServiceIDGV" runat="server" Height="15px" Visible="false" Width="5px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtSourceSQLIDGV" runat="server" Visible="false" Height="15px" Width="15px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoGV" runat="server" Visible="false" Height="15px" Width="15px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:CommandField ButtonType="Image"  DeleteImageUrl="~/Images/delete_icon_color.gif" ShowDeleteButton="True" ><FooterStyle VerticalAlign="Top" /><ItemStyle Height="15px" /></asp:CommandField><asp:TemplateField><FooterStyle HorizontalAlign="Left" /><FooterTemplate><asp:Button ID="btnAddDetail" runat="server" OnClick="btnAddDetail_Click" Text="Add New Row" ValidationGroup="VGroup" Visible="false" /></FooterTemplate><ItemStyle ForeColor="#507CD1" /></asp:TemplateField></Columns><FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" /><RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" /><SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /><PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" /></asp:GridView></ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="grvTargetDetails" EventName="SelectedIndexChanged" /></Triggers></asp:UpdatePanel>&nbsp;</td></tr><caption><br /><tr style="width:95%"><td style="text-align:right;"><asp:Button ID="btnSvcSave" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="return DoValidationSvc()" Text="SAVE" Width="10%"></asp:Button><asp:Button ID="btnSvcSaveSchedule" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="SAVE & SCHEDULE" Width="16%" OnClientClick="return DoValidationSvc()" ></asp:Button><asp:Button ID="btnSvcCancel" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="10%"></asp:Button></td></tr></caption></table><asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"><ContentTemplate><table border="0" style="width:75%; margin:auto"><tr><td  style="font-size:14px;font-weight:bold; width:5%;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtLocationId" runat="server" AutoCompleteType="Disabled" BackColor="White" BorderStyle="None" ForeColor="White" Height="16px" style="text-align:right" Width="25%"></asp:TextBox>&nbsp;<asp:TextBox ID="txtSalesman" runat="server" AutoCompleteType="Disabled" BackColor="White" BorderStyle="None" ForeColor="White" Height="16px" style="text-align:right" Width="25%"></asp:TextBox><asp:TextBox ID="txtContractGroupSelected" runat="server" AutoCompleteType="Disabled" BackColor="White" Height="16px" Visible="False" Width="35%"></asp:TextBox></td><td  style="font-size:14px;font-weight:bold; width:5%;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtFrequency" runat="server" AutoCompleteType="Disabled" Height="16px" Width="35%" BorderStyle="None" ForeColor="White"></asp:TextBox>
                <asp:TextBox ID="txtBranch" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" Width="35%"></asp:TextBox>
                </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:5% "><asp:TextBox ID="txtFrequencyDesc" runat="server" AutoCompleteType="Disabled" BackColor="White" Height="16px" Width="35%" Visible="False"></asp:TextBox></td><td style="font-size:14px;font-weight:bold;width:3%;font-family:'Calibri';color:black;text-align:left; "><asp:TextBox ID="txtNoofInterval" runat="server" AutoCompleteType="Disabled" Height="16px" Width="55%" BackColor="White" ReadOnly="True" style="text-align:right" Visible="False"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:5% "><asp:TextBox ID="txtNoofSvcInterval" runat="server" AutoCompleteType="Disabled" Height="16px" Width="35%" style="text-align:right" Visible="False"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:5% "><asp:Label ID="lblInterval" runat="server" Text=" " Visible="false" ForeColor="Black"></asp:Label><asp:TextBox ID="txtZone" runat="server" AutoCompleteType="Disabled" BackColor="White" Height="16px" ReadOnly="True" style="text-align:right" Visible="False" Width="25%"></asp:TextBox><asp:TextBox ID="txtCreatedOn" runat="server" BorderStyle="None" ForeColor="White" Width="50%"></asp:TextBox></td><td><asp:TextBox ID="txtServiceName" runat="server" AutoCompleteType="Disabled" BackColor="White" Height="16px" ReadOnly="True" style="text-align:right" Width="55%" BorderStyle="None" ForeColor="White"></asp:TextBox></td><td><asp:TextBox ID="txtServiceAddress" runat="server" AutoCompleteType="Disabled" BackColor="White" Height="16px" ReadOnly="True" style="text-align:right" Visible="False" Width="55%"></asp:TextBox></td><td><asp:TextBox ID="txtUOM" runat="server" AutoCompleteType="Disabled" BackColor="White" Height="16px" ReadOnly="True" style="text-align:right" Visible="False" Width="55%"></asp:TextBox></td><td><asp:TextBox ID="txtArea" runat="server" AutoCompleteType="Disabled" BackColor="White" Height="16px" ReadOnly="True" style="text-align:right" Visible="False" Width="55%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:5% "><asp:TextBox ID="txtServiceId" runat="server" AutoCompleteType="Disabled" Height="16px" Width="35%" BorderStyle="None" ForeColor="White"></asp:TextBox></td><td  style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtServiceDesc" runat="server" AutoCompleteType="Disabled" BackColor="White" Height="16px" Width="35%" Visible="False"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:5% "><asp:TextBox ID="txtNoService" runat="server" AutoCompleteType="Disabled" Height="16px" Width="35%" BackColor="White" ReadOnly="True" style="text-align:right" Visible="False"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:5% "><asp:TextBox ID="txtValuePerService" runat="server" AutoCompleteType="Disabled" Height="16px" Width="35%" style="text-align:right" Visible="False" ></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:5% "><asp:TextBox ID="txtServiceNotes" runat="server" AutoCompleteType="Disabled" Height="16px" Width="35%" Visible="False"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:5% "><asp:TextBox ID="txtRecordAdded" runat="server" AutoCompleteType="Disabled" Height="16px" Width="40%" Visible="False"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:5% "><asp:TextBox ID="txtMonthByWhichMonth" runat="server" AutoCompleteType="Disabled" Height="16px" style="text-align:right" Width="35%" Visible="False"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:5% "><asp:TextBox ID="txtDuplicateTarget" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:5% "><asp:TextBox ID="txtDOWByWhichWeek" runat="server" AutoCompleteType="Disabled" Height="16px" style="text-align:left" Width="40%" Visible="False"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:5% "><asp:TextBox ID="txtValue" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="40%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:5% "><asp:TextBox ID="txtOrigCode" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="20%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:5% "><asp:TextBox ID="txtSourceSQLID" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox>&#160;<asp:TextBox ID="txtServiceDescriptionCons" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:5% "><asp:TextBox ID="txtTargetDesc" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" ForeColor="White"></asp:TextBox><asp:TextBox ID="txtTargetId" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" ForeColor="White"></asp:TextBox><asp:TextBox ID="txtIsPopup" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox><asp:TextBox ID="txtSearchPopup" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; color:black; text-align:left; padding-left:1%; width:5% "><asp:TextBox ID="txtRecordDeleted" runat="server" AutoCompleteType="Disabled" Height="16px" Width="20%" Visible="False"></asp:TextBox>&nbsp;
                                                              <asp:TextBox ID="txtLocationGroup" runat="server" AutoCompleteType="Disabled" BackColor="White" Height="16px" ReadOnly="True" style="text-align:right" Visible="False" Width="25%"></asp:TextBox></td><td><asp:TextBox ID="txtDuplicateFreq" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox></td></tr></table></contenttemplate></asp:UpdatePanel></ContentTemplate>

            </asp:TabPanel>

       <asp:TabPanel ID="TabPanel6" runat="server" HeaderText="Items Issued"><HeaderTemplate><asp:Label ID="lblItemsIssuedCount" runat="server" Font-Size="11px" Text="Items Issued"></asp:Label></HeaderTemplate><ContentTemplate><div style="text-align:center"><h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">Items Issued</h3><table style="width:100%;text-align:center;"><tr><td colspan="2" style="text-align:left;"><asp:Button ID="Button1" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" Visible="False" /><asp:Button ID="Button2" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="False" /><asp:Button ID="Button3" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()" Visible="False"/><asp:Button ID="Button4" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="False" /><asp:Button ID="Button5" runat="server" Visible="False"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CLOSE" Width="100px" /></td></tr><tr><td colspan="2"><br /></td></tr><tr class="Centered"><td colspan="2">
           <asp:GridView ID="gvItemsIssued" runat="server" DataSourceID="sqlDSItemsIssued" OnRowDataBound = "OnRowDataBoundgNotes" OnSelectedIndexChanged = "OnSelectedIndexChangedgNotes" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="ItemId" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True" GridLines="Vertical" ForeColor="#333333" ><AlternatingRowStyle BackColor="White" /><Columns><asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False" ><ControlStyle Width="150px" /><ItemStyle Width="150px" /></asp:CommandField><asp:BoundField DataField="ItemID" HeaderText="Item ID" SortExpression="ItemID"><ControlStyle Width="150px" /><HeaderStyle Font-Size="12pt" HorizontalAlign="Left" /><ItemStyle Width="150px" HorizontalAlign="Left" /></asp:BoundField><asp:BoundField DataField="ItemName" HeaderText="Item Name" SortExpression="ItemName"><ControlStyle Width="300px" />
               <HeaderStyle Font-Size="12pt" HorizontalAlign="Left" /><ItemStyle Width="300px" HorizontalAlign="Left" /></asp:BoundField>
               <asp:BoundField DataField="Qty" HeaderText="Qty" />
               <asp:BoundField DataField="UnitMS" HeaderText="Unit MS" />
               </Columns><EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" /><HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#507CD1" /><RowStyle BackColor="#EFF3FB" Font-Names="Calibri" /><SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle BackColor="#E9EBEF" /><SortedDescendingHeaderStyle BackColor="#4870BE" /></asp:GridView></td></tr><tr><td colspan="2"><br /></td></tr><tr><td class="CellFormat" style="text-align:right;padding-right:5px;"></td><td class="CellTextBox"><asp:Label ID="lbItemsIssued" runat="server" MaxLength="100" Height="18px" Width="40%" ReadOnly="True" BackColor="#CCCCCC" Visible="False"></asp:Label></td></tr><tr><td class="CellFormat" style="text-align:right;padding-right:5px;">
           &nbsp;</td><td class="CellTextBox">&nbsp;</td></tr><tr><td class="CellFormat" style="text-align:right;padding-right:5px;">&nbsp;</td><td class="CellTextBox">&nbsp;</td></tr><tr><td colspan="2" style="text-align:right"><asp:Button ID="Button6" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" Visible="False"/><asp:Button ID="Button7" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" Visible="False" /></td></tr></table><asp:SqlDataSource ID="sqlDSItemsIssued" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource></div><asp:TextBox ID="TextBox6" runat="server" CssClass="dummybutton"></asp:TextBox><asp:TextBox ID="TextBox7" runat="server" CssClass="dummybutton"></asp:TextBox></ContentTemplate>

       </asp:TabPanel>


     <asp:TabPanel ID="TabPanel7" runat="server" HeaderText="Rate Schedule"><HeaderTemplate><asp:Label ID="lblRatescheduleCount" runat="server" Font-Size="11px" Text="Schedule of Rate"></asp:Label></HeaderTemplate><ContentTemplate><div style="text-align:center"><h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">Rate Schedule</h3><table style="width:100%;text-align:center;"><tr><td colspan="2" style="text-align:left;"><asp:Button ID="btnAddRateSchedule" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" /><asp:Button ID="btnEditRateSchedule" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" /><asp:Button ID="btnDeleteRateSchedule" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()"/><asp:Button ID="Button11" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="False" />
         <asp:Button ID="Button12" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CLOSE" Width="100px" /></td></tr><tr><td colspan="2"><br /></td></tr><tr class="Centered"><td colspan="2">
           <asp:GridView ID="gvRateSchedule" runat="server" DataSourceID="sqlDSRateSchedule" OnRowDataBound = "OnRowDataBoundgRateSchedule" OnSelectedIndexChanged = "OnSelectedIndexChangedgRateSchedule" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True" GridLines="Vertical" ForeColor="#333333" ><AlternatingRowStyle BackColor="White" /><Columns><asp:CommandField ShowHeader="True" ShowSelectButton="True" ><ControlStyle Width="150px" /><ItemStyle Width="150px" /></asp:CommandField><asp:BoundField DataField="TargetID" HeaderText="Target ID" SortExpression="TargetID"><ControlStyle Width="150px" /><HeaderStyle Font-Size="12pt" HorizontalAlign="Left" /><ItemStyle Width="150px" HorizontalAlign="Left" /></asp:BoundField>
               <asp:BoundField DataField="Descrip1" HeaderText="Description" SortExpression="Descrip1"><ControlStyle Width="300px" />
               <HeaderStyle Font-Size="12pt" HorizontalAlign="Left" /><ItemStyle Width="300px" HorizontalAlign="Left" /></asp:BoundField>
               <asp:BoundField DataField="PricePerService" HeaderText="Price Per Service" />
               <asp:BoundField DataField="DiscountPercent" HeaderText="Discount %" />
               <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                       <EditItemTemplate>
                       <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label></EditItemTemplate><ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                           </ItemTemplate>

                   </asp:TemplateField>
               <asp:BoundField DataField="FinalPricePerService" HeaderText="Final Price/Service" />
               </Columns><EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" /><HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#507CD1" /><RowStyle BackColor="#EFF3FB" Font-Names="Calibri" /><SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle BackColor="#E9EBEF" /><SortedDescendingHeaderStyle BackColor="#4870BE" /></asp:GridView></td></tr><tr><td colspan="2"><br /></td></tr><tr><td class="CellFormat" style="text-align:right;padding-right:5px;"></td><td class="CellTextBox"><asp:Label ID="lblScheduleRate1" runat="server" MaxLength="100" Height="18px" Width="40%" ReadOnly="True" BackColor="#CCCCCC" Visible="False"></asp:Label></td></tr>
         <tr><td class="CellFormat" style="text-align:right;padding-right:5px;">
             Target ID</td><td class="CellTextBox">
                 <asp:DropDownList ID="ddlTagetIDRateSchedule" runat="server" AppendDataBoundItems="True" Height="20px" Width="30%" AutoPostBack="True">
                     <asp:ListItem>--SELECT--</asp:ListItem>
                 </asp:DropDownList>
             </td></tr>
         <tr><td class="CellFormat" style="text-align:right;padding-right:5px;">Target Description</td><td class="CellTextBox">
             <asp:TextBox ID="txtTargetDescriptionRateSchedule" runat="server" Height="16px" MaxLength="50" style="text-align:left;" Width="30%"></asp:TextBox>
             </td></tr>
         <tr>
             <td class="CellFormat" style="text-align:right;padding-right:5px;">Price Per Service</td>
             <td class="CellTextBox">
                 <asp:TextBox ID="txtPricePerServiceRateSchedule" runat="server" Height="16px" MaxLength="50" style="text-align:right;" Width="30%" onchange="CalculateRateSchedule();"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td class="CellFormat" style="text-align:right;padding-right:5px;">Discount Percenrage</td>
             <td class="CellTextBox">
                 <asp:TextBox ID="txtDiscountPercentageRateSchedule" runat="server" Height="16px" MaxLength="50" style="text-align:right;" Width="30%" onchange="CalculateRateSchedule();"></asp:TextBox>
             </td>
         </tr>
         <tr>
             <td class="CellFormat" style="text-align:right;padding-right:5px;">Final Price Per Service</td>
             <td class="CellTextBox">
                 <asp:TextBox ID="txtFinalPricePerServiceRateSchedule" runat="server" Height="16px" MaxLength="50" style="text-align:right;" Width="30%"></asp:TextBox>
             </td>
         </tr>
         <tr><td colspan="2" style="text-align:right"><asp:Button ID="btnSaveRateSchedule" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/><asp:Button ID="btnCancelRateSchedule" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table><asp:SqlDataSource ID="sqlDSRateSchedule" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource></div><asp:TextBox ID="txtRateScheduleMode" runat="server" CssClass="dummybutton"></asp:TextBox><asp:TextBox ID="txtRateScheduleRcNo" runat="server" CssClass="dummybutton"></asp:TextBox></ContentTemplate>

       </asp:TabPanel>


                
                  <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Commission"><HeaderTemplate>Commission</HeaderTemplate><ContentTemplate><table class="Centered" style="padding-top:5px;width:80%"><tr style="vertical-align: middle"><td style="width:100%;text-align:left;"><asp:Button ID="btnCommEdit" runat="server" Font-Bold="True" Text="EDIT" Width="90px"  CssClass="roundbutton1" BackColor="#CFC6C0" /></td></tr></table><table class="Centered" style="padding-top:5px;width:60%"><tr><td class="CellFormat">Contract No </td><td colspan="1" class="CellTextBox"><asp:Label ID="lblContractNo2" runat="server" MaxLength="100" Height="16px" Width="50%" ReadOnly="True" BackColor="#CCCCCC"></asp:Label></td></tr><tr><td class="CellFormat">Account ID </td><td colspan="1" class="CellTextBox"><asp:Label ID="lblAccountID2" runat="server" MaxLength="100" Height="16px" Width="50%" ReadOnly="True" BackColor="#CCCCCC"></asp:Label></td></tr><tr><td class="CellFormat">Account Name </td><td colspan="1" class="CellTextBox"><asp:Label ID="lblAccountName" runat="server" MaxLength="100" Height="32px" Width="50%" ReadOnly="True" BackColor="#CCCCCC"></asp:Label></td></tr><tr><td colspan="2"><br /></td></tr><tr><td colspan="2" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:0%; background-color: #CCCCCC;">Salesman Commission</td></tr><tr><td class="CellFormat">Commission %</td><td colspan="1" class="CellTextBox"><asp:TextBox ID="txtSalesCommPercent" runat="server" MaxLength="50" Height="16px" Width="50%" AutoPostBack="True" style="text-align:right;"></asp:TextBox></td></tr><tr><td class="CellFormat">Commission Amount</td><td colspan="1" class="CellTextBox"><asp:TextBox ID="txtSalesCommAmt" runat="server" MaxLength="50" Height="16px" Width="50%" style="text-align:right;"></asp:TextBox></td></tr><tr><td colspan="2"><br /></td></tr><tr><td colspan="2" style="width:100%;font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:0%; background-color: #CCCCCC;">Technician Referral Commission</td></tr><tr><td class="CellFormat">Commission %</td><td colspan="1" class="CellTextBox"><asp:TextBox ID="txtTechCommPercent" runat="server" MaxLength="50" Height="16px" Width="50%" AutoPostBack="True" style="text-align:right;"></asp:TextBox></td></tr><tr><td class="CellFormat">Commission Amount</td><td colspan="1" class="CellTextBox"><asp:TextBox ID="txtTechCommAmt" runat="server" MaxLength="50" Height="16px" Width="50%" style="text-align:right;"></asp:TextBox></td></tr><tr><td colspan="2"><br /></td></tr><tr><td colspan="2" style="text-align:right;"><asp:Button ID="btnCommSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="return DoServiceValidation()"/><asp:Button ID="btnCommCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /><asp:Label ID="txtCommMode" runat="server" CssClass="dummybutton"></asp:Label></td></tr></table></ContentTemplate>

                  </asp:TabPanel>

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
              
      <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="File Upload"><HeaderTemplate> <asp:Label ID="lblFileUploadCount" runat="server" Font-Size="11px" Text="File Upload"></asp:Label></HeaderTemplate><ContentTemplate><table style="width:100%;height:1000px"><tr><td><table style="text-align:center;width:100%;padding-top:10px;" class="centered"><tr><td class="CellFormat">Contract No.</td><td class="CellTextBox"><asp:Label ID="lblContractNo1" runat="server" BackColor="#CCCCCC" Height="18px" MaxLength="100" ReadOnly="True" Width="50%"></asp:Label></td></tr><tr><td class="CellFormat">Account ID </td><td class="CellTextBox"><asp:Label ID="lblAccountID1" runat="server" BackColor="#CCCCCC" Height="18px" MaxLength="100" ReadOnly="True" Width="50%"></asp:Label></td></tr><tr><td class="CellFormat">Contract Name </td><td class="CellTextBox"><asp:Label ID="lblName1" runat="server" MaxLength="100" Height="18px" Width="50%" ReadOnly="True" BackColor="#CCCCCC"></asp:Label></td></tr><tr><td><asp:TextBox ID="txtDeleteUploadedFile" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" MaxLength="50" style="text-align: left;" Visible="False" Width="5%"></asp:TextBox><br /></td></tr><asp:TextBox ID="txtFileLink" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" style="text-align: left;" Visible="False" Width="5%"></asp:TextBox><caption><br /></caption>
          </td></tr><tr><td class="CellFormat">Select File to Upload (Max. 20MB)</td><td colspan="1" class="CellTextBox" style="text-align:center"><asp:FileUpload ID="FileUpload1" runat="server" Width="100%" CssClass="Centered" /></td></tr><tr><td class="CellFormat">Document Type<asp:Label ID="Label64" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
          </td><td colspan="1" class="CellTextBox" style="text-align:left">
          <asp:DropDownList ID="ddlDocumentType" runat="server" AppendDataBoundItems="True" Height="20px" Width="45%">
              <asp:ListItem>--SELECT--</asp:ListItem>
          </asp:DropDownList>
          </td></tr>
          <tr>
              <td class="CellFormat">Description 
                  <asp:Label ID="Label65" runat="server" Font-Size="14px" ForeColor="Red" Height="20px" Text="*"></asp:Label>
              </td>
              <td class="CellTextBox" colspan="1" style="text-align:left">
                  <asp:TextBox ID="txtFileDescription" runat="server" Width="70%"></asp:TextBox>
              </td>
          </tr>
          <tr><td colspan="2" class="centered"><asp:Button ID="btnUpload" runat="server" Text="Upload" OnClientClick="currentdatetime()" /></td></tr><tr><td><br /></td></tr><tr><td colspan="2">
              
              <asp:GridView ID="gvUpload" runat="server" AutoGenerateColumns="False" EmptyDataText = "No files uploaded" Width="90%" CssClass="Centered" DataSourceID="SqlDSUpload">
                  <Columns>
                      <asp:BoundField DataField="FileName" HeaderText="File Name" />
          <asp:BoundField DataField="DocumentType" HeaderText="Document Type" />
          <asp:BoundField DataField="FileDescription" HeaderText="File Description" />
                  <asp:BoundField DataField="CreatedOn" DataFormatString="{0:d}" HeaderText="UploadDate" SortExpression="CreatedOn" />
                      <asp:BoundField DataField="CreatedBy" HeaderText="UploadedBy" SortExpression="CreatedBy" />
                      <asp:TemplateField><ItemTemplate>
                          <asp:LinkButton ID = "lnkPreview" Text = "Preview" CommandArgument = '<%# Eval("FileNameLink")%>' runat = "server" OnClick = "PreviewFile" /></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate>
                              <asp:LinkButton ID="lnkDownload" Text = "Download" CommandArgument = '<%# Eval("FileNameLink")%>' runat="server" OnClick = "DownloadFile"></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate>
                              <asp:LinkButton ID = "lnkDelete" Text = "Delete" CommandArgument = '<%# Eval("FileNameLink")%>' runat = "server" OnClick = "DeleteFile" /></ItemTemplate></asp:TemplateField>
                       <%--<asp:TemplateField><ItemTemplate><asp:LinkButton ID="lnkEmail" Text = "Email" CommandArgument = '<%# Eval("FileNameLink")%>' runat="server" OnClick = "EmailFile"></asp:LinkButton></ItemTemplate></asp:TemplateField>--%>
                  <asp:TemplateField>
                                                                         <ItemTemplate>
                                                                             <a href="Email.aspx?Type=ContractFileUpload" target="_blank">
                                                                             <asp:LinkButton ID="lnkEmail1" runat="server" CommandArgument='<%# Eval("FileNameLink")%>' OnClick="EmailFile" OnClientClick="openInNewTab();" Text="Email"></asp:LinkButton>
                                                                             </a><%--<a href ="#" title ="Update" onclick="return jsPopup('Email.aspx?Type=FileUpload&FileName=<%#Eval("FileNameLink")%> '); "> Email</a>--%>
                                                                         </ItemTemplate>
                                                                     </asp:TemplateField>
                        </Columns><FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" /><RowStyle BackColor="#E4E4E4" ForeColor="#8C4510" Font-Names="Calibri" /><SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" /><SortedAscendingCellStyle BackColor="#E4E4E4" /><SortedAscendingHeaderStyle BackColor="#000066" /><SortedDescendingCellStyle BackColor="#E4E4E4" /><SortedDescendingHeaderStyle BackColor="#000066" /></asp:GridView><asp:SqlDataSource ID="SqlDSUpload" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" 
              SelectCommand="SELECT * FROM tblfileupload where fileref = 'aa'"></asp:SqlDataSource></td></tr></table></td></tr><tr style="height:800px;width:100%"><td style="height:100%;width:100%"><br />
                  <iframe runat="server" id ="iframeid" style="width:80%;height:80%" ></iframe></td></tr></table>

                                                                                                                       </ContentTemplate></asp:TabPanel>
       <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="Notes"><HeaderTemplate><asp:Label ID="lblNotesCount" runat="server" Font-Size="11px" Text="Notes"></asp:Label></HeaderTemplate><ContentTemplate><div style="text-align:center"><h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">Notes</h3><table style="width:100%;text-align:center;"><tr><td colspan="2" style="text-align:left;"><asp:Button ID="btnAddNotesMaster" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" /><asp:Button ID="btnEditNotesMaster" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" /><asp:Button ID="btnDeleteNotesMaster" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()"/><asp:Button ID="btn" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="False" /><asp:Button ID="btnQuitNotesMaster" runat="server" Visible="False"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CLOSE" Width="100px" /></td></tr><tr><td colspan="2"><br /></td></tr>
           <tr class="Centered"><td colspan="2"><asp:GridView ID="gvNotesMaster" runat="server" DataSourceID="SqlDSNotesMaster" OnRowDataBound = "OnRowDataBoundgNotes" OnSelectedIndexChanged = "OnSelectedIndexChangedgNotes" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True" GridLines="Vertical" ForeColor="#333333" >
               <AlternatingRowStyle BackColor="White" /><Columns><asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False" >
                   <ControlStyle Width="150px" />
                   <ItemStyle Width="150px" /></asp:CommandField>
                   <asp:BoundField DataField="StaffID" HeaderText="StaffID" SortExpression="StaffID"><ControlStyle Width="150px" /><HeaderStyle Font-Size="12pt" /><ItemStyle Width="150px" HorizontalAlign="Left" /></asp:BoundField>
                   <asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes"><ControlStyle Width="300px" /><HeaderStyle Font-Size="12pt" /><ItemStyle Width="300px" HorizontalAlign="Left" /></asp:BoundField>
                   <asp:BoundField DataField="CreatedBy" HeaderText="EntryStaff" SortExpression="CreatedBy" />
                   <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                       <EditItemTemplate>
                       <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label></EditItemTemplate><ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                           </ItemTemplate>

                   </asp:TemplateField>
                   <asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EntryDate" SortExpression="CreatedOn" /><asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" />
                   <asp:BoundField DataField="LastModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EditedOn" SortExpression="LastModifiedOn" /></Columns><EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" /><HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#507CD1" /><RowStyle BackColor="#EFF3FB" Font-Names="Calibri" /><SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle BackColor="#E9EBEF" /><SortedDescendingHeaderStyle BackColor="#4870BE" />

                                                </asp:GridView></td></tr><tr><td colspan="2"><br /></td></tr><tr><td class="CellFormat" style="text-align:right;padding-right:5px;"></td><td class="CellTextBox"><asp:Label ID="lblNotesKeyField" runat="server" MaxLength="100" Height="18px" Width="40%" ReadOnly="True" BackColor="#CCCCCC" Visible="False"></asp:Label></td></tr><tr><td class="CellFormat" style="text-align:right;padding-right:5px;">StaffID </td><td class="CellTextBox"><asp:Label ID="lblNotesStaffID" runat="server" MaxLength="100" Height="18px" Width="40%" ReadOnly="True" BackColor="#CCCCCC"></asp:Label></td></tr><tr><td class="CellFormat" style="text-align:right;padding-right:5px;">Notes<asp:Label ID="Label23" runat="server" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td><td class="CellTextBox"><asp:TextBox ID="txtNotes" runat="server" MaxLength="50" Height="60px" Width="80%" TextMode="MultiLine"></asp:TextBox></td></tr><tr><td colspan="2" style="text-align:right"><asp:Button ID="btnSaveNotesMaster" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/><asp:Button ID="btnCancelNotesMaster" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table><asp:SqlDataSource ID="SqlDSNotesMaster" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource></div><asp:TextBox ID="txtNotesRcNo" runat="server" CssClass="dummybutton"></asp:TextBox><asp:TextBox ID="txtNotesMode" runat="server" CssClass="dummybutton"></asp:TextBox></ContentTemplate>

       </asp:TabPanel>
                   
              </asp:TabContainer>
                    </td>
                </tr>

                         
                              <tr>
                                  <td  style="width:10%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                      <asp:TextBox ID="TextBox3" runat="server" BorderStyle="None" ForeColor="#003366"></asp:TextBox></td>
                                  <td colspan ="2" style="text-align:left; padding-right:2%">
                                     
                                      </td>

                                 
                                 
                                   <td style="width:10%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;"> 
                                        <asp:Button ID="btnChStatus" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" /></td>
                                  <td style="text-align:left;width:8%">
                                      <asp:Button ID="btnDummy" runat="server" CssClass="roundbutton" Font-Bold="True" Text=" " Width="24px" BackColor="White" BorderStyle="None" />
                                      &nbsp;&nbsp;&nbsp;<asp:Button ID="btnDummyT" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" />
                                      &nbsp;<asp:Button ID="btnDummyC" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" />
                                      &nbsp;<asp:Button ID="btnDummyClient" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
                                      <asp:Button ID="btnDummyInvoice" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
                                  </td>

                                   <td style="width:10%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                      <asp:TextBox ID="txtPrintBody" runat="server" AutoCompleteType="Disabled" Height="20px" Visible="False" Width="1px"></asp:TextBox>
                                  </td>
                                  <td style="text-align:left;width:10%">
                                      <asp:Button ID="btnLoc" runat="server" BackColor="#CFC6C0" CssClass="roundbutton" Font-Bold="True" Text="LOC/BR [O]" Visible="False" Width="44px" />
                                  </td>
                                  <td style="text-align: left">
                                      <asp:Button ID="btnDummyClient2" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
                                      </td>
                                   <td style="text-align: left">
                                       <asp:Button ID="btnDummyClient3" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" /> 
                                  </td>
                            
                              </tr>

    </table>
         </div>


     


    <asp:Panel ID="pnlSearch" runat="server" BackColor="White" Width="85%" Height="95%" BorderColor="#003366" BorderWidth="1" Visible="true" ScrollBars="Auto" HorizontalAlign="Center">
              
                     <table  style="width:90%; border:thin;   padding-left:3px; margin-left:auto; margin-right:auto;  " >

                            <tr>
                               <td style="font-size:18px; font-weight:bold;font-family:Calibri;color:black;text-align:center;"  colspan="6">Search</td>
                           </tr>
                    
                       <tr>
                               <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Contract No.
                               </td>
                              <td colspan="3" style="text-align:left;padding-left:5px; width:20%">   
                                   <asp:TextBox ID="txtSearchID" runat="server" MaxLength="50" Height="16px" Width="90%"></asp:TextBox>
                            </td> 
                                 <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Contact Type</td>
                              <td  style="width:35%">     
                                  <asp:DropDownList ID="ddlSearchContactType" runat="server" AppendDataBoundItems="true" Height="25px" Width="92%">
                                      <asp:ListItem Text="--SELECT--" Value="-1" />
                                      <asp:ListItem>Company</asp:ListItem>
                                      <asp:ListItem>Person</asp:ListItem>
                                  </asp:DropDownList>
                            </td>                              
                           </tr>

                         <tr>
                               <td style="width:10%; font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Client Name</td>
                              <td colspan="3" style="text-align:left;padding-left:5px;width:20%;">  
                                    <asp:TextBox ID="txtSearchCompany" runat="server" MaxLength="100" Height="16px" Width="90%"></asp:TextBox>
                            </td>
                                 <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Account Id</td>
                              <td>
                                  <asp:TextBox ID="txtSearchCustCode" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                               </td>


                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Address </td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:20%;">
                                       <asp:TextBox ID="txtSearchAddress" runat="server" Height="16px" MaxLength="100" Width="90%"></asp:TextBox>
                                   </td>
                                          <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Status</td>
                              <td  style="width:35%">
                                  <asp:DropDownList ID="ddlSearchStatus" runat="server" Width="92%">
                                      <asp:ListItem Value="O">O - Open</asp:ListItem>
                                      <asp:ListItem Value="C">C - Completed</asp:ListItem>
                                      <asp:ListItem Value="E">E - Early Completion</asp:ListItem>
                                      <asp:ListItem Value="H">H - On Hold</asp:ListItem>
                                      <asp:ListItem Value="R">R - Renewed</asp:ListItem>
                                      <asp:ListItem Value="T">T - Terminated</asp:ListItem>
                                      <asp:ListItem Value="V">V - Void</asp:ListItem>
                                      <asp:ListItem Value="X">X - Cancelled by Company</asp:ListItem>
                                  </asp:DropDownList>
                                   </td>
                               </tr>


                               <tr>
                                   <td style="width:15%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Contact Person </td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:20%">
                                       <asp:TextBox ID="txtSearchContact" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                                   </td>
                                          <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Renewal Status</td>
                              <td>
                                  <asp:DropDownList ID="ddlSearchRenewalStatus" runat="server" Width="92%">
                                      <asp:ListItem Value="O">O - Open</asp:ListItem>
                                      <asp:ListItem Value="E">C - Completed</asp:ListItem>
                                      <asp:ListItem Value="H">H - On Hold</asp:ListItem>
                                      <asp:ListItem Value="R">R - Renewed</asp:ListItem>
                                      <asp:ListItem Value="V">V - Void</asp:ListItem>
                                  </asp:DropDownList>
                                   </td>

                               </tr>
                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Postal </td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:20%">
                                       <asp:TextBox ID="txtSearchPostal" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                                   </td>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Generate Schedule Status</td>
                                   <td colspan="1">
                                       <asp:DropDownList ID="ddlSearchGenerateScheduleStatus" runat="server" Width="92%">
                                           <asp:ListItem>--SELECT--</asp:ListItem>
                                           <asp:ListItem Value="O">O - Open</asp:ListItem>
                                           <asp:ListItem Value="P">P - Schedule is Generated</asp:ListItem>
                                       </asp:DropDownList>
                                   </td>
                               </tr>

                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Salesman </td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:20%">
                                       <asp:DropDownList ID="ddlSearchSalesman" runat="server" AppendDataBoundItems="true" DataValueField="StaffId" Height="25px" Width="91%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                                   </td>
                                   <td style="width:12%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Contact No</td>
                                   <td colspan="1" style="width:15%">
                                       <asp:TextBox ID="txtSearchContactNo" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                                   </td>
                               </tr>


                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Scheduler </td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:20%">
                                       <asp:DropDownList ID="ddlSearchScheduler" runat="server" AppendDataBoundItems="true" DataValueField="StaffId" Height="25px" Width="91%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                                   </td>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">In-charge Id</td>
                                   <td colspan="1">
                                       <asp:DropDownList ID="ddlSearchInChargeId" runat="server" AppendDataBoundItems="true" Height="25px" Width="92%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                                   </td>
                               </tr>

                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Our Reference</td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:20%">
                                       <asp:TextBox ID="txtSearchOurRef" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                                   </td>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Contract Group</td>
                                   <td colspan="1">
                                       <asp:DropDownList ID="ddlSearchContractGroup" runat="server" AppendDataBoundItems="true" Height="25px" Width="92%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                                   </td>
                               </tr>
                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Your Reference</td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:20%">
                                       <asp:TextBox ID="txtSearchYourRef" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                                   </td>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Company Group</td>
                                   <td colspan="1">
                                       <asp:DropDownList ID="ddlSearchCompanyGroup" runat="server" AppendDataBoundItems="true" Height="25px" Width="92%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                                   </td>
                               </tr>

                   <tr>
                      <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Contract Date From</td>
                       <td  style="text-align:left;padding-left:5px;width:15%">
                             <asp:TextBox ID="txtSearchContractDateFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchContractDateFrom" TargetControlID="txtSearchContractDateFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:35px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:15%; padding-right:12px; ">
                       <asp:TextBox ID="txtSearchContractDateTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchContractDateTo" TargetControlID="txtSearchContractDateTo"/>
                     </td>
                           

                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Location Group</td>
                                   <td colspan="1">
                                       <asp:DropDownList ID="ddlSearchLocationGroup" runat="server" AppendDataBoundItems="true" Height="25px" Width="92%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                       </td>
                               </tr>

                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Start Date From</td>
                       <td  style="text-align:left;padding-left:5px;width:15%">
                             <asp:TextBox ID="txtSearchStartDateFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchStartDateFrom" TargetControlID="txtSearchStartDateFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:35px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:15%; padding-right:12px; ">
                       <asp:TextBox ID="txtSearchStartDateTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchStartDateTo" TargetControlID="txtSearchStartDateTo"/>
                     </td>

                      <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                      <td colspan="1">&nbsp;</td>
                      </tr>

                      <tr>
                        <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">End Date From</td>
                       <td  style="text-align:left;padding-left:5px;width:15%">
                             <asp:TextBox ID="txtSearchEndDateFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchEndDateFrom" TargetControlID="txtSearchEndDateFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:35px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:15%; padding-right:12px; ">
                       <asp:TextBox ID="txtSearchEndDateTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender9" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchEndDateTo" TargetControlID="txtSearchEndDateTo"/>
                     </td>

                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                                   <td colspan="1">&nbsp;</td>
                               </tr>


                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Service Start From</td>
                       <td  style="text-align:left;padding-left:5px;width:15%">
                             <asp:TextBox ID="txtSearchServiceStartFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender10" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchServiceStartFrom" TargetControlID="txtSearchServiceStartFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:35px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:15%; padding-right:12px; ">
                       <asp:TextBox ID="txtSearchServiceStartTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender11" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchServiceStartTo" TargetControlID="txtSearchServiceStartTo"/>
                     </td>

                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                                   <td colspan="1">&nbsp;</td>
                               </tr>


                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Service End From</td>
                       <td  style="text-align:left;padding-left:5px;width:15%">
                             <asp:TextBox ID="txtSearchServiceEndFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender12" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchServiceEndFrom" TargetControlID="txtSearchServiceEndFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:35px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:15%; padding-right:12px; ">
                       <asp:TextBox ID="txtSearchServiceEndTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender13" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchServiceEndTo" TargetControlID="txtSearchServiceEndTo"/>
                     </td>

                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                                   <td colspan="1">&nbsp;</td>
                               </tr>


                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Actual End From</td>
                       <td  style="text-align:left;padding-left:5px;width:15%">
                             <asp:TextBox ID="txtSearchActualEndFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender14" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchActualEndFrom" TargetControlID="txtSearchActualEndFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:35px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:15%; padding-right:12px; ">
                       <asp:TextBox ID="txtSearchActualEndTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender15" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchActualEndTo" TargetControlID="txtSearchActualEndTo"/>
                     </td>

                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                                   <td colspan="1">&nbsp;</td>
                               </tr>


                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;" >
                                       Created Date From</td>
                       <td  style="text-align:left;padding-left:5px;width:15%">
                             <asp:TextBox ID="txtSearchEntryDateFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender16" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchEntryDateFrom" TargetControlID="txtSearchEntryDateFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:35px; padding-right:35px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:15%; padding-right:12px; ">
                       <asp:TextBox ID="txtSearchEntryDateTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender17" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchEntryDateTo" TargetControlID="txtSearchEntryDateTo"/>
                     </td>

                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                                   <td colspan="1">&nbsp;</td>
                               </tr>

                               <tr>
                                   <td style="width: 10%; font-size: 15px; font-weight: bold; font-family: Calibri; color: black; text-align: left;">Created By</td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;">
                                       <asp:DropDownList ID="ddlSearchCreatedBy" runat="server" AppendDataBoundItems="true" DataValueField="StaffId" Height="25px" Width="91%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                                   </td>
                                   <td style="width: 10%; font-size: 15px; font-weight: bold; font-family: Calibri; color: black; text-align: left;">&nbsp;</td>
                                   <td colspan="1">&nbsp;</td>
                               </tr>
                               <tr>
                                   <td style="width: 10%; font-size: 15px; font-weight: bold; font-family: Calibri; color: black; text-align: left;">Edited Date From</td>
                                   <td style="text-align: left; padding-left: 5px; width: 15%">
                                       <asp:TextBox ID="txtSearchModifiedDateFrom" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" Width="80%"></asp:TextBox>
                                       <asp:CalendarExtender ID="txtSearchModifiedDateFrom_CalendarExtender" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchModifiedDateFrom" TargetControlID="txtSearchModifiedDateFrom" />
                                   </td>
                                   <td style="text-align: left; padding-left: 35px; padding-right: 35px; font-size: 15px; font-weight: bold; font-family: Calibri; color: black; width: 2%">To</td>
                                   <td style="text-align: left; width: 15%; padding-right: 12px;">
                                       <asp:TextBox ID="txtSearchModifiedDateTo" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" Width="80%"></asp:TextBox>
                                       <asp:CalendarExtender ID="txtSearchModifiedDateTo_CalendarExtender" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchModifiedDateTo" TargetControlID="txtSearchModifiedDateTo" />
                                   </td>
                                   <td style="width: 10%; font-size: 15px; font-weight: bold; font-family: Calibri; color: black; text-align: left;">&nbsp;</td>
                                   <td colspan="1">&nbsp;</td>
                               </tr>
                               <tr>
                                   <td style="width: 10%; font-size: 15px; font-weight: bold; font-family: Calibri; color: black; text-align: left;">Edited By</td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;">
                                       <asp:DropDownList ID="ddlSearchEditedBy" runat="server" AppendDataBoundItems="true" DataValueField="StaffId" Height="25px" Width="91%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                                   </td>
                                   <td style="width: 10%; font-size: 15px; font-weight: bold; font-family: Calibri; color: black; text-align: left;">&nbsp;</td>
                                   <td colspan="1">&nbsp;</td>
                               </tr>

                               <tr>
                                   <td colspan="4">
                                       <br />
                                   </td>
                               </tr>
                               <tr>
                                   <td></td>
                                   <td colspan="3" style="text-align:center">
                                       <asp:Button ID="btnSearch" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="Search" Width="100px" />
                                  
                                       <asp:Button ID="btnClose" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="Cancel" Width="100px" />
                                   </td>
                                      <td></td>
                              <td></td>
                               </tr>
                             </tr>

        </table>
           </asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnClose" PopupControlID="pnlSearch" TargetControlID="btnFilter" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>



    
         <%-- Change Status--%>


         
    <asp:Panel ID="PnlChSt" runat="server" BackColor="White" Width="45%" Height="58%" BorderColor="#003366" BorderWidth="1" Visible="true" ScrollBars="Auto">
              <br />
               <table border="0" style="width:100%;padding-left:5px; margin-left:auto; margin-right:auto;">
                 <tr>
                     <td style="font-size:18px; font-weight:bold;font-family:Calibri;color:black;text-align:center;"  colspan="2">Change Status</td>
                 </tr>
                   <tr>
                       <td colspan="2" style="font-size:18px; font-weight:bold;font-family:Calibri;color:black;text-align:center;">
                           <asp:Label ID="lblMessageStatus" runat="server"></asp:Label>
                       </td>
                   </tr>
                   <tr>

                        
                       <td colspan="2" style="font-size:18px; font-weight:bold;font-family:Calibri;text-align:left;">
                           <asp:TextBox ID="txtAlertStatus" runat="server" style="font-size:18px; font-weight:bold;font-family:Calibri;text-align:center;" AutoCompleteType="Disabled" Height="16px" Width="99%" BackColor="Red" BorderStyle="None" ForeColor="White" ></asp:TextBox>
                       </td>
                   </tr>
              <tr>
                              
                        <td style="width:21%; font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:right;" >Contract No.</td>
                         
                                 <td style="text-align:left;padding-left:3px;width:70%">  
                                     <asp:TextBox ID="txtContractNoChSt" runat="server" AutoCompleteType="Disabled" Height="16px" Width="50%"></asp:TextBox>
                            </td>    
                  </tr>
                   <tr>                          
                           <td style="width:21%; font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:right;">Customer Name</td>
                        <td style="text-align:left;padding-left:3px;width:70%">
                                         <asp:TextBox ID="txtCustomerNameChSt" runat="server" AutoCompleteType="Disabled" Height="16px" Width="50%"></asp:TextBox>
                        </td>
                           </tr>


                          <tr>
                              <td style="width:21%; font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:right;">Status
                                  <asp:Label ID="Label30" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                              </td>
                              <td  style="text-align:left;padding-left:3px;width:70%">
                                  <asp:DropDownList ID="ddlStatusChSt" runat="server" AutoPostBack="True" Width="50%">
                                      <asp:ListItem>--SELECT--</asp:ListItem>
                                  </asp:DropDownList>
                              </td>
                   </tr>


                          <tr>
                              <td style="width:20%; font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:right;" >
                                 <asp:Label ID="lblActualEnd" runat="server" Text="Actual End"></asp:Label>
                                   &nbsp;<asp:Label ID="Label29" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                                  
                              </td>
                              <td  style="text-align:left;padding-left:5px;width:80%">
                                  <asp:TextBox ID="txtActualEndChSt" runat="server" AutoCompleteType="Disabled" Height="16px" Width="49%" AutoPostBack="True"></asp:TextBox>
                                      
                                  <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtActualEndChSt" TargetControlID="txtActualEndChSt" />
                              </td>
                         </tr>
                  
                    <tr>
                               <td style="width:20%; font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:right;">
                                   <asp:Label ID="lblTermType" runat="server" Text="Termination Type"></asp:Label>
                               </td>
                              <td  style="text-align:left;padding-left:5px;width:10%">  
                                    <asp:TextBox ID="txtTerminationTypeChSt" runat="server" AutoCompleteType="Disabled" Height="16px" Width="50%"></asp:TextBox>
                            </td>
                             </tr>
                          <tr>
                              <td style="width:20%; font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:right;">
                                  <asp:Label ID="lblTermCode" runat="server" Text="Termination Code"></asp:Label>
                                  <asp:Label ID="Label62" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                              </td>
                              <td style="text-align:left;padding-left:5px;width:10%">
                                  <asp:DropDownList ID="ddlTerminationCode" runat="server" AppendDataBoundItems="true" DataSourceID="SqlDSTerminationCode" DataTextField="TC" DataValueField="TC" Width="99%">
                                      <asp:ListItem>--SELECT--</asp:ListItem>
                                  </asp:DropDownList>
                              </td>
                   </tr>
                          <tr>
                              <td style="width:20%; font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:right; vertical-align:top;"><asp:Label ID="lblCommetsChSt" runat="server" Text="Comments"></asp:Label>
                                  <asp:Label ID="Label66" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                              </td>
                              <td  style="text-align:left;padding-left:5px;width:10%">
                                  <asp:TextBox ID="txtCommentChSt" runat="server" Font-Names="calibri" Font-Size="15px" Height="80px" MaxLength="500" TextMode="MultiLine" Width="98%"></asp:TextBox>
                              </td>
                   </tr>
                          <tr style="display:none">
                                  
   
                               <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:right;" >
                                   <asp:DropDownList ID="ddlRenewalStatus" runat="server" Visible="False" Width="20%">
                                       <asp:ListItem Text="--SELECT--" Value="-1" />
                                       <asp:ListItem Value="O">O - Open</asp:ListItem>
                                       <asp:ListItem Value="R">R - Renewal</asp:ListItem>
                                       <asp:ListItem Value="N">N - No Follow up Needed</asp:ListItem>
                                       <asp:ListItem Value="D">D - Client Declined Renewal</asp:ListItem>
                                   </asp:DropDownList>
                                   </td>
                              <td  style="text-align:left;padding-left:5px;width:70%">
                                  <asp:TextBox ID="txtOnHoldDateChSt" runat="server" AutoCompleteType="Disabled" Height="16px" Width="50%"></asp:TextBox>
                               </td>
                             </tr>
                         
                          <tr>
                              <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:right;">
                                  <asp:Label ID="lblOnHoldCode" runat="server" Text="OnHold Code"></asp:Label>
                                  <asp:Label ID="Label67" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                              </td>
                              <td  style="text-align:left;padding-left:5px;width:70%">
                                  <asp:DropDownList ID="ddlOnHoldCodeChSt" runat="server" AppendDataBoundItems="true" Width="99%" AutoPostBack="True">
                                      <asp:ListItem>--SELECT--</asp:ListItem>
                                  </asp:DropDownList>
                              </td>
                   </tr>
                   <tr style="display:none">
                       <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;"><asp:Label ID="lblOnHoldDescription" runat="server" Text="OnHold Description"></asp:Label>
                       </td>
                       <td  style="text-align:left;padding-left:5px;width:70%">
                           <asp:TextBox ID="txtOnHoldDscriptionChSt" runat="server" AutoCompleteType="Disabled" Height="16px" Width="50%"></asp:TextBox>
                       </td>
                   </tr>
                   <tr>
                       <td style="width:12%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;"><asp:Label ID="lblOnHoldComments" runat="server" Text="OnHold Comments"></asp:Label>
                           <asp:Label ID="Label68" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                       </td>
                       <td  style="text-align:left;padding-left:5px;width:70%">
                           <asp:TextBox ID="txtOnHoldCommentsChSt" runat="server" Font-Names="calibri" Font-Size="15px" Height="80px" MaxLength="500" TextMode="MultiLine" Width="98%"></asp:TextBox>
                       </td>
                   </tr>
                  
                         
                          <tr>
                                  
   
                               <td >
                                   <asp:TextBox ID="txtChangeStatus" runat="server" AutoCompleteType="Disabled" BackColor="White" Height="16px" Width="35%" BorderStyle="None" ForeColor="White"></asp:TextBox>
                                   <asp:TextBox ID="txtFirstDate" runat="server" AutoCompleteType="Disabled" Height="16px" Width="30%" CssClass="dummybutton"></asp:TextBox>
                                    <asp:TextBox ID="txtLastDate" runat="server" AutoCompleteType="Disabled" Height="16px" Width="30%" CssClass="dummybutton"></asp:TextBox>
                                   <br />
                               </td>
                             </tr>
                         
                     <tr><td colspan="2" style="text-align:center" >
                            
                                 <asp:Button ID="BtnChSt" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Save" Width="100px" OnClientClick="ConfirmChSt()"/>
                                 <asp:Button ID="BtnChStClose" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Cancel" Width="100px" />
                             </td>
                         </tr>
                       <%--  <tr>
                             <td colspan="1" style="text-align:center; width:20%" >
                             &nbsp;</td>
                             <td style="text-align:center; width:40%">
                                 <asp:Button ID="BtnChSt" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="Save" Width="10%" OnClientClick="ConfirmChSt()"/>
                                 <asp:Button ID="BtnChStClose" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="Cancel" Width="10%" />
                             </td>
                             <td colspan="1" style="text-align:center; width:20%" >
                             </td>
                             
                         </tr>--%>

        </table>
           </asp:Panel>

     <asp:ModalPopupExtender ID="ModalPopupExtender5" runat="server"  CancelControlID="" PopupControlID="pnlChSt" TargetControlID="btnChStatus" BackgroundCssClass="modalBackground"  ></asp:ModalPopupExtender>


           <asp:Panel ID="pnlStatusSearch" runat="server" BackColor="White" Width="40%" Height="40%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          <br /><br />    <table style="font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:left;padding-left:20px;">
                  <tr>
                      <td>
                          <asp:RadioButtonList ID="rdbStatusSearch" runat="server" AutoPostBack="True" Visible="False">
                              <asp:ListItem Value="ALL">ALL</asp:ListItem>
                              <asp:ListItem Value="Status">STATUS</asp:ListItem>
                          </asp:RadioButtonList>
                     <asp:checkboxlist ID="chkStatusSearch" runat="server" Width="100%" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="3" CellPadding="2" CellSpacing="2" TextAlign="Right">
                                   <asp:ListItem Value="O">O - Open</asp:ListItem>
                                   <asp:ListItem Value="C">C - Completed</asp:ListItem>
                                   <asp:ListItem Value="T">T - Terminated</asp:ListItem>
                                   <asp:ListItem Value="H">H - On Hold</asp:ListItem>  
                                   <asp:ListItem Value="V">V - Void</asp:ListItem>  
                                   <asp:ListItem Value="R">R - Revised</asp:ListItem>                              
                                   <asp:ListItem Value="P">P - Posted</asp:ListItem>   
                                   <asp:ListItem Value="E">E - Expired</asp:ListItem>  
                                   <asp:ListItem Value="X">X - Cancelled</asp:ListItem>  
                                  
                               </asp:checkboxlist></td>
                           </tr>
                           
                         <tr>
                             <td colspan="2"><asp:CheckBox ID="chkSearchAll" runat="server"  Text="All" Font-Size="15px" Font-Names="Calibri" Font-Bold="True" onchange="EnableDisableStatusSearch()" /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnStatusSearch" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="120px"/>
                                 <asp:Button ID="btnStatusCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>
      <asp:ModalPopupExtender ID="mdlPopupStatusSearch" runat="server" CancelControlID="btnStatusCancel" PopupControlID="pnlStatusSearch" TargetControlID="btnDummyClient2" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>

       
               <asp:Panel ID="pnlPopUpTeam" runat="server" BackColor="White" Width="700px" Height="80%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
        <asp:ModalPopupExtender ID="mdlPopUpTeam" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlTeamClose" PopupControlID="pnlPopUpTeam" TargetControlID="btnDummyClient3"  Enabled="True" DynamicServicePath=""></asp:ModalPopupExtender>
                       <table><tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Team</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlTeamClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                           
                           <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;padding-left:40px;">&nbsp;Search &nbsp;<asp:ImageButton ID="btnPopUpTeamReset" OnClick="btnPopUpTeamReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/>&nbsp;<asp:TextBox ID="txtPopUpTeam" runat="server" MaxLength="50" Height="16px" Width="400px" ForeColor="Gray" onblur = "WaterMarkTeam(this, event);" onfocus = "WaterMarkTeam(this, event);" AutoPostBack="True">Search Here for Team or In-ChargeId</asp:TextBox>
    <asp:ImageButton ID="btnPopUpTeamSearch" OnClick="btnPopUpTeamSearch_Click" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle" Visible="true" /></td><td>
                               <asp:TextBox ID="txtPopupTeamSearch" runat="server" Visible="False" Width="20%"></asp:TextBox>
                           </td></tr>


        </table>
              <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;"><div class="AlphabetPager">
    <asp:Repeater ID="rptrTeam" runat="server" Visible="False"><ItemTemplate><asp:LinkButton ID="lbtnTeam" runat="server" Text='<%#Eval("Value")%>' OnClick="TeamAlphabet_Click" ForeColor="Black" /></ItemTemplate></asp:Repeater></div><br />
        
        <asp:GridView ID="gvTeam" runat="server" DataSourceID="SqlDSTeam" ForeColor="#333333" OnRowDataBound = "OnRowDataBoundgTeam" OnSelectedIndexChanged = "OnSelectedIndexChangedgTeam" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
         CellPadding="2" GridLines="None" Width="80%"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false"><ControlStyle Width="50px" />
        <HeaderStyle HorizontalAlign="Left" />
        <ItemStyle Width="8%" /></asp:CommandField>
                
                <asp:BoundField DataField="TeamID" HeaderText="Id" SortExpression="TeamID"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField>
                <asp:BoundField DataField="TeamName" HeaderText="Name" SortExpression="TeamName"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField>
                <asp:BoundField DataField="InChargeId" HeaderText="InCharge Id" SortExpression="InChargeId"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField>
                   <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" SortExpression="Supervisor"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField>

            </Columns><EditRowStyle BackColor="#999999" /><FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" /><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#E9E7E2" /><SortedAscendingHeaderStyle BackColor="#506C8C" /><SortedDescendingCellStyle BackColor="#FFFDF8" /><SortedDescendingHeaderStyle BackColor="#6F8DAE" /></asp:GridView><asp:SqlDataSource ID="SqlDSTeam" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource></div>


                     <table><tr><td colspan="2" style="text-align:center;"></td></tr>
                           
    <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;padding-left:40px;">
  </td>
        </tr>

<tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;padding-left:10px;">
  <asp:Button ID="btnAddTeam" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="90px" Height="30px" Visible="False" />
    <asp:Button ID="btnEditTeam" runat="server" Font-Bold="True" Text="EDIT" Width="90px"  CssClass="roundbutton1" BackColor="#CFC6C0" visible="False" Height="30px" />
               
             </td>
        </tr>
        </table>
          </asp:Panel>


          <asp:Panel ID="pnlPopupInvoiceDetails" runat="server" BackColor="White" Width="98%" Height="90%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
        <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" CancelControlID="ImageButton4" PopupControlID="pnlPopupInvoiceDetails" TargetControlID="btnDummyInvoice"  Enabled="True" DynamicServicePath=""></asp:ModalPopupExtender>
                       
                <table border="0">
                           <tr>
                               <td colspan="2" style="text-align:center;"></td>
                               <td style="width:1%;text-align:right;">
                             <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                    <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:40px;"> <h4 style="color: #000000">Transaction Details</h4> <asp:TextBox ID="TextBox2" runat="server" MaxLength="50" Height="16px" Width="400px" ForeColor="Gray" onblur = "WaterMarkTeam(this, event);" onfocus = "WaterMarkTeam(this, event);" AutoPostBack="True" Visible="False">Search Here for Team or In-ChargeId</asp:TextBox>
  </td> <td>
                               <asp:TextBox ID="txtContractNoSelected" runat="server" Visible="False" Width="20%"></asp:TextBox>
                           </td></tr>
                

        </table>
              <div style="text-align: center; padding-left: 10px; padding-bottom: 5px;"><div class="AlphabetPager">
    <asp:Repeater ID="Repeater1" runat="server" Visible="False"><ItemTemplate><asp:LinkButton ID="lbtnTeam" runat="server" Text='<%#Eval("Value")%>' OnClick="TeamAlphabet_Click" ForeColor="Black" /></ItemTemplate></asp:Repeater></div><br />
        
        <asp:GridView ID="grdViewInvoiceDetails" runat="server" DataSourceID="SqlDSInvoiceDetails" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
         CellPadding="2" GridLines="None" Width="95%"><AlternatingRowStyle BackColor="White" ForeColor="#284775" /><Columns><asp:BoundField DataField="VoucherDate" HeaderText="Doc Date" DataFormatString="{0:dd/MM/yyyy}">
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="DocType" HeaderText="Doc Type">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Wrap="False" />
                <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="VoucherNumber" HeaderText="Doc No.">
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Description">
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" >
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>

             
                <asp:HyperLinkField 
         DataTextField="Type"                 
         DataNavigateUrlFields="Type,VoucherNumber" 
         DataNavigateUrlFormatString="{0}.aspx?VoucherNumber={1}&CustomerFrom=Contract" HeaderText="View" DataTextFormatString="View" target="_blank" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="White" ItemStyle-ForeColor="Black" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />
                </asp:HyperLinkField>

            </Columns><EditRowStyle BackColor="#999999" /><FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" /><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#E9E7E2" /><SortedAscendingHeaderStyle BackColor="#506C8C" /><SortedDescendingCellStyle BackColor="#FFFDF8" /><SortedDescendingHeaderStyle BackColor="#6F8DAE" /></asp:GridView><asp:SqlDataSource ID="SqlDSInvoiceDetails" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                  </asp:SqlDataSource>

                   
              </div>

            <table border="0">
      
            <tr style="padding-top:40px; text-align:center; width:auto; " >

                <td colspan="2" style="text-align:center;">
                    <asp:Button ID="btnCloseInvoice" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Close" Width="100px" Visible="False" />
                 

                </td>
                               <td style="width:1%;text-align:right;">
                                   </td></tr>
                <tr>
                    <td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;padding-left:40px;">&nbsp; <asp:TextBox ID="TextBox4" runat="server" MaxLength="50" Height="16px" Width="400px" ForeColor="Gray" onblur = "WaterMarkTeam(this, event);" onfocus = "WaterMarkTeam(this, event);" AutoPostBack="True" Visible="False">Search Here for Team or In-ChargeId</asp:TextBox>
  </td> <td>
                           </td>


                    </tr>
                            
               

        </table>
          </asp:Panel>
   <asp:ModalPopupExtender ID="ModalPopupInvoice" runat="server" CancelControlID="ImageButton4" PopupControlID="pnlPopupInvoiceDetails" TargetControlID="btnDummyInvoice" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>



        <%-- start--%>

         <asp:Panel ID="pnlNotesTemplate" runat="server" BackColor="White" Width="70%" Height="50%" BorderColor="#003366" BorderWidth="1" Visible="true" ScrollBars="Auto">
              <br />
               <table border="0" style="width:100%;padding-left:5px; margin-left:auto; margin-right:auto;">
                 <tr>
                     <td style="font-size:18px; font-weight:bold;font-family:Calibri;color:black;text-align:center;"  colspan="3">Notes Template</td>
                 </tr>
                          <tr>
                              <td colspan="1"    style="font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:right; width:8% ">&nbsp;</td>
                              <td colspan="1" style="text-align:left;padding-left:5px;width:40%">
                                  <asp:TextBox ID="txtNotesTemplate" runat="server" Font-Names="calibri" Font-Size="15px" Height="150px" MaxLength="500" TextMode="MultiLine" Width="95%"></asp:TextBox>
                              </td>

                               <td colspan="1"    style="font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left; width:5% ">
                                   <asp:ImageButton ID="ImageButton5" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                              </td>
                   </tr>
                                                  
                     
                         
                     <tr>
                             <td  style="text-align:center" colspan="3">
                                    <asp:Button ID="btnUpdateNotes" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Update" Width="100px" />
                            <%--      <a href="RV_ContractServiceSchedule.aspx" target="_blank"> <asp:Button ID="Button1" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="PrintNew" Width="100px" OnClientClick="openInNewTab();" OnClick = "PrintFile"></asp:Button></a>--%>
  
 <a href="RV_ContractServiceSchedule.aspx?Status=Open" target="_blank"><button class="roundbutton1" style="background-color:#CFC6C0;font-weight:bold;width:100px;font-family:Calibri;font-size:14px;" type="button">Print</button></a>
                     <a href="RV_ContractServiceSchedule.aspx?Status=OpenPosted" target="_blank"><button class="roundbutton1" style="background-color:#CFC6C0;font-weight:bold;width:180px;font-family:Calibri;font-size:14px;" type="button">Print with All Services</button></a>
    <asp:Button ID="btnPrintService" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Print with Selected Services" Width="195px" Visible="true" />
     <a href="Email.aspx?Type=ContractServiceSchedule" target="_blank"><button class="roundbutton1" style="background-color:#CFC6C0;font-weight:bold;width:100px;font-family:Calibri;font-size:14px;" type="button">Email</button></a>
                            
                                 <asp:Button ID="btnPrintSchedule" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Print" Width="100px" Visible="False" />
                                 <asp:Button ID="btnCloseTemplate" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Close" Width="100px" />

                             </td>
                         <%--  <td colspan="1"    style="font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:right; width:5% "></td>--%>
                         </tr>
                         <tr>
                             <td  style="text-align:center" colspan="3">
                                  <asp:Button ID="btnScheduleSummary" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Schedule Summary" Width="150px" Visible="False" />
                                 </td>
                             </tr>
                      <%-- <tr>
                       <td colspan="3" style="text-align:center;" class="CellFormat">
                           <asp:CheckBox ID="chkPrintStatus" runat="server" Text="Include Posted Records" />
                       </td>
                   </tr>--%>
        </table>
           </asp:Panel>

     <asp:ModalPopupExtender ID="ModalPopupNotestemplate" runat="server"  CancelControlID="btnCloseTemplate" PopupControlID="pnlNotesTemplate" TargetControlID="btndummyPrint" BackgroundCssClass="modalBackground"  ></asp:ModalPopupExtender>

           <asp:Button ID="btndummyPrint" runat="server" cssclass="dummybutton" />
         
         <%--end--%>


         <%--Start Extend Contract--%>

               


         
    <asp:Panel ID="pnlExtendContract" runat="server" BackColor="White" Width="50%" Height="55%" BorderColor="#003366" BorderWidth="1" Visible="true" ScrollBars="Auto">
              <br />
               <table border="0" style="width:100%;padding-left:25px; margin-left:auto; margin-right:auto;">
                 <tr>
                     <td style="font-size:18px; font-weight:bold;font-family:Calibri;color:black;text-align:center;"  colspan="2">Extend Contract End Date</td>
                 </tr>
                   <tr>
                       <td colspan="2" style="font-size:18px; font-weight:bold;font-family:Calibri;color:black;text-align:center;">
                           <asp:Label ID="lblMessageExtendContract" runat="server"></asp:Label>
                       </td>
                   </tr>
                   <tr>

                        
                       <td colspan="2" style="font-size:18px; font-weight:bold;font-family:Calibri;text-align:center;">
                           <asp:TextBox ID="txtAlertExtendContract" runat="server" style="font-size:18px; font-weight:bold;font-family:Calibri;text-align:center;" AutoCompleteType="Disabled" Height="16px" Width="80%" BackColor="Red" BorderStyle="None" ForeColor="White" ></asp:TextBox>
                           <asp:CalendarExtender ID="CalendarExtenderExtendContract" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtEditExtendContract" TargetControlID="txtAlertStatus" />
                       </td>
                   </tr>


                          <tr>
                              <td style="width:30%; font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;" >
                                  Current Contract End Date</td>
                              <td colspan="1" style="text-align:left;padding-left:5px;width:70%">
                                  <asp:TextBox ID="txtEndDate" runat="server" AutoCompleteType="Disabled" Height="16px" Width="20%"></asp:TextBox>
                               
                                  </td>
                         </tr>
                  
                         
                          <tr>
                              <td style="width:30%; font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                  <asp:Label ID="Label20" runat="server" Text="Extend Contract End Date"></asp:Label>
                                  &nbsp;<asp:Label ID="Label21" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                              </td>
                              <td colspan="1" style="text-align:left;padding-left:5px;width:70%">
                                  <asp:TextBox ID="txtEditEndDate" runat="server" AutoCompleteType="Disabled" Height="16px" Width="20%"></asp:TextBox>
                                  <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtEditEndDate" TargetControlID="txtEditEndDate" />
                              </td>
                   </tr>
                  
                         
                            <tr>
                             <td class="CellFormat">Contract Notes</td>
                             <td colspan="1" style="text-align:left;padding-left:5px;width:70%">
                                    <asp:TextBox ID="txtContractNotesEdit" runat="server" Height="70px" MaxLength="10" Width="70%" TextMode="MultiLine"></asp:TextBox>
                      </td>
                         </tr>
                  
                         
                          <tr>
                                  
   
                               <td >
                                   <asp:TextBox ID="TextBox11" runat="server" AutoCompleteType="Disabled" BackColor="White" Height="16px" Width="35%" BorderStyle="None" ForeColor="White"></asp:TextBox>
                                   <br />
                               </td>
                             </tr>
                         
                     <tr><td colspan="1" style="text-align:center" >
                             &nbsp;</td>
                             <td style="text-align:left">
                                 <asp:Button ID="btnConfirmExtendContract" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Save" Width="100px" OnClientClick="ConfirmChSt()"/>
                                 <asp:Button ID="btnCancelExtendContract" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Cancel" Width="100px" />
                             </td>
                         </tr>
                     

        </table>
           </asp:Panel>

     <asp:ModalPopupExtender ID="mdlPopupExtendContract" runat="server"  CancelControlID="" PopupControlID="pnlExtendContract" TargetControlID="btnDummyExtendContract" BackgroundCssClass="modalBackground"  ></asp:ModalPopupExtender>
                    <asp:Button ID="btnDummyExtendContract" runat="server" cssclass="dummybutton" />


       <%--  end Extend Contract--%>



          <%-- start--%>

         <asp:Panel ID="pnlNotesTemplateGrid" runat="server" style="margin-left:auto; margin-right:auto; " HorizontalAlign="Center" BackColor="White" Width="50%" Height="90%" BorderColor="#003366" BorderWidth="1" Visible="true" ScrollBars="Auto" >
              <br />
               <table border="1" style="width:100%;padding-left:5px; margin-left:auto; margin-right:auto;">
                 <tr>
                     <td style="font-size:18px; font-weight:bold;font-family:Calibri;color:black;text-align:center;"  colspan="2">Notes Template</td>
                 </tr>
                         <tr style="width:100%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:center;  width:100%;margin-left:auto; margin-right:auto; " colspan="2">
        
        
        <asp:UpdatePanel ID="updpnlServiceRecs" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <div style="height: 400px; overflow: scroll">
            <asp:GridView ID="grvNotesTemplate" runat="server" AllowSorting="True"  
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="12px"  Font-Size="14px"
            ShowFooter="True" Style="text-align: left" Width="80%" DataSourceID="SqlDSNotesTemplate">
                
                <Columns> 
                              
                <asp:TemplateField> <HeaderTemplate><asp:CheckBox ID="chkSelectInvoiceGV" runat="server" AutoPostBack="true" TextAlign="Right"  Width="5%" ></asp:CheckBox></HeaderTemplate>


               <ItemTemplate><asp:CheckBox ID="chkSelectGV" runat="server" Font-Size="12px" Enabled="true" Height="18px"  Width="5%" AutoPostBack="false"   ></asp:CheckBox></ItemTemplate></asp:TemplateField>            
              <asp:TemplateField HeaderText="Note ID"><ItemTemplate><asp:TextBox ID="txtNoteIDGV" runat="server" Text='<%# Bind("NoteID")%>' Visible="true" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="100px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Content" ><ItemTemplate><asp:TextBox ID="txtContentGV" runat="server" Text='<%# Bind("Content")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="250px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Module" ><ItemTemplate><asp:TextBox ID="txtModuleGV" runat="server" Text='<%# Bind("Module")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="150px"></asp:TextBox></ItemTemplate></asp:TemplateField>
             <asp:TemplateField HeaderText="Default YN" ><ItemTemplate><asp:TextBox ID="txtDefaultDisplayGV" runat="server" Text='<%# Bind("DefaultDisplay")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="55px"></asp:TextBox></ItemTemplate></asp:TemplateField>
           
               </Columns>

        <FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" /><RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" /></asp:GridView>
                </div>
            </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="grvNotesTemplate" EventName="SelectedIndexChanged" /></Triggers></asp:UpdatePanel>&nbsp;</td></tr>
             

             
              <tr>
                  <td  style="width:80%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center" colspan="2">
                 <asp:UpdatePanel ID="UpdateSelect" runat="server" UpdateMode="Conditional"></asp:UpdatePanel>
                       </td>
                  
            </tr>

               
            
        </table>


                
       

                <table border="0" style="width:99%; margin:auto; border:solid; border-color:ButtonFace;">
                     <tr>
                    
                         
                  <td  style="width:80%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
                  <asp:UpdatePanel ID="updPnlProgress" runat="server">

<ContentTemplate>    
                            <asp:SqlDataSource ID="SqlDSNotesTemplate" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                       
                      </ContentTemplate>

</asp:UpdatePanel>  
                   
                        </td>
    
                         
                 
            </tr>
                                                  
                     
                         
                     <tr>
                             <td  style="text-align:center" colspan="2">
                                 <asp:Button ID="btnSelectNotesTemplateGrid" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Select" Width="100px" />
                                 <asp:Button ID="btnCloseNotesTemplateGrid" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Close" Width="100px" />
                             </td>
                         </tr>
                       

        </table>
           </asp:Panel>

     <asp:ModalPopupExtender ID="ModalPopupNotestemplateGrid" runat="server"  CancelControlID="btnCloseNotesTemplateGrid" PopupControlID="pnlNotesTemplateGrid" TargetControlID="ImageButton5" BackgroundCssClass="modalBackground"  ></asp:ModalPopupExtender>


         <%--end--%>

                <%--Confirm delete uploaded file--%>
                                              
                 <asp:Panel ID="pnlConfirmDeleteUploadedFile" runat="server" BackColor="White" Width="500px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="lblEvent" runat="server" Text="Confirm DELETE"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="lblQuery" runat="server" Text="Are you sure to DELETE the File?"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmDelete" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                                 <asp:Button ID="btnConfirmDeleteNo" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupDeleteUploadedFile" runat="server" CancelControlID="btnConfirmNo" PopupControlID="pnlConfirmDeleteUploadedFile" TargetControlID="btndummyDeleteUploadedFile" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyDeleteUploadedFile" runat="server" CssClass="dummybutton" />

             <%-- Confirm Delete uploaded file--%>


                      <%--Confirm Zeo value--%>
                                              
                 <asp:Panel ID="pnlZeroValue" runat="server" BackColor="White" Width="400px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label10" runat="server" Text="ZERO VALUE CONTRACT"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label11" runat="server" Text="Agreed Value is set to ZERO, Do you wish to confirm?"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnZeroValueYes" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                                 <asp:Button ID="btnZeroValueNo" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupZeroValue" runat="server" CancelControlID="" PopupControlID="pnlZeroValue" TargetControlID="btnZeroValue" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnZeroValue" runat="server" CssClass="dummybutton" />

             <%-- Confirm Zero Value--%>


              <%-- Start: Confirm Different Contract Group and Service ID--%>
                                              
                 <asp:Panel ID="pnlServiceID" runat="server" BackColor="White" Width="400px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label2ServiceID" runat="server" Text="DIFFERENT CONTRACT GROUP AND SERVICE ID"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="LabelServiceID" runat="server" Text="Contract Group and Service ID do not match."></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                                 <asp:Label ID="LabelServiceID0" runat="server" Text="Do you wish to proceed and save the record?"></asp:Label>
                                </td>
                         </tr>
                            <tr>
                             <td>
                                 <br />
                               </td>
                         </tr>                       
                            <tr style="padding-top:40px;">
                             <td style="text-align:center"><asp:Button ID="btnDiffContGrpYes" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                                 <asp:Button ID="btnDiffContGrpNo" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupServiceID" runat="server" CancelControlID="btnDiffContGrpNo" PopupControlID="pnlServiceID" TargetControlID="btnDummyServiceID" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyServiceID" runat="server" CssClass="dummybutton" />

             <%-- End:  Confirm Different Contract Group and Service ID--%>


             <%--Start: Schedule: Confirm Different Contract Group and Service ID--%>
                                              
                 <asp:Panel ID="pnlServiceIDSchdl" runat="server" BackColor="White" Width="400px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label2Schdl" runat="server" Text="DIFFERENT CONTRACT GROUP AND SERVICE ID"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          <asp:Label ID="Label17Schdl" runat="server" Text="Contract Group and Service ID do not match."></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                              <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                                               <asp:Label ID="Label17Schdl0" runat="server" Text="Do you wish to proceed and save the record?"></asp:Label>
                        
             

                              </td>
                         </tr>
                            <tr>
                             <td>
                                 <br />
                               </td>
                         </tr>                       
                            <tr style="padding-top:40px;">
                             <td style="text-align:center"><asp:Button ID="btnDiffContGrpYesSchdl" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                                 <asp:Button ID="btnDiffContGrpNoSchdl" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupServiceIDSchdl" runat="server" CancelControlID="btnDiffContGrpNoSchdl" PopupControlID="pnlServiceIDSchdl" TargetControlID="btnDummyServiceIDSchdl" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyServiceIDSchdl" runat="server" CssClass="dummybutton" />

             <%-- End: Schedule: Confirm Different Contract Group and Service ID--%>




                    <%--Start: Terminate Expired Contracts --%>
                                              
                 <asp:Panel ID="pnlTerminateExpiredContracts" runat="server" BackColor="White" Width="400px" Height="150px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="lblTerminateExpiredContracts" runat="server" Text="TERMINATE EXPIRED CONTRACTS"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label27" runat="server" Text="There are Open services under this contract after"></asp:Label>
                        
                          &nbsp;<asp:Label ID="lblActualEndDate" runat="server"></asp:Label>
                        
                      </td>
                           </tr>

                <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label28" runat="server" Text="Do you wish to continue the termination?"></asp:Label>
                        
                      </td>
                           </tr>

     <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label102" runat="server" Text="If yes, these open services will be terminated"></asp:Label>
                        
                          .</td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td style="text-align:center"><asp:Button ID="btnTerminateExpiredContractsYes" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                                 <asp:Button ID="BtnTerminateExpiredContractsNo" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlTerminateExpiredContracts" runat="server" CancelControlID="" PopupControlID="pnlTerminateExpiredContracts" TargetControlID="btnDummyTerminateExpiredContracts" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyTerminateExpiredContracts" runat="server" CssClass="dummybutton" />

             <%-- End: Terminate Expired Contracts--%>



                       <%--Start: SAVE: Edit Agree Value: No Change --%>
                                              
                 <asp:Panel ID="pnlEditAgreeValueSaveNoChange" runat="server" BackColor="White" Width="400px" Height="140px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="lblEditAgreeValueNoChange" runat="server" Text="EDIT AGREE VALUE"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label18" runat="server" Text="No change was done on the Agreed Value."></asp:Label>
                        
                      </td>
                           </tr>

                <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="lblLine2NoChange" runat="server" Text="Please press Cancel if you wish to cancel this operation."></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnEditAgreeValueNoChange" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px"/>
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlEditAgreeValueSaveNoChange" runat="server" CancelControlID="" PopupControlID="pnlEditAgreeValueSaveNoChange" TargetControlID="btnDummyEditAgreeValueSaveNoChange" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyEditAgreeValueSaveNoChange" runat="server" CssClass="dummybutton" />

             <%-- End: SAVE: Edit Agree Value : No Change--%>

                 <%--Start: SAVE: Edit Agree Value--%>
                                              
                 <asp:Panel ID="pnlEditAgreeValueSave" runat="server" BackColor="White" Width="400px" Height="170px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="lblEditAgreeValue" runat="server" Text="EDIT AGREE VALUE"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label17" runat="server" Text="Do you wish to change the price of the Contract"></asp:Label>
                        
                      </td>
                           </tr>

               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="lblLine2EditAgreeValueSave" runat="server" Text="and all service records starting"></asp:Label>
                        
                      </td>
                           </tr>

               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="lblLine3EditAgreeValueSave" runat="server"></asp:Label>
                        
                      </td>
                           </tr>


               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="lblLine4EditAgreeValueSave" runat="server" Text="Do you wish to continue?" Visible="False"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnEditAgreeValueSaveYes" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" OnClientClick="calculateportfoliovaluesAgreeValueEdit()"  Font-Bold="True" Text="Yes" Width="100px"/>
                                 <asp:Button ID="btnEditAgreeValueSaveNo" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlEditAgreeValueSave" runat="server" CancelControlID="" PopupControlID="pnlEditAgreeValueSave" TargetControlID="btnDummyEditAgreeValueSave" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyEditAgreeValueSave" runat="server" CssClass="dummybutton" />

             <%-- End: SAVE: Edit Agree Value --%>


        <%--Start : Edit PO NO--%>
                
                                              
                 <asp:Panel ID="pnlEditPONOSave" runat="server" BackColor="White" Width="450px" Height="170px" BorderColor="#003366" BorderWidth="1px" ScrollBars="None">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="lblEditPONOLabel" runat="server" Text="EDIT PO. NO. & MANUAL CONTRACT NO."></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label31" runat="server" Text="Do you wish to update the Manual Contract No. and P.O. Number "></asp:Label>
                        
                      </td>
                           </tr>

               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label32" runat="server" Text="and all service records starting from "></asp:Label>
                        
                      </td>
                           </tr>

               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label33" runat="server"></asp:Label>
                        
                      </td>
                           </tr>


               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label34" runat="server" Text="Do you wish to continue?" Visible="False"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnEditPONOSaveYes" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" OnClientClick="calculateportfoliovaluesAgreeValueEdit()"  Font-Bold="True" Text="Yes" Width="100px"/>
                                 <asp:Button ID="btnEditPONOSaveNo" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlEditPONOSave" runat="server" CancelControlID="" PopupControlID="pnlEditPONOSave" TargetControlID="btnDummyEditPONOSave" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyEditPONOSave" runat="server" CssClass="dummybutton" />

            

        <%-- End : Edit PO NO--%>

           <%--'28.02--%>

       <%--  12.05.21--%>

          <%--Start : InActive Accounts--%>
                
                                              
                 <asp:Panel ID="pnlInActiveAccount" runat="server" BackColor="White" Width="500px" Height="200px" BorderColor="#003366" BorderWidth="1px" ScrollBars="None">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium; ">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center; " colspan="2">
                         
                          <asp:Label ID="lblInactiveAccount" runat="server" Text="INACTIVE ACCOUNT"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr style="height:20px;">
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"" colspan="2">
                         
                          &nbsp;<asp:Label ID="lblInactiveAccount1" runat="server" Text=""></asp:Label>
                        
                      </td>
                           </tr>

               <tr style="height:20px;">
                      <td class="CellFormat" style="text-align:left; margin-left:auto; margin-right:auto; width:14%; padding-left:2%;">
                         
                           Account ID :</td>
                           <td class="CellFormat" style="text-align:left; margin-left:auto; margin-right:auto; width:80%;;padding-left:2%;">
                               <asp:Label ID="lblInactiveAccount2" runat="server" Text=""></asp:Label>
                      </td>
                           </tr>



               <tr style="height:20px;">
                       <td class="CellFormat" style="text-align:left; margin-left:auto; margin-right:auto;width:14%; padding-left:2%;">
                         
                          Name :
                        
                      </td>
                           <td class="CellFormat" style="text-align:left; margin-left:auto; margin-right:auto; width:80%;padding-left:2%;">
                               <asp:Label ID="lblInactiveAccount3" runat="server"></asp:Label>
                           </td>
                           </tr>

               <tr style="height:25px;">
                      <td class="CellFormat" style="text-align:left; margin-left:auto; margin-right:auto;width:15%; padding-left:2%;">
                         
                         Reason/Remarks: 
                        
                      </td>
                           <td class="CellFormat" style="text-align:left; margin-left:auto; margin-right:auto; width:80%;padding-left:2%;">
                               <asp:Label ID="lblInactiveAccount4" runat="server"></asp:Label>
                           </td>
                           </tr>
              
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnOkInActiveAccounts" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"   Font-Bold="True" Text="OK" Width="100px"/>
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlInActiveAccount" runat="server" CancelControlID="" PopupControlID="pnlInActiveAccount" TargetControlID="btnDummyInActiveAccount" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyInActiveAccount" runat="server" CssClass="dummybutton" />

            

        <%-- End : Inactive Accounts--%>

        <%-- 12.05.21  --%>


            <asp:Panel ID="pnlWarning" runat="server" BackColor="White" Width="470px" Height="250px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center" colspan="2">
                         
                          <asp:Label ID="lblWarning" runat="server" Text="WARNING"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2" style="text-align:center; margin-left:auto; margin-right:auto;">
                                 <asp:Label ID="lblWarningAlert" runat="server" Font-Bold="True" ForeColor="Red" Width="90%" ></asp:Label>
                                 /td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto; margin-top:10px" colspan="2">
                         
                          &nbsp;<asp:Label ID="Label16" runat="server" Text="Do you wish to REGENERATE the schedule of this Contract?"></asp:Label>
                        
                      </td>
                           </tr>

               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"" colspan="2">
                         
                          &nbsp;<asp:Label ID="Label19" runat="server" Text="Take note that all the current services that are generated "></asp:Label>
                        
                      </td>
                           </tr>

               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"" colspan="2">
                         
                          &nbsp;<asp:Label ID="lblwarning2" runat="server" Text=" will be voided and a new set of schedules will be generated."></asp:Label>
                        
                      </td>
                           </tr>

               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"" colspan="2">
                         
                          &nbsp;<asp:Label ID="Label22" runat="server"></asp:Label>
                        
                      </td>
                           </tr>


               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"" colspan="2">
                         
                          &nbsp;<asp:Label ID="Label24" runat="server" Text="Please Enter "  Visible="True"></asp:Label>
                        <asp:Label ID="lblRandom" runat="server" Visible="True"></asp:Label>
                        <asp:Label ID="Label25" runat="server" Text=" in the box below and click OK to proceed." Visible="True"></asp:Label>
                        
                      </td>
                           </tr>

             
                  

                   <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"" colspan="2">
                         
                          &nbsp;<asp:Label ID="Label1" runat="server"></asp:Label>
                        
                      </td>
                           </tr>


               <tr>

                   <td class="CellFormat" style="text-align:right; margin-left:auto; margin-right:auto;">&nbsp;<asp:Label ID="lblLine5EditAgreeValueSave" runat="server" Text="Confirmation Code " Visible="True"></asp:Label>
                   </td>
                   <td class="CellFormat" style="text-align:left; margin-left:auto; margin-right:auto;">
                       <asp:TextBox ID="txtConfirmationCode" runat="server" Visible="True" Width="30%"></asp:TextBox>
                   </td>
                  <tr>
                      <td colspan="2">
                          <br />
                      </td>
                  </tr>
                  <tr style="padding-top:40px;">
                      <td colspan="2" style="text-align:center">
                          <asp:Button ID="btnRegenerateConfirm" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="calculateportfoliovaluesAgreeValueEdit()" Text="OK" Width="100px" />
                          <asp:Button ID="Button13" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Cancel" Width="100px" />
                      </td>
                  </tr>
            
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlWarning" runat="server" CancelControlID="" PopupControlID="pnlWarning" TargetControlID="btnDummyWarning" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnDummyWarning" runat="server" CssClass="dummybutton" />

             <%-- End: SAVE: Edit Agree Value --%>

        <%--'28.02--%>

               <%-- start: edit PO--%>

            <asp:Panel ID="pnlEditPONo" runat="server" BackColor="White" Width="40%" Height="45%" BorderColor="#003366" BorderWidth="1px" ScrollBars="None">
              
                     <table border="0" style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Manual Contract No., PO No and Other Reference</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageSchDate" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertSchDate" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:left;">Manual Contract No.</td>
                             <td class="CellTextBox">
                                    <asp:TextBox ID="txtYourReferenceEdit" runat="server" Height="16px" MaxLength="25" Width="50%"></asp:TextBox>
                      </td>
                         </tr>
                          <tr>
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:left;">PO No.</td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtPONoEdit" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="100" Width="50%"></asp:TextBox>
                              </td>
                         </tr>
                          <tr>
                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:left;">Other Reference</td>
                              <td class="CellTextBox" colspan="1">   
                                     <asp:TextBox ID="txtOurReferenceEdit" runat="server" MaxLength="50" Height="16px" Width="50%"></asp:TextBox>
                
                              </td>
                           </tr>
                        
                        
                         <tr>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:left;">Effective Date <asp:Label ID="lblEditPONO" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td>
                             <td class="CellTextBox" colspan="1">
                                 <asp:TextBox ID="txtEffectiveDateEditPONO" runat="server" AutoCompleteType="Disabled" Height="16px" Width="49%" AutoPostBack="True"></asp:TextBox>
                                      
                                  <asp:CalendarExtender ID="CalendarExtenderEditPONO" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtEffectiveDateEditPONO" TargetControlID="txtEffectiveDateEditPONO" />
                             </td>
                         </tr>
                        
                        
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnPONoEditSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnSchCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupPONoEdit" runat="server" CancelControlID="btnSchCancel" PopupControlID="pnlEditPONo" TargetControlID="btndummyPONoEdit" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Button ID="btndummyPONoEdit" runat="server" cssclass="dummybutton" />
  

        <%-- end:Edit PO--%>


              <%-- start: GST Inclusive--%>

            <asp:Panel ID="pnlGSTInclusive" runat="server" BackColor="White"  Width="30%" Height="38%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto" >
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">GST Inclusive Amount </h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageGSTInclusive" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertGSTInclusive" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td style="width:30%; font-size:15px; font-weight:bold;font-family:Calibri;color:black; text-align:left;">GST Inclusive Amount</td>
                             <td class="CellTextBox">
                                    <asp:TextBox ID="txtGSTInclusiveAmount" runat="server" Height="16px" style="text-align:right" MaxLength="25" Width="50%" AutoPostBack="True" onkeypress="return allowOnlyNumber(event);"></asp:TextBox>
                      </td>
                         </tr>
                          <tr>
                              <td style="width:30%; font-size:15px; font-weight:bold;font-family:Calibri;color:black; text-align:left;">GST Amount</td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtGSTAmount" runat="server" AutoPostBack="True" style="text-align:right"  Height="16px" MaxLength="25" Width="50%"></asp:TextBox>
                              </td>
                         </tr>
                          <tr>
                              <td style="width:30%; font-size:15px; font-weight:bold;font-family:Calibri;color:black; text-align:left;">Agreed Value Amount</td>
                              <td class="CellTextBox">
                                  <asp:TextBox ID="txtAgreedValueAmount" runat="server" AutoCompleteType="Disabled" Height="16px" style="text-align:right"  MaxLength="30" Width="50%"></asp:TextBox>
                              </td>
                         </tr>
                         
                        
                        
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnGSTInclusiveOK" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnGSTInclusiveCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlGSTInclusive" runat="server" CancelControlID="btnGSTInclusiveCancel" PopupControlID="pnlGSTInclusive" TargetControlID="btndummyGSTInclusive" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Button ID="btndummyGSTInclusive" runat="server" cssclass="dummybutton" />
  

        <%-- end:GST Inclusive --%>

                       <%-- start: Notes--%>

            <asp:Panel ID="pnlEditNotes" runat="server" BackColor="White" Width="40%" Height="55%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Notes</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageNote" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertNote" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat">Notes</td>
                             <td class="CellTextBox">
                                    <asp:TextBox ID="txtNotesEdit" runat="server" Height="70px" MaxLength="10" Width="70%" TextMode="MultiLine"></asp:TextBox>
                      </td>
                         </tr>
                        
                        
                        
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnNoteEditSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnNotesCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupNotes" runat="server" CancelControlID="btnNotesCancel" PopupControlID="pnlEditNotes" TargetControlID="btndummyNotesEdit" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Button ID="btndummyNotesEdit" runat="server" cssclass="dummybutton" />
  

        <%-- end:Notes--%>



                       <%-- start: Billing Frequency--%>

            <asp:Panel ID="pnlEditBillingFrequency" runat="server" BackColor="White" Width="40%" Height="55%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:10px">
                         <tr>
                             <td colspan="2" >
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Billing Frequency</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="Label14" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertBillingFrequency" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat">Billing Frequency</td>
                             <td class="CellTextBox">
                                    <asp:DropDownList ID="ddlBillingFreqEdit" runat="server" AppendDataBoundItems="True" Width="40.5%">
                                        <asp:ListItem>--SELECT--</asp:ListItem>
                                    </asp:DropDownList>
                      </td>
                         </tr>
                        
                        
                        
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnBillingFrequencyEditSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnBillingFrequencyCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupBillingFrequency" runat="server" CancelControlID="btnBillingFrequencyCancel" PopupControlID="pnlEditBillingFrequency" TargetControlID="btndummyBillingFrequencyEdit" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Button ID="btndummyBillingFrequencyEdit" runat="server" cssclass="dummybutton" />
  

        <%-- end:Billing Fequency--%>


             <%-- start: Scheduler--%>

            <asp:Panel ID="pnlEditScheduler" runat="server" BackColor="White" Width="40%" Height="55%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Scheduler</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageScheduler" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertScheduler" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat">Scheduler</td>
                             <td class="CellTextBox">
                                    <asp:DropDownList ID="ddlSchedulerEdit" runat="server" AppendDataBoundItems="True" Width="60%">
                                        <asp:ListItem>--SELECT--</asp:ListItem>
                                    </asp:DropDownList>
                      </td>
                         </tr>
                        
                        
                        
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnSchedulerEditSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnSchedulerCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                       
                
        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupScheduler" runat="server" CancelControlID="btnSchedulerCancel" PopupControlID="pnlEditScheduler" TargetControlID="btndummySchedulerEdit" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Button ID="btndummySchedulerEdit" runat="server" cssclass="dummybutton" />
  

        <%-- end:Scheduler--%>
      
           <%-- start: Salesman--%>

            <asp:Panel ID="pnlEditSalesman" runat="server" BackColor="White" Width="40%" Height="55%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Salesman</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageSalesman" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertSalesman" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat">Salesman</td>
                             <td class="CellTextBox">
                                    <asp:DropDownList ID="ddlSalesmanEdit" runat="server" AppendDataBoundItems="True" Width="60%">
                                        <asp:ListItem>--SELECT--</asp:ListItem>
                                    </asp:DropDownList>
                      </td>
                         </tr>
                        
                        
                        
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnSalesmanEditSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnSalesmanCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                       
                
        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupSalesman" runat="server" CancelControlID="btnSalesmanCancel" PopupControlID="pnlEditSalesman" TargetControlID="btndummySalesmanEdit" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Button ID="btndummySalesmanEdit" runat="server" cssclass="dummybutton" />
  

        <%-- end:Salesman--%>



            <%-- start: Company Group--%>

            <asp:Panel ID="pnlEditCompanyGroup" runat="server" BackColor="White" Width="40%" Height="55%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Company Group</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageCompanyGroup" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertCompanyGroup" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat">Company Group</td>
                             <td class="CellTextBox">
                                    <asp:DropDownList ID="ddlCompanyGroupEdit" runat="server" AppendDataBoundItems="True" Width="40.5%">
                                        <asp:ListItem>--SELECT--</asp:ListItem>
                                    </asp:DropDownList>
                      </td>
                         </tr>
                        
                        
                        
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnCompanyGroupEditSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnCompanyGroupCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                       
                
        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupCompanyGroup" runat="server" CancelControlID="btnCompanyGroupCancel" PopupControlID="pnlEditCompanyGroup" TargetControlID="btndummyCompanyGroupEdit" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Button ID="btndummyCompanyGroupEdit" runat="server" cssclass="dummybutton" />
  

        <%-- end:Company Group--%>


           <%-- start: Agree Value--%>

            <asp:Panel ID="pnlEditAgreeValue" runat="server" BackColor="White" Width="90%" Height="95%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table border="0" style="width:100%;padding-left:25px">
                         <tr>
                             <td colspan="8">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Agree Value</h4>
                             </td>
                         </tr>

                          <tr>
               <td colspan="8" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="Label15" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="8" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertEditAgreeValue" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         

                           <tr>
                                  <td class="CellFormat1">Contract No.
                                     
                                  </td>
                                  <td  style="color:black;  text-align:left; width:14%;">
                                            <asp:TextBox ID="txtContractNoEditAgreeValue" runat="server" ReadOnly="true" BackColor="#C8C8C8" BorderStyle="None" style="text-align:left" Width="98%" AutoPostBack="True"></asp:TextBox>
                          
                                  </td>
                                  <td style="font-size:15px; font-weight:bold; font-family:Calibri; color:black; text-align:right;  width:12%;" >
                                  Current Agreed Price
                                     </td>
                                  <td style="color:black;  text-align:left; width:10%;">
                                            <asp:TextBox ID="txtCurrentAgreeValue" runat="server" ReadOnly="true" BackColor="#C8C8C8" BorderStyle="None" style="text-align:right" Width="80%" AutoPostBack="True"></asp:TextBox>
                          
                                  </td>
                                   <td style= "width:7%; font-size:15px; font-weight:bold; font-family:Calibri; color:black; text-align:right;">
                                 Account ID
                                   </td>
                                  <td style="color:black;  text-align:left; width:9%;"> 
                                         <asp:TextBox ID="txtAccountIDEditAgreeValue" runat="server" ReadOnly="true" BackColor="#C8C8C8" BorderStyle="None" style="text-align:left" Width="90%" AutoPostBack="True"></asp:TextBox>
                                

                                  </td>
                                  <td style= "width:5%;font-size:15px; font-weight:bold; font-family:Calibri; color:black; text-align:right;">Name</td>
                                  <td style= "width:24%">
                                        <asp:TextBox ID="txtCustomerNameEditAgreeValue" runat="server" ReadOnly="true" BackColor="#C8C8C8" BorderStyle="None" style="text-align:left" Width="98%" AutoPostBack="True"></asp:TextBox>
                              

                                  </td>
                         </tr>



                               <tr>
                                  <td class="CellFormat1">Start Date
                                     
                                  </td>
                                  <td  style="color:black;  text-align:left; width:14%;">
                                            <asp:TextBox ID="txtContractStartDateEditAgreeValue" runat="server" ReadOnly="true" BackColor="#C8C8C8" BorderStyle="None" style="text-align:left" Width="98%" AutoPostBack="True"></asp:TextBox>
                          
                                  </td>
                                  <td style="font-size:15px; font-weight:bold; font-family:Calibri; color:black; text-align:right;  width:12%;" >
                                      End Date
                                     </td>
                                  <td style="color:black;  text-align:left; width:10%;">
                                              <asp:TextBox ID="txtContractEndDateEditAgreeValue" runat="server" AutoPostBack="True" BackColor="#C8C8C8" BorderStyle="None" ReadOnly="true" style="text-align:left" Width="98%"></asp:TextBox>
                 
                                  </td>
                                   <td style= "width:7%; font-size:15px; font-weight:bold; font-family:Calibri; color:black; text-align:right;">
                                 
                                       Actual End Date</td>
                                  <td style= "width:9%"> 
                                

                                      <asp:TextBox ID="txtContractActualEndDateEditAgreeValue" runat="server" AutoPostBack="True" BackColor="#C8C8C8" BorderStyle="None" ReadOnly="true" style="text-align:left" Width="98%"></asp:TextBox>
                                

                                  </td>
                                  <td style= "width:7%;font-size:15px; font-weight:bold; font-family:Calibri; color:black; text-align:right;">Last Price Change Date</td>
                                  <td style= "width:24%; text-align:left;">
                                      

                                      <asp:TextBox ID="txtLastPriceChangeDate" runat="server" BackColor="#C8C8C8" BorderStyle="None" ReadOnly="True" style="text-align:left" Width="60%"></asp:TextBox>
                                      

                                  </td>
                         </tr>

                         <tr>
                             <td class="CellFormat1" colspan="1"></td>
                             <td style="color:black;  text-align:left;" colspan="7">
                      </td>
                         </tr>
                        
                              <tr>
                                  <td class="CellFormat" colspan="1">&nbsp;</td>
                                  <td class="CellTextBox" colspan="7">
                                      &nbsp;</td>
                         </tr>
                        
                             <tr>
                                  <td class="CellFormat" colspan="1">Effective Date<asp:Label ID="Label70" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                                  </td>
                                  <td style="color:black;  text-align:left;" colspan="7">
                                      <asp:TextBox ID="txtEffectiveDate" runat="server" AutoCompleteType="Disabled" Height="16px" Width="20%" AutoPostBack="True"></asp:TextBox>
                                      <asp:CalendarExtender ID="txtEffectiveDate_CalendarExtender" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtEffectiveDate" TargetControlID="txtEffectiveDate" />
                                  </td>
                         </tr>

                              <tr>
                                  <td class="CellFormat1">&nbsp;</td>
                             
                                       <td style="color:black; text-align:left; font-weight:bold; margin-left:3px; padding-left:1px; font-family:Calibri; font-size:14px; border-top-width:1px; border-bottom-width:1px; border-left-width:1px;border-right-width:1px; border-left-style: solid; border-left-color: inherit; border-right-style: solid; border-right-color: inherit; border-top-style: solid; border-top-color: inherit; border-bottom-style: none; border-bottom-color: inherit;" colspan="3" >
                          
                                                         <asp:RadioButton ID="rdbFixedValueChange" runat="server" Text="Fixed Value Change" GroupName="EditAgreeValue" AutoPostBack="True" />
                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                      <asp:RadioButton ID="rdbPercentageChange" runat="server" AutoPostBack="True" GroupName="EditAgreeValue" Text="Percentage Change" />
                                  </td>
                                  <td style="width:45%" colspan="4"></td>
                         </tr>
                        
                              <tr>
                                  <td class="CellFormat1">New Agree Value
                                      <asp:Label ID="Label69" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                                  </td>

                             
                                  <td  style="color:black;  text-align:left; margin-left:2px;  padding-left:2px; border-bottom-width:1px; border-left-width:1px;border-right-width:1px; border-left-style: solid; border-left-color: inherit; border-right-style: solid; border-right-color: inherit; border-top-style: none; border-top-color: inherit; border-top-width: medium; border-bottom-style: solid; border-bottom-color: inherit;" colspan="3">
                                   
                                           <asp:TextBox ID="txtAgreeValueEdit" runat="server"  style="text-align:right" Width="25%" AutoPostBack="True"></asp:TextBox>
                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label74" runat="server" Text="% Change" style="font-size:15px;  font-weight:bold; font-family:Calibri; color:black; text-align:left;"></asp:Label>
                                      &nbsp;<asp:TextBox ID="txtPercentageChangeAgreeValueEdit" runat="server" AutoPostBack="True" style="text-align:right" Width="10%"></asp:TextBox>
                                       <asp:Label ID="lblNewValue" runat="server" Text="New Value" style="font-size:15px;  font-weight:bold; font-family:Calibri; color:black; text-align:left;"></asp:Label> 
                                      <asp:TextBox ID="txtNewValue" runat="server" ReadOnly="true" BackColor="#C8C8C8"  AutoPostBack="True" style="text-align:right" Width="20%"></asp:TextBox>
                                  
                                               </td>
                                   
                                   <td style="width:45%" colspan="4"></td>
                         </tr>
                        
                                                    
                          
                        
                              <tr >
                             <td class="CellFormat">Reason of Price Change<asp:Label ID="Label71" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                                  </td>
                             <td style="color:black;  text-align:left; " colspan="7">
                                    <asp:TextBox ID="txtCommentsAgreeValue" runat="server"  style="text-align:left" TextMode="MultiLine" Width="44%" Height="70px"></asp:TextBox>
                      </td>
                         </tr>
                        
                        
                         <tr style="display: none;">
                             <td class="CellFormat">Portfolio Value </td>
                             <td class="CellTextBox" colspan="7">
                                 <asp:TextBox ID="txtPortfolioValueEdit" runat="server" style="text-align:right" Width="40%"></asp:TextBox>
                             </td>
                         </tr>
                        
                        
                         <tr >
                             <td class="CellFormat">&nbsp;</td>
                             <td class="CellTextBox" colspan="7" style="font-size:15px;  font-weight:bold; font-family:Calibri; color:red; text-align:center;">   <asp:Label ID="lblTotalRecords" runat="server" Text="Records found" style="font-size:15px;  font-weight:bold; font-family:Calibri; color:red; text-align:center;"></asp:Label> 
                               </td>
                         </tr>
                         <tr >
                             <td class="CellFormat" colspan="1">
                                 <asp:SqlDataSource ID="SqlDSServicesAgreeValueEdit" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                             </td>
                             <td class="CellTextBox" colspan="7">

               <asp:GridView ID="grvServiceRecAgreeValueEdit" runat="server" AllowSorting="True"  
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="12px"  Font-Size="14px"
            ShowFooter="True" Style="text-align: left" Width="90%">
                
                  <Columns> 
                              
                <asp:TemplateField> 
                    <HeaderTemplate>
                        </HeaderTemplate>
                <ItemTemplate>
                    </ItemTemplate></asp:TemplateField>            
              <asp:TemplateField HeaderText="Record No."><ItemTemplate><asp:TextBox ID="txtServiceRecordNoGV" runat="server" Text='<%# Bind("RecordNo")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="123px"></asp:TextBox></ItemTemplate></asp:TemplateField>
           
                        <asp:TemplateField HeaderText="Service Date"><ItemTemplate><asp:TextBox ID="txtServiceDateGV" runat="server" Text='<%# Bind("ServiceDate", "{0:dd/MM/yyyy}")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="70px" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Location Id"><ItemTemplate><asp:TextBox ID="txtLocationIdGV" runat="server" Text='<%# Bind("LocationID")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="88px"></asp:TextBox></ItemTemplate></asp:TemplateField>
            
                <asp:TemplateField HeaderText="Name"><ItemTemplate><asp:TextBox ID="txtClientNameGV" runat="server" Text='<%# Bind("CustName")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="160px"></asp:TextBox></ItemTemplate></asp:TemplateField>
            
                <asp:TemplateField HeaderText="Old Price"><ItemTemplate><asp:TextBox ID="txtToBillAmtGV" runat="server" Text='<%# Bind("BillAmount")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" style="text-align:right" Height="18px" Width="70px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText="New Price"><ItemTemplate><asp:TextBox ID="txtNewBillAmtGV" runat="server" Text='0.00' Font-Size="12px" ReadOnly="false"  BorderStyle="None" style="text-align:right" Height="18px" Width="70px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
              
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoServiceRecordGV" runat="server"  Text='<%# Bind("Rcno")%>' Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
            
                         </Columns>

                   <EmptyDataTemplate>
                       NO SERVICE RECORD TO BE AFFECTED </>
                    </EmptyDataTemplate>

        <FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" /><RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" /></asp:GridView>
                             </td>
                         </tr>
                        
                        
                         <tr>
                             <td class="CellFormat1">&nbsp;</td>
                              <td class="CellFormat2">
                                           
                                      </td>
                                  <td style="text-align:right">
                            
                                  </td>
                                  <td style="text-align:right" >
                                                   <asp:Button ID="btnAgreeValueSave" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime()" Text="Save" Width="120px" />
                           
                                  </td>
                              <td style="text-align:left" >
                                           <asp:Button ID="btnAgreeValueCancel" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Cancel" Width="100px" />
                                 
                             

                              </td>
                              <td style="width:45%" colspan="3"></td>
                         </tr>

                         

                
        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupAgreeValue" runat="server" CancelControlID="btnAgreeValueCancel" PopupControlID="pnlEditAgreeValue" TargetControlID="btndummyAgreeValueEdit" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Button ID="btndummyAgreeValueEdit" runat="server" cssclass="dummybutton" />
  

        <%-- end:Agree Value--%>

          <%-- start: Billing Remarks--%>

            <asp:Panel ID="pnlEditBillingRemarks" runat="server" BackColor="White" Width="40%" Height="55%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Billing Remarks </h4>
                             </td>
                         </tr>
                      
               <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageBillingRemarks" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertBillingRemarks" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat">Remarks </td>
                             <td class="CellTextBox">
                                    
<asp:TextBox ID="txtBillingRemarksEdit" runat="server" AutoCompleteType="Disabled" Height="40px" TextMode="MultiLine" Width="60%" Font-Names="calibri" Font-Size="15px" MaxLength="4000">
                                  
                                    </asp:TextBox>
                      </td>
                         </tr>
                        
                           
                        
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnEditBillingRemarksSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnBillingRemarksCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                       
                
        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupBillingRemarks" runat="server" CancelControlID="btnBillingRemarksCancel" PopupControlID="pnlEditBillingRemarks" TargetControlID="btndummyBillingRemarksEdit" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Button ID="btndummyBillingRemarksEdit" runat="server" cssclass="dummybutton" />
  

        <%-- end:Billing Remarks--%>




          <%-- start: AutoRenewal--%>

            <asp:Panel ID="pnlAutoRenewal" runat="server" BackColor="White" Width="40%" Height="55%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Auto Renewal </h4>
                             </td>
                         </tr>
                      
               <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="LblMessageAutoRenewal" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertAutoRenewal" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat">Auto Renewal</td>
                             <td class="CellTextBox">
                                    
                     <asp:CheckBox ID="chkAutoRenewEdit" runat="server" Enabled="False" Text="Auto Renew" />
                      </td>
                         </tr>
                        
                           
                        
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnEditAutoRenewalSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnAutoRenewalCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                       
                
        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupAutoRenewalEdit" runat="server" CancelControlID="btnAutoRenewalCancel" PopupControlID="pnlAutoRenewal" TargetControlID="btndummyAutoRenewalEdit" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Button ID="btndummyAutoRenewalEdit" runat="server" cssclass="dummybutton" />
  

        <%-- end:AutoRenewal--%>
         



            <%-- start: Exclude from Batch Price --%>

            <asp:Panel ID="pnlExcludeFromBatchPriceEdit" runat="server" BackColor="White" Width="40%" Height="55%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Exclude from Batch Price </h4>
                             </td>
                         </tr>
                      
               <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblExcludeFromBatchPrice" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="Label26" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat" >Exclude from Batch Price</td>
                             <td class="CellTextBox" style="font-size:14px;font-family:'Calibri';">
                                    
                     <asp:CheckBox ID="chkExcludeFromBatchPriceEdit" runat="server" Enabled="True" Text="Exclude From BatchPrice" />
                      </td>
                         </tr>
                          
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnExcludeFromBatchPriceEdit" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnExcludeFromBatchPriceCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlExcludeFromBatchPriceEdit" runat="server" CancelControlID="btnExcludeFromBatchPriceCancel" PopupControlID="pnlExcludeFromBatchPriceEdit" TargetControlID="btndummyExcludeFromBatchPriceEdit" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Button ID="btndummyExcludeFromBatchPriceEdit" runat="server" cssclass="dummybutton" />
  

        <%-- end: Exclude from Batch Price--%>

                            <%-- Start:View Edit History--%>
              
              
              <asp:Panel ID="pnlViewEditHistory" runat="server" BackColor="White" Width="1000px" Height="85%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
                      
                <table border="0">
                           <tr>
                               <td colspan="2" style="text-align:center;"></td>
                               <td style="width:1%;text-align:right;">
                             <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                    <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:40px;"> <h4 style="color: #000000">View Record History</h4> 
  </td> <td>
                               <asp:TextBox ID="TextBox5" runat="server" Width="20%" Visible="true" CssClass="dummybutton" ></asp:TextBox>
                           </td></tr>
                
           
                    <tr><td colspan="2" style="text-align:CENTER"><asp:Label ID="Label43" runat="server" Text="" Font-Names="Calibri" Font-Size="20px" Font-Bold="True" ForeColor="Red"></asp:Label></td></tr>
        </table>
              <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
        
        <asp:GridView ID="grdViewEditHistory" runat="server" DataSourceID="sqlDSViewEditHistory" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
         CellPadding="2" GridLines="None" Width="99%"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>  
              
                <asp:BoundField DataField="LogDate" HeaderText="Date &amp; Time" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="18%" />
                </asp:BoundField>
                <asp:BoundField DataField="StaffID" HeaderText="Staff ID" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="12%" />
                </asp:BoundField>
                <asp:BoundField DataField="Action" HeaderText="Action">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="Comments" HeaderText="Comments">
                <HeaderStyle HorizontalAlign="Left" />
               
                <ItemStyle Width="50%" />
               
                </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#0099CC" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

        </asp:GridView><br />
            
                  <asp:SqlDataSource ID="sqlDSViewEditHistory" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                  </asp:SqlDataSource>

                   
              </div>

            <table border="0">
      
            <tr style="padding-top:40px; text-align:center; width:auto; " >

                <td colspan="2" style="text-align:center;">
                    <asp:Button ID="Button8" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Close" Width="100px" Visible="False" />
                 

                </td>
                               <td style="width:1%;text-align:right;">
                                   </td></tr>
                <tr>
                    <td colspan="2"><br /></td>
                </tr>
              
             

        </table>
          </asp:Panel>

                <asp:ModalPopupExtender ID="mdlViewEditHistory" runat="server" CancelControlID="ImageButton4" PopupControlID="pnlViewEditHistory" TargetControlID="btnDummyViewEditHistory" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btnDummyViewEditHistory" runat="server" BackColor="White" CssClass="dummybutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
  

             <%-- End:View Edit History--%>



        <%-- start--%>

                                <%-- Start:View Contract Price History--%>
              
              
              <asp:Panel ID="pnlViewContractPriceHistory" runat="server" BackColor="White" Width="1300px" Height="85%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
                      
                <table border="0">
                           <tr>
                               <td colspan="7" style="text-align:center;"></td>
                               <td style="width:1%;text-align:right;">
                             <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                    <tr><td colspan="7" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:20px;"> <h4 style="color: #000000">View Contract Price History</h4> 
  </td> <td>
                               <asp:TextBox ID="TextBox8" runat="server" Width="20%" Visible="true" CssClass="dummybutton" ></asp:TextBox>
                           </td></tr>
                
           
                           <tr>
                               <td colspan="7" style="text-align:CENTER">
                                   <asp:Label ID="LabelContractPriceHistory" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="20px" ForeColor="Red" Text=""></asp:Label>
                               </td>
                           </tr>
                           <tr>
                               <td style="text-align:center; width: 10%; font-size:15px;font-weight:bold;font-family:Calibri;  color:black;">Contract No</td>
                               <td style="text-align:left; font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;" class="auto-style1">
                                <asp:TextBox ID="txtContractNoPH" runat="server" AutoCompleteType="Disabled" Height="16px" Width="80%" Enabled="false"></asp:TextBox>    
                               </td>
                               <td style="text-align:center; width: 5%; font-size:15px;font-weight:bold;font-family:Calibri; color:black;">Name</td>
                               <td style="text-align:left; width: 45%; font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;" colspan="3">
                                    <asp:TextBox ID="txtCustomerNamePH" runat="server" AutoCompleteType="Disabled" Height="16px" Width="96%" Enabled="false"></asp:TextBox>
                               </td>
                               <td style="text-align:right; width: 20%; font-size:15px;font-weight:bold;font-family:Calibri; text-align:center; color:black;">
                                   <asp:CheckBox ID="chkShowVoidRcords" runat="server" Enabled="True" Text="Show Void Records" AutoPostBack="True" />
                               </td>
                                    <td style="text-align:right; width: 20%; font-size:15px;font-weight:bold;font-family:Calibri; color:black;">&nbsp;</td>
                         
                           </tr>
                           <tr>
                               <td style="text-align:center; width: 10%; font-size:15px;font-weight:bold;font-family:Calibri;  color:black;">Agree Value</td>
                               <td class="auto-style1" style="text-align:left; font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;">
                                   <asp:TextBox ID="txtAgreeValuePH" runat="server" AutoCompleteType="Disabled" Enabled="false" Height="16px" Width="80%"></asp:TextBox>
                               </td>
                               <td style="text-align:center; width: 10%; font-size:15px;font-weight:bold;font-family:Calibri; color:black;">Start Date</td>
                               <td style="text-align:left; width: 15%; font-size:15px;font-weight:bold;font-family:Calibri;  color:black;">
                                   <asp:TextBox ID="txtContractStartPH" runat="server" AutoCompleteType="Disabled" Height="16px"  Width="90%" Enabled="false"></asp:TextBox>
                               </td>
                               <td style="text-align:center; width: 10%; font-size:15px;font-weight:bold;font-family:Calibri; color:black;">End Date</td>
                               <td style="text-align:left; width: 15%; font-size:15px;font-weight:bold;font-family:Calibri;  color:black;">
                                   <asp:TextBox ID="txtContractEndPH" runat="server" AutoCompleteType="Disabled" Height="16px"  Width="90%" Enabled="false"></asp:TextBox>
                               </td>
                               <td style="text-align:right; width: 30%; font-size:15px;font-weight:bold;font-family:Calibri; color:black;">&nbsp;</td>
                                <td style="text-align:right; width: 30%; font-size:15px;font-weight:bold;font-family:Calibri; color:black;">&nbsp;</td>
                         
                                </tr>
        </table>
              <div style="text-align: center; padding-left: 10px; padding-bottom: 5px;">
        
        <asp:GridView ID="grdViewPriceHistory" runat="server" DataSourceID="SqlDSViewPriceHistory" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
         CellPadding="2" GridLines="None" Width="99%"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>  
              
                 <asp:TemplateField HeaderText="Void">
                 <HeaderStyle Wrap="True" />
                <ItemTemplate>
                      <asp:CheckBox ID="chkVoid" runat="server" Checked='<%# IIf(Eval("Void").ToString().Equals("1"), True, False)%>' Enabled="False" Font-Bold="False" /> 
                      </ItemTemplate>
            </asp:TemplateField>

                 <asp:BoundField DataField="Type" HeaderText="Type" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="9%" />
                </asp:BoundField>

                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Width="7%" />
                </asp:BoundField>
               
                <asp:BoundField DataField="OldAgreedValue" DataFormatString="{0:N2}" HeaderText="Old Contract Value">
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle Width="11%" HorizontalAlign="Right" />
                </asp:BoundField>

                 <asp:BoundField DataField="NewAgreedValue" DataFormatString="{0:N2}" HeaderText="New Contract Value">
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle Width="10%" HorizontalAlign="Right" />
                </asp:BoundField>

                   <asp:BoundField DataField="ChangeInAgreedValue" DataFormatString="{0:N2}" HeaderText="Price Change">
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle Width="8%" HorizontalAlign="Right" />
                </asp:BoundField>

                <asp:BoundField DataField="PortfolioValueChange" DataFormatString="{0:N2}" HeaderText="Portfolio Value Change">
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle Width="11%" HorizontalAlign="Right" />
                </asp:BoundField>
               
                   <asp:BoundField DataField="PercentChange" DataFormatString="{0:0.000000}%" HeaderText="Percent Change">
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle Width="9%" HorizontalAlign="Right" />
                </asp:BoundField>

                <asp:BoundField DataField="Comments" HeaderText="Comments">
                <HeaderStyle HorizontalAlign="Left" />               
                <ItemStyle Width="15%" />               
                </asp:BoundField>

                 <asp:BoundField DataField="PriceModifiedBy" HeaderText="Modified By">
                <HeaderStyle HorizontalAlign="Left" />               
                <ItemStyle Width="8%" />               
                </asp:BoundField>

                 <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On">
                <HeaderStyle HorizontalAlign="Left" />               
                <ItemStyle Width="13%" />               
                </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#0099CC" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

        </asp:GridView><br />
            
                  <asp:SqlDataSource ID="SqlDSViewPriceHistory" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                  </asp:SqlDataSource>

                   
              </div>

            <table border="0">
      
            <tr style="padding-top:40px; text-align:center; width:auto; " >

                <td colspan="2" style="text-align:center;">
                    <asp:Button ID="Button9" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Close" Width="100px" Visible="False" />
                 

                </td>
                               <td style="width:1%;text-align:right;">
                                   </td></tr>
                <tr>
                    <td colspan="2"><br /></td>
                </tr>
              
             

        </table>
          </asp:Panel>

                <asp:ModalPopupExtender ID="mdlViewContractPriceHistory" runat="server" CancelControlID="ImageButton4" PopupControlID="pnlViewContractPriceHistory" TargetControlID="btnDummyViewContarctPriceHistory" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
           <asp:Button ID="btnDummyViewContarctPriceHistory" runat="server" BackColor="White" CssClass="dummybutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" />
  

             <%-- End:View Contract Price History--%>

       <%--  end--%>



         <%--start: Service Record Print--%>

           <asp:Panel ID="pnlServicesPrint" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1"  ScrollBars="auto"  style="text-align:left; width:1300px; height:600px; margin-left:auto; margin-right:auto;"  Visible="true" >

                   <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:center;padding-left:10px; width:100%;">
                           
         <tr style="width:100%"><td  style="width:80%;font-size:18px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:10px;">&nbsp;Select Records to Print&nbsp; </td>
             
                 <td  style="width:20%;font-size:18px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:0px;">
                            </td></tr>


        </table>

     
        <div style="text-align: center; padding-left: 10px; padding-bottom: 5px;">
            <br />
            <div style="text-align:center; width:100%; margin-left:auto; margin-right:auto;" >
            
                                      <asp:Panel ID="Panel9" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1"  ScrollBars="auto"  style="text-align:left; width:900px; height:300px; margin-left:auto; margin-right:auto;"  Visible="true" >
                                             <asp:Label ID="lblAlertServicePrint" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                   <br />       <asp:TextBox ID="txtPrintContractNo" runat="server" Visible="False"></asp:TextBox>
                                  
                                         <table id="table1" border="0" runat="server" style="border: 1px solid #CC3300; text-align: right; width: 100%; border-radius: 25px; height: 60px; background-color: #F3F3F3;">
            <tr>
                <td style="text-align: left;">
                         <table border="0" style="font-family: calibri; font-size: 15px; font-weight: bold; color: #000000; padding-left: 2px;width:90%;text-align:center">
                        <tr>
                            <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; text-align: right;width:15%">Service Date From               
                            </td>
                            <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black;width:15%">
                                <asp:TextBox ID="txtSvcDateFrom" runat="server" Style="text-align: left;" MaxLength="50" Height="20px" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtenderDateFrom" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSvcDateFrom" TargetControlID="txtSvcDateFrom"
                                    Enabled="True" />
                            </td>
                             <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; text-align: right;width:15%">Service Date To                
                            </td>
                            <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black;width:15%">
                                <asp:TextBox ID="txtSvcDateTo" runat="server" Style="text-align: left;" MaxLength="50" Height="20px" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtenderDateTo" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSvcDateTo" TargetControlID="txtSvcDateTo"
                                    Enabled="True" />
                            </td>
                               <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black;width:10%;" colspan="1">
                                <asp:Button ID="btnSearchDatePrint" runat="server" BackColor="#CFC6C0" CssClass="button" Font-Bold="True" Text="Search" Width="88px" 
                                   Style="position: static"  OnClick="btnSearch_Click" />
                            </td>

                        </tr>
                                                 </table>
                       </td>
            </tr>
        </table>
                                          <br />
            <asp:GridView ID="grdViewServicesPrint" Width="100%" Font-Size="15px" runat="server"  AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SqlDSServicesPrint" ForeColor="#333333" GridLines="Vertical"> 
                                              <AlternatingRowStyle BackColor="White"/>
                                                <Columns>

                                                          <asp:TemplateField> 
               <HeaderTemplate><asp:CheckBox ID="chkSelectAllServicesPrintGV" runat="server" AutoPostBack="false" TextAlign="left" onchange="checkmultiprint()" Width="5%" OnCheckedChanged="chkServicesPrint_CheckedChanged" ></asp:CheckBox></HeaderTemplate>    <HeaderStyle HorizontalAlign="Left" />
               <ItemTemplate><asp:CheckBox ID="chkSelectServicesPrintGV" runat="server" Font-Size="12px" Enabled="true" Height="18px" OnCheckedChanged="chkServicesPrint_CheckedChanged"  Width="5%" AutoPostBack="false" CommandName="CHECK" ></asp:CheckBox></ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Left" />
                                                          </asp:TemplateField>      
                                                                                                                  <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" ItemStyle-HorizontalAlign="Center" />
             

                                                      <asp:TemplateField HeaderText="Service Record No" SortExpression="RecordNo">
                                                              <EditItemTemplate>
                                                                  <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("RecordNo")%>'></asp:TextBox>
                                                              </EditItemTemplate>
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblRecNo" runat="server" Text='<%# Bind("RecordNo")%>'></asp:Label>
                                                              </ItemTemplate>
                                                              <ControlStyle Width="8%" />
                                                              <ItemStyle Width="8%" Wrap="False" HorizontalAlign="LEFT" />
                                                          </asp:TemplateField>
                                                  <asp:CommandField ShowHeader="True" ShowSelectButton="True" Visible="false">
                                                  <ControlStyle Width="4%" />
                                                  <ItemStyle Width="4%" Wrap="False" HorizontalAlign="Left" />
                                                  </asp:CommandField>
                                                  <asp:BoundField DataField="Rcno" InsertVisible="False" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                  <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                                      <EditItemTemplate>
                                                          <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                                                      </EditItemTemplate>
                                                      <ItemTemplate>
                                                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                                      </ItemTemplate>
                                                      <ControlStyle CssClass="dummybutton" />
                                                      <HeaderStyle CssClass="dummybutton" />
                                                      <ItemStyle CssClass="dummybutton" />
                                                  </asp:TemplateField>
                                                
                                                       <asp:BoundField DataField="SchServiceDate" HeaderText="ServiceDate" SortExpression="ServiceDate" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
               
                                                       <asp:BoundField DataField="SchServiceTime" HeaderText="Sch. TimeIn" SortExpression="SchServiceTime">
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
                  <asp:BoundField DataField="SchServiceTimeOut" HeaderText="Sch. TimeOut" SortExpression="SchServiceTimeOut">
                  <ItemStyle Wrap="False" />
                  </asp:BoundField>
                                                        <asp:TemplateField HeaderText="ServiceDescription" SortExpression="Notes">
                <ItemTemplate>
                    <div style="width: 200px;text-align:left;height:37px;overflow-y:auto;">
                        <%# Eval("Notes")%>  
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
                                                 
                                              </Columns>
                                              <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                            <HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
                                            <PagerStyle ForeColor="White" HorizontalAlign="Left" BackColor="#507CD1" />
                                            <RowStyle BackColor="#EFF3FB" Font-Names="Calibri" />
                                            <SelectedRowStyle BackColor="#00ccff" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                      </asp:GridView>

                                          </asp:Panel>
                </div>
            <br /><br />
                          <table border="0" style="WIDTH:100%;font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:center;padding-left:20px;">
                    
        <tr style="padding-left:40px; width:100%">
                             <td style="text-align:center; width:100%">
                                 <asp:Button ID="btnPrintServicesPrint" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px" OnClick="btnPrintServicesPrint_Click" OnClientClick="window.open('RV_ContractServiceSchedule.aspx?Status=Services');" />
                                 <asp:Button ID="btnCancelServicesPrint" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>


        </table>
           
            <asp:SqlDataSource ID="SqlDSServicesPrint" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
            </asp:SqlDataSource>
      
              </div>
    </asp:Panel>

          <asp:ModalPopupExtender ID="mdlPopupServicesPrint" runat="server" CancelControlID="btnCancelServicesPrint" PopupControlID="pnlServicesPrint" TargetControlID="btnDummyServicesPrint" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
   
         
          <asp:Button ID="btnDummyServicesPrint" runat="server" CssClass="dummybutton" />
    <%--    <asp:Button ID="btnDummyCancelServicesPrint" runat="server" CssClass="dummybutton" />--%>

         <%--End: Multi print--%>
            </div>

         </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="tb1$TabPanel3$btnUpload" />
             <asp:PostBackTrigger ControlID="tb1$TabPanel3$gvUpload" />
              <asp:PostBackTrigger ControlID="btnScheduleSummary" />
             <asp:AsyncPostBackTrigger ControlID="tb1$TabPanel1$ddlContractGrp" EventName="SelectedIndexChanged" />
        </Triggers>
</asp:UpdatePanel>



    <script type="text/javascript">
        window.history.forward(1);

        ///

        //window.onload = LoadDefaultDates();

        var submit = 0;
        var submit1 = 0;

        function DoValidation() {

            var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency
            var valid = true;

            //alert("1");

            //if (++submit > 1) {
            //    alert('Saving the Contract is in progress.. Please wait.');
            //    submit = 0;
            //    valid = false;
            //    return valid;
            //}


            //   if (document.getElementById("<%=btnSave.ClientID%>").value == 'Saving...') {
            //       return false;
            //   }
            //   else {
            //       document.getElementById("<%=btnSave.ClientID%>").value = 'Saving...';
            //   };



            var stat = document.getElementById("<%=txtStatus.ClientID%>").value;
            if (Left(stat, 1) != 'O') {
            //alert("Records with status [O-Open] can be saved only");
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Records with status [O-Open] can be saved only";
                ResetScrollPosition();

                valid = false;
                return valid;

            }



            var agreementtype = document.getElementById("<%=ddlAgreementType.ClientID%>").options[document.getElementById("<%=ddlAgreementType.ClientID%>").selectedIndex].text;

        if (agreementtype == '--SELECT--') {
            //alert("Please Select Company Group");
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Agreement Type.";
              ResetScrollPosition();
              document.getElementById("<%=ddlAgreementType.ClientID%>").focus();
            valid = false;
            document.getElementById("<%=btnSave.ClientID%>").value = "SAVE";
                return valid;
            }



            var contactType = document.getElementById("<%=ddlContactType.ClientID%>").options[document.getElementById("<%=ddlContactType.ClientID%>").selectedIndex].text;

        if (contactType == '--SELECT--') {
            //alert("Please Select Customer Type");
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Account Type";
              ResetScrollPosition();
              document.getElementById("<%=ddlContactType.ClientID%>").focus();
            document.getElementById("<%=btnSave.ClientID%>").value = "SAVE";
                valid = false;
                return valid;
            }



            var accountid = document.getElementById("<%=txtAccountId.ClientID%>").value;

        if (accountid.trim() == '') {
            //alert("Please Select Client");
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Client";
              ResetScrollPosition();
              document.getElementById("<%=txtAccountId.ClientID%>").focus();
            valid = false;
            document.getElementById("<%=btnSave.ClientID%>").value = "SAVE";
                return valid;
            }

            var custname = document.getElementById("<%=txtCustName.ClientID%>").value;

        if (custname.trim() == '') {
            //alert("Please Select Client");
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Account Name";
              ResetScrollPosition();
              document.getElementById("<%=txtCustName.ClientID%>").focus();
            valid = false;
            document.getElementById("<%=btnSave.ClientID%>").value = "SAVE";
                return valid;
            }

        //      var clientid = document.getElementById("<%=txtClient.ClientID%>").value;

        //      if (clientid == '') {
        //          //alert("Please Select Client");
        //          document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Client";
        //          ResetScrollPosition();
        //          document.getElementById("<%=txtClient.ClientID%>").focus();
        //          valid = false;
        //          return valid;
        //      }


        //      var location = document.getElementById("<%=ddlLocateGrp.ClientID%>").options[document.getElementById("<%=ddlLocateGrp.ClientID%>").selectedIndex].text;

        //       if (location == '--SELECT--') {
        //           //alert("Please Select Location Group");
        //           document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Zone";
        //            ResetScrollPosition();
        //            document.getElementById("<%=ddlLocateGrp.ClientID%>").focus();
        //            valid = false;
        //            return valid;
        //         }


        var companygrp = document.getElementById("<%=ddlCompanyGrp.ClientID%>").options[document.getElementById("<%=ddlCompanyGrp.ClientID%>").selectedIndex].text;

        if (companygrp == '--SELECT--') {
            //alert("Please Select Company Group");
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Company Group";
              ResetScrollPosition();
              document.getElementById("<%=ddlCompanyGrp.ClientID%>").focus();
            valid = false;
            document.getElementById("<%=btnSave.ClientID%>").value = "SAVE";
                return valid;
            }


            var salesman = document.getElementById("<%=ddlSalesman.ClientID%>").options[document.getElementById("<%=ddlSalesman.ClientID%>").selectedIndex].text;

        if (salesman == '--SELECT--') {
            //alert("Please Select Salesman");
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Salesman";
              ResetScrollPosition();
              document.getElementById("<%=ddlSalesman.ClientID%>").focus();
            valid = false;
            document.getElementById("<%=btnSave.ClientID%>").value = "SAVE";
                return valid;
            }


            var scheduler = document.getElementById("<%=ddlScheduler.ClientID%>").options[document.getElementById("<%=ddlScheduler.ClientID%>").selectedIndex].text;

        if (scheduler == '--SELECT--') {
            //alert("Please Select Scheduler");
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Scheduler";
              ResetScrollPosition();
              document.getElementById("<%=ddlScheduler.ClientID%>").focus();

                valid = false;
                document.getElementById("<%=btnSave.ClientID%>").value = "SAVE";
                return valid;
            }


            var contractgrp = document.getElementById("<%=ddlContractGrp.ClientID%>").options[document.getElementById("<%=ddlContractGrp.ClientID%>").selectedIndex].text;

        if (contractgrp == '--SELECT--') {
            //alert("Please Select Contract Group");
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Contract Group";
              ResetScrollPosition();
              document.getElementById("<%=ddlContractGrp.ClientID%>").focus();
            valid = false;
            document.getElementById("<%=btnSave.ClientID%>").value = "SAVE";
                return valid;
            }

            var condetval = document.getElementById("<%=txtConDetVal.ClientID%>").value;

        if (condetval.trim() == '' || condetval < 0) {
            //alert("Please Enter Valid Contract Value");
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Valid Contract Value";
              ResetScrollPosition();
              document.getElementById("<%=txtConDetVal.ClientID%>").focus();
            valid = false;
            document.getElementById("<%=btnSave.ClientID%>").value = "SAVE";
                return valid;
            }


            var optionselected1;
            var fixedcontinuous = document.getElementById("<%=rbtFixedContinuous.ClientID%>");
                var radio1 = fixedcontinuous.getElementsByTagName("input");
                for (var j = 0; j < radio1.length; j++) {
                    if (radio1[j].checked) {
                        optionselected1 = j;
                        break;
                    }
                }
            var duration = document.getElementById("<%=txtDuration.ClientID%>").value;

            if (optionselected1 == 0) {
                if (duration.trim() == '' || duration < 1) {
                    //alert("Please Enter Valid Duration Value");
                    document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Valid Duration";
                    ResetScrollPosition();
                    document.getElementById("<%=txtDuration.ClientID%>").focus;
                    valid = false;
                    document.getElementById("<%=btnSave.ClientID%>").value = "SAVE";
                    return valid;
                }
            }

       //     if (optionselected1 == 1) {
       //         if (duration.trim() == '' || duration > 0) {
      //              //alert("Please Enter Valid Duration Value");
      //              document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Valid Duration";
     //               ResetScrollPosition();
     //               document.getElementById("<%=txtDuration.ClientID%>").focus;
     //               valid = false;
      //              document.getElementById("<%=btnSave.ClientID%>").value = "SAVE";
       //             return valid;
       //         }
       //     }

            var durationms = document.getElementById("<%=rbtLstDuration.ClientID%>");
        var radio = durationms.getElementsByTagName("input");
        var isChecked = false;
        for (var i = 0; i < radio.length; i++) {
            if (radio[i].checked) {
                isChecked = true;
                break;
            }
        }

        if (!isChecked) {
            //alert("Please Select Duration Type");
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Duration Type";
              ResetScrollPosition();

              valid = false;
              document.getElementById("<%=btnSave.ClientID%>").value = "SAVE";
              return valid;
          }



          var agreevalue = document.getElementById("<%=txtAgreeVal.ClientID%>").value;

        if (agreevalue.trim() == '' || agreevalue < 0) {
            //alert("Please Enter Valid Agreee Value");
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Valid Agreed Value";
              ResetScrollPosition();
              document.getElementById("<%=txtAgreeVal.ClientID%>").focus();
            valid = false;
            document.getElementById("<%=btnSave.ClientID%>").value = "SAVE";
                return valid;
            }

            var billingfreq = document.getElementById("<%=ddlBillingFreq.ClientID%>").options[document.getElementById("<%=ddlBillingFreq.ClientID%>").selectedIndex].text;

        if (billingfreq == '--SELECT--') {
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Billing Frequency";
              ResetScrollPosition();
              document.getElementById("<%=ddlBillingFreq.ClientID%>").focus();
            valid = false;
            document.getElementById("<%=btnSave.ClientID%>").value = "SAVE";
                return valid;
            }


        //            var industry = document.getElementById("<%=ddlIndustry.ClientID%>").options[document.getElementById("<%=ddlIndustry.ClientID%>").selectedIndex].text;

        //            if (industry == '--SELECT--') {
        //                //alert("Please Select Customer Type");
        //                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Industry";
        //                ResetScrollPosition();
        //                document.getElementById("<%=ddlIndustry.ClientID%>").focus();
        //                valid = false;
        //                return valid;
        //            }

        var servicetypeid = document.getElementById("<%=ddlServiceTypeID.ClientID%>").options[document.getElementById("<%=ddlServiceTypeID.ClientID%>").selectedIndex].text;

        if (servicetypeid == '--SELECT--') {
            //alert("Please Select Customer Type");
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Service Type ID";
              ResetScrollPosition();
              document.getElementById("<%=ddlServiceTypeID.ClientID%>").focus();
            valid = false;
            document.getElementById("<%=btnSave.ClientID%>").value = "SAVE";
                return valid;
            }



        //            var marketsegmentID = document.getElementById("<%=ddlMarketSegmentID.ClientID%>").options[document.getElementById("<%=ddlMarketSegmentID.ClientID%>").selectedIndex].text;

        //            if (marketsegmentID == '--SELECT--') {
        //                //alert("Please Select Customer Type");
        //                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select MarketSegmentID";
        //                ResetScrollPosition();
        //               document.getElementById("<%=ddlMarketSegmentID.ClientID%>").focus();
        //                valid = false;
        //                return valid;
        //            }
        var str = document.getElementById("<%=txtAgreeVal.ClientID%>").value;
        var str1 = document.getElementById("<%=txtAllocTime.ClientID%>").value;
        var str2 = document.getElementById("<%=txtValPerMnth.ClientID%>").value;
        var str3 = document.getElementById("<%=txtDuration.ClientID%>").value;


        var str25 = document.getElementById("<%=txtDisPercent.ClientID%>").value;
        var str26 = document.getElementById("<%=txtDisAmt.ClientID%>").value;


        var str31 = document.getElementById("<%=txtBillingAmount.ClientID%>").value;

        var str32 = document.getElementById("<%=txtTotalArea.ClientID%>").value;
        var str33 = document.getElementById("<%=txtCompletedArea.ClientID%>").value;
        var str34 = document.getElementById("<%=txtBalanceArea.ClientID%>").value;
        var str35 = document.getElementById("<%=txtPortfolioValue.ClientID%>").value;

        var str36 = document.getElementById("<%=txtRetentionPerc.ClientID%>").value;
        var str37 = document.getElementById("<%=txtRetentionValue.ClientID%>").value;


        if (str != "" && isNaN(str)) {

            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Enter Only Numbers for Agreed Value";
              ResetScrollPosition();
              document.getElementById("<%=txtAgreeVal.ClientID%>").focus();
                valid = false;
            }

            if (str1 != "" && isNaN(str1)) {
                //alert("Enter Only Numbers for Allocated Time");

                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Enter Only Numbers for Allocated Time";
                ResetScrollPosition();
                document.getElementById("<%=txtAllocTime.ClientID%>").focus();
                valid = false;
            }

            if (str2 != "" && isNaN(str2)) {
                //alert("Enter Only Numbers for Value per month");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Enter Only Numbers for Value per month";
                ResetScrollPosition();
                document.getElementById("<%=txtValPerMnth.ClientID%>").focus();
                valid = false;
            }
            if (str3 != "" && isNaN(str3)) {
                //alert("Enter Only Numbers for Duration");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Enter Only Numbers for Duration";
                ResetScrollPosition();
                document.getElementById("<%=txtDuration.ClientID%>").focus();
                valid = false;
            }

        //if (str4 != "" && isNaN(str4)) {
        //    alert("Enter Only Numbers for No. of Services Tot. Amt.");
        //    valid = false;
        //}
        //if (str5 != "" && isNaN(str5)) {
        //    alert("Enter Only Numbers for No. of Services Tot. Services");
        //    valid = false;
        //}
        //if (str6 != "" && isNaN(str6)) {
        //    alert("Enter Only Numbers for No. of Services Completed");
        //    valid = false;
        //}
        //if (str7 != "" && isNaN(str7)) {
        //    alert("Enter Only Numbers for No. of Services Balance");
        //    valid = false;
        //}

        //if (str8 != "" && isNaN(str8)) {
        //    alert("Enter Only Numbers for No. of Hours Tot. Amt.");
        //    valid = false;
        //}
        //if (str9 != "" && isNaN(str9)) {
        //    alert("Enter Only Numbers for No. of Hours Tot. Services");
        //    valid = false;
        //}
        //if (str10 != "" && isNaN(str10)) {
        //    alert("Enter Only Numbers for No. of Hours Service Completed");
        //    valid = false;
        //}
        //if (str11 != "" && isNaN(str11)) {
        //    alert("Enter Only Numbers for No. of Hours Service Balance");
        //    valid = false;
        //}

        //if (str12 != "" && isNaN(str12)) {
        //    alert("Enter Only Numbers for No. of Phone Calls Tot. Amt.");
        //    valid = false;
        //}
        //if (str13 != "" && isNaN(str13)) {
        //    alert("Enter Only Numbers for No. of Phone Calls Tot. Services");
        //    valid = false;
        //}
        //if (str14 != "" && isNaN(str14)) {
        //    alert("Enter Only Numbers for No. of Phone Calls Service Completed");
        //    valid = false;
        //}
        //if (str15 != "" && isNaN(str15)) {
        //    alert("Enter Only Numbers for No. of Phone Calls Service Balance");
        //    valid = false;
        //}

        //if (str16 != "" && isNaN(str16)) {
        //    alert("Enter Only Numbers for No. of Units Tot. Amt.");
        //    valid = false;
        //}
        //if (str17 != "" && isNaN(str17)) {
        //    alert("Enter Only Numbers for No. of Units Tot. Services");
        //    valid = false;
        //}
        //if (str18 != "" && isNaN(str18)) {
        //    alert("Enter Only Numbers for No. of Units Service Completed");
        //    valid = false;
        //}
        //if (str19 != "" && isNaN(str19)) {
        //    alert("Enter Only Numbers for No. of Units Service Balance");
        //    valid = false;
        //}

        //if (str20 != "" && isNaN(str20)) {
        //    alert("Enter Only Numbers for Unexpired Compensation Balance Tot. Service");
        //    valid = false;
        //}
        //if (str21 != "" && isNaN(str21)) {
        //    alert("Enter Only Numbers for Unexpired Compensation Balance");
        //    valid = false;
        //}

        //if (str22 != "" && isNaN(str22)) {
        //    alert("Enter Only Numbers for Response");
        //    valid = false;
        //}

        //if (str23 != "" && isNaN(str23)) {
        //    alert("Enter Only Numbers for Cancel Charges");
        //    valid = false;
        //}
        //if (str24 != "" && isNaN(str24)) {
        //    alert("Enter Only Numbers for Min. Duration");
        //    valid = false;
        //}

        //if (str27 != "" && isNaN(str27)) {
        //    alert("Enter Only Numbers for Quote Price");
        //    valid = false;
        //}

        //   if (str28 != "" && isNaN(str28)) {
        //       alert("Enter Only Numbers for Contract Det. Value");
        //       valid = false;
        //   }
        //if (str29 != "" && isNaN(str29)) {
        //    alert("Enter Only Numbers for Per Service Value");
        //    valid = false;
        //}

            if (str25 != "" && isNaN(str25)) {
                //alert("Enter Only Numbers for Discount Percent");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Enter Only Numbers for Discount Percent";
                ResetScrollPosition();
                document.getElementById("<%=txtDisPercent.ClientID%>").focus();
                valid = false;
            }

            if (str26 != "" && isNaN(str26)) {
                //alert("Enter Only Numbers for Discount Amount");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Enter Only Numbers for Discount Amount";
                ResetScrollPosition();
                document.getElementById("<%=txtDisAmt.ClientID%>").focus();
                valid = false;
            }

        //   if (str30 != "" && isNaN(str30)) {
        //       alert("Enter Only Numbers for Total Contract Value");
        //       valid = false;
        //   }

            if (str31 != "" && isNaN(str31)) {
                //alert("Enter Only Numbers for Billing Amount");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Enter Only Numbers for Billing Amount";
                ResetScrollPosition();
                document.getElementById("<%=txtBillingAmount.ClientID%>").focus();
                valid = false;
            }


            if (str32 != "" && isNaN(str32)) {
                //alert("Enter Only Numbers for Billing Amount");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Enter Only Numbers for Total Area";
                ResetScrollPosition();
                document.getElementById("<%=txtBillingAmount.ClientID%>").focus();
                valid = false;
            }

            if (str33 != "" && isNaN(str33)) {
                //alert("Enter Only Numbers for Billing Amount");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Enter Only Numbers for Area Completed";
                ResetScrollPosition();
                document.getElementById("<%=txtBillingAmount.ClientID%>").focus();
                valid = false;
            }

            if (str34 != "" && isNaN(str34)) {
                //alert("Enter Only Numbers for Billing Amount");
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Enter Only Numbers for Balance Area";
                ResetScrollPosition();
                document.getElementById("<%=txtBillingAmount.ClientID%>").focus();
                valid = false;
            }
        //
            if (str35 != "" && isNaN(str35)) {

                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Enter Only Numbers for Portfolio Value";
                ResetScrollPosition();
                document.getElementById("<%=txtPortfolioValue.ClientID%>").focus();
                valid = false;
            }

            if (str36 != "" && isNaN(str36)) {
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Enter Only Numbers for Retention Percent";
                ResetScrollPosition();
                document.getElementById("<%=txtRetentionPerc.ClientID%>").focus();
                valid = false;
            }

            if (str37 != "" && isNaN(str37)) {
                document.getElementById("<%=lblAlert.ClientID%>").innerText = "Enter Only Numbers for Retention Amount";
                  ResetScrollPosition();
                  document.getElementById("<%=txtRetentionValue.ClientID%>").focus();
                 valid = false;
             }
             currentdatetime();
           
             return valid;
         };


        function CalculateRateSchedule() {
      
            var priceperservice= document.getElementById("<%=txtPricePerServiceRateSchedule.ClientID%>").value;
            var discountpercent = document.getElementById("<%=txtDiscountPercentageRateSchedule.ClientID%>").value;
            var finalpriceperservice;
            

           
            if (priceperservice == '')
                priceperservice = 0.00;
            if (discountpercent == '')
                discountpercent = 0.00;


         
            finalpriceperservice = priceperservice * discountpercent * .01;
            
            document.getElementById("<%=txtFinalPricePerServiceRateSchedule.ClientID%>").value = priceperservice - finalpriceperservice;

        }
         function DoValidationSvc() {

             var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency
             var valid = true;

             currentdatetime();


             //if (++submit1 > 1) {
             //    alert('Saving the Contract Detail is in progress.. Please wait.');
             //    submit1 = 0;
             //    valid = false;
             //    return valid;
             //}

             var scheduleType = document.getElementById("<%=ddlScheduleType.ClientID%>").options[document.getElementById("<%=ddlScheduleType.ClientID%>").selectedIndex].text;

        if (scheduleType == '--SELECT--') {
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Schedule Type";
                ResetScrollPosition();
                document.getElementById("<%=ddlScheduleType.ClientID%>").focus();
                //alert("Please Select Schedule Type");
                valid = false;
                return valid;
            }

        //       var team = document.getElementById("<%=txtTeam.ClientID%>").value;

        //        if (team == '') {

        //            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Team";
        //             ResetScrollPosition();
        //             document.getElementById("<%=txtTeam.ClientID%>").focus();
        //             valid = false;
        //             return valid;
        //         }



        var allocatime = document.getElementById("<%=txtAllocTime.ClientID%>").value;

        if ((allocatime == '') || (allocatime == 0)) {
            //alert("Please Select Team");
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Allocated Time";
             ResetScrollPosition();

             valid = false;
             return valid;
         }

             var teamidmandatory = document.getElementById("<%=txtTeamIDMandatory.ClientID%>").value;
             var teamid = document.getElementById("<%=txtTeam.ClientID%>").value;

         
         
             if (teamidmandatory == "1") {
              
                 if ((teamid.trim() == '')) {
                 
                     //alert("Please Select Team");
                     document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Team ID";
                     ResetScrollPosition();

                     valid = false;
                     return valid;
                 }
             }

             var servicebydmandatory = document.getElementById("<%=txtServiceByMandatory.ClientID%>").value;
             var serviceby = document.getElementById("<%=txtServiceBy.ClientID%>").value;

             if (servicebydmandatory == "1") {
                 if ((serviceby.trim() == '')) {
                     //alert("Please Select Team");
                     document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Service By";
                     ResetScrollPosition();

                     valid = false;
                     return valid;
                 }
             }


         var location = document.getElementById("<%=txtLocationId.ClientID%>").value;

        if (location == '') {
            //alert("Please Select Team");
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Service Location";
             ResetScrollPosition();

             valid = false;
             return valid;
         }


         var service = document.getElementById("<%=txtServiceId.ClientID%>").value;

        if (service == '') {
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Service";
             ResetScrollPosition();

             valid = false;
             return valid;
         }


         var frequency = document.getElementById("<%=txtFrequency.ClientID%>").value;

        if (frequency == '') {
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Service Frequency";
             ResetScrollPosition();

             valid = false;
             return valid;
         }
         return valid;
     }


        function ViewFixedContinuous()
        {
            var e = document.getElementById("<%=txtFixedContinuousVisible.ClientID%>").value;
            // alert(e);

        
            if (e == "1") 
            {
                document.getElementById("<%=rbtFixedContinuous.ClientID%>").style.display = 'inline';
                document.getElementById("<%=lblDurationType.ClientID%>").style.display = 'inline';
            }
        else{
                document.getElementById("<%=rbtFixedContinuous.ClientID%>").style.display = 'inline';
                document.getElementById("<%=lblDurationType.ClientID%>").style.display = 'inline';
            }
        }


     function currentdatetimecontract() {
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
         document.getElementById("<%=txtContractDate.ClientID%>").value = dd + "/" + mm + "/" + y;
         document.getElementById("<%=txtContractStart.ClientID%>").value = dd + "/" + mm + "/" + y;
         document.getElementById("<%=txtServStart.ClientID%>").value = dd + "/" + mm + "/" + y;
         //   document.getElementById("<%=txtWarrStart.ClientID%>").value = dd + "/" + mm + "/" + y;
        // document.getElementById("<%=txtActualEndChSt.ClientID%>").value = dd + "/" + mm + "/" + y;

         submit = 0;
         submit1 = 0;
     }

        //function allowOnlyNumber(evt) {
        //    var charCode = (evt.which) ? evt.which : event.keyCode
        //    if (charCode > 31 && (charCode < 48 || charCode > 57))
        //        return false;
        //    return true;
        //}

        function allowOnlyNumber(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 46) {
                var inputValue = $("#inputfield").val()
                if (inputValue.indexOf('.') < 1) {
                    return true;
                }
                return false;
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
            function RefreshSubmit() {
                submit = 0;
                submit1 = 0;
            }

            function PopulateSegment() {
                var e = document.getElementById("<%=ddlIndustry.ClientID%>");
                var value = e.options[e.selectedIndex].value;
                var text = e.options[e.selectedIndex].text;


                chkBoxCount[7].onclick = function () {
                    if (chkBoxCount[1].checked) {
                        alert("hi");
                    }
                }

            }

            function ClearSearch() {
                document.getElementById("<%=txtContractnoSearch.ClientID%>").value = "";
                document.getElementById("<%=txtAccountIdSearch.ClientID%>").value = "";

                document.getElementById("<%=txtClientNameSearch.ClientID%>").value = "";
                document.getElementById("<%=txtSearch1Status.ClientID%>").value = "O,P,H,R";
                document.getElementById("<%=txt.ClientID%>").value = "";

            }

      

            function statuschange() {


                var DropdownList = document.getElementById('<%=ddlStatusChSt.ClientID%>');
                var DropdownList1 = document.getElementById('<%=ddlTerminationCode.ClientID%>');
                //var SelectedIndex = DropdownList.selectedIndex;
                //var SelectedValue = DropdownList.value;
                var SelectedText = DropdownList.options[DropdownList.selectedIndex].text;

                //       alert(SelectedText);
                // alert(Left(SelectedText, 1));

                if (Left(SelectedText, 1) == 'T') {

         
                    document.getElementById("<%=lblActualEnd.ClientID%>").innerHTML = 'Actual End';

                    document.getElementById("<%=lblTermCode.ClientID%>").style.display = 'inline';
                    document.getElementById("<%=Label62.ClientID%>").style.display = 'inline';
                    document.getElementById("<%=ddlTerminationCode.ClientID%>").style.display = 'inline';

                    document.getElementById("<%=lblCommetsChSt.ClientID%>").style.display = 'inline';
                    document.getElementById("<%=txtCommentChSt.ClientID%>").style.display = 'inline';
                    document.getElementById("<%=Label66.ClientID%>").style.display = 'inline';

          
                    document.getElementById("<%=lblOnHoldCode.ClientID%>").style.display = 'none';
                    document.getElementById("<%=ddlOnHoldCodeChSt.ClientID%>").style.display = 'none';
                    document.getElementById("<%=lblOnHoldDescription.ClientID%>").style.display = 'none';
                    document.getElementById("<%=txtOnHoldDscriptionChSt.ClientID%>").style.display = 'none';
                    document.getElementById("<%=lblOnHoldComments.ClientID%>").style.display = 'none';
                    document.getElementById("<%=txtOnHoldCommentsChSt.ClientID%>").style.display = 'none';
                    document.getElementById("<%=label67.ClientID%>").style.display = 'none';
                    document.getElementById("<%=Label68.ClientID%>").style.display = 'none';

                    document.getElementById("<%=txtOnHoldDscriptionChSt.ClientID%>").value = "";
                    document.getElementById("<%=txtOnHoldCommentsChSt.ClientID%>").value = "";
                }

                if (Left(SelectedText, 1) == 'H') {
         
                    document.getElementById("<%=lblActualEnd.ClientID%>").innerHTML = 'OnHold Date';

                    document.getElementById("<%=lblTermCode.ClientID%>").style.display = 'none';
                    document.getElementById("<%=Label62.ClientID%>").style.display = 'none';
                    document.getElementById("<%=ddlTerminationCode.ClientID%>").style.display = 'none';

                    document.getElementById("<%=lblCommetsChSt.ClientID%>").style.display = 'none';
                    document.getElementById("<%=txtCommentChSt.ClientID%>").style.display = 'none';
                    document.getElementById("<%=Label66.ClientID%>").style.display = 'none';

                    document.getElementById("<%=lblOnHoldCode.ClientID%>").style.display = 'inline';
                    document.getElementById("<%=ddlOnHoldCodeChSt.ClientID%>").style.display = 'inline';
                    document.getElementById("<%=lblOnHoldDescription.ClientID%>").style.display = 'inline';
                    document.getElementById("<%=txtOnHoldDscriptionChSt.ClientID%>").style.display = 'inline';
                    document.getElementById("<%=lblOnHoldComments.ClientID%>").style.display = 'inline';
                    document.getElementById("<%=txtOnHoldCommentsChSt.ClientID%>").style.display = 'inline';

                    document.getElementById("<%=ddlOnHoldCodeChSt.ClientID%>").removeAttribute("disabled");
             
                    document.getElementById("<%=txtOnHoldCommentsChSt.ClientID%>").removeAttribute("disabled");
                    document.getElementById("<%=label67.ClientID%>").style.display = 'inline';
                    document.getElementById("<%=Label68.ClientID%>").style.display = 'inline';
                }

                if (Left(SelectedText, 1) == 'X') {
                    document.getElementById("<%=lblActualEnd.ClientID%>").style.display = 'none';
                    document.getElementById("<%=txtActualEndChSt.ClientID%>").style.display = 'none';
                    document.getElementById("<%=Label29.ClientID%>").style.display = 'none';

                    document.getElementById("<%=lblCommetsChSt.ClientID%>").style.display = 'none';
                    document.getElementById("<%=txtCommentChSt.ClientID%>").style.display = 'none';
                    document.getElementById("<%=Label66.ClientID%>").style.display = 'none';

                    document.getElementById("<%=lblOnHoldCode.ClientID%>").style.display = 'none';
                    document.getElementById("<%=ddlOnHoldCodeChSt.ClientID%>").style.display = 'none';
                    document.getElementById("<%=lblOnHoldDescription.ClientID%>").style.display = 'none';
                    document.getElementById("<%=txtOnHoldDscriptionChSt.ClientID%>").style.display = 'none';
                    document.getElementById("<%=lblOnHoldComments.ClientID%>").style.display = 'none';
                    document.getElementById("<%=txtOnHoldCommentsChSt.ClientID%>").style.display = 'none';
                }

                if (Left(SelectedText, 1) == 'V') {
                    document.getElementById("<%=lblActualEnd.ClientID%>").style.display = 'none';
                    document.getElementById("<%=txtActualEndChSt.ClientID%>").style.display = 'none';
                    document.getElementById("<%=Label29.ClientID%>").style.display = 'none';

                    document.getElementById("<%=lblCommetsChSt.ClientID%>").style.display = 'none';
                    document.getElementById("<%=txtCommentChSt.ClientID%>").style.display = 'none';
                    document.getElementById("<%=Label66.ClientID%>").style.display = 'none';

                    document.getElementById("<%=lblOnHoldCode.ClientID%>").style.display = 'none';
                    document.getElementById("<%=ddlOnHoldCodeChSt.ClientID%>").style.display = 'none';
                    document.getElementById("<%=lblOnHoldDescription.ClientID%>").style.display = 'none';
                    document.getElementById("<%=txtOnHoldDscriptionChSt.ClientID%>").style.display = 'none';
                    document.getElementById("<%=lblOnHoldComments.ClientID%>").style.display = 'none';
                    document.getElementById("<%=txtOnHoldCommentsChSt.ClientID%>").style.display = 'none';
                    document.getElementById("<%=label67.ClientID%>").style.display = 'none';
                    document.getElementById("<%=Label68.ClientID%>").style.display = 'none';
                }

                if (Left(SelectedText, 1) == 'O') {
        
                    document.getElementById("<%=lblActualEnd.ClientID%>").innerHTML = 'Resume Date';

                    document.getElementById("<%=lblTermCode.ClientID%>").style.display = 'none';
                    document.getElementById("<%=Label62.ClientID%>").style.display = 'none';
                    document.getElementById("<%=ddlTerminationCode.ClientID%>").style.display = 'none';

                    document.getElementById("<%=lblCommetsChSt.ClientID%>").style.display = 'none';
                    document.getElementById("<%=txtCommentChSt.ClientID%>").style.display = 'none';
                    document.getElementById("<%=Label66.ClientID%>").style.display = 'none';

                  
                   if (Left(document.getElementById("<%=txtStatus.ClientID%>").value, 1) == 'H')
                    {
               

                        document.getElementById("<%=lblOnHoldCode.ClientID%>").style.display = 'none';
                        document.getElementById("<%=ddlOnHoldCodeChSt.ClientID%>").style.display = 'none';
                        document.getElementById("<%=lblOnHoldDescription.ClientID%>").style.display = 'none';
                        document.getElementById("<%=txtOnHoldDscriptionChSt.ClientID%>").style.display = 'none';
                        document.getElementById("<%=lblOnHoldComments.ClientID%>").style.display = 'none';
                        document.getElementById("<%=txtOnHoldCommentsChSt.ClientID%>").style.display = 'none';

                        document.getElementById("<%=ddlOnHoldCodeChSt.ClientID%>").removeAttribute("disabled");

                        document.getElementById("<%=txtOnHoldCommentsChSt.ClientID%>").removeAttribute("disabled");
                        document.getElementById("<%=label67.ClientID%>").style.display = 'none';
                        document.getElementById("<%=Label68.ClientID%>").style.display = 'none';
                    }
                    else
                    {
             

                        document.getElementById("<%=lblOnHoldCode.ClientID%>").style.display = 'none';
                        document.getElementById("<%=ddlOnHoldCodeChSt.ClientID%>").style.display = 'none';
                        document.getElementById("<%=lblOnHoldDescription.ClientID%>").style.display = 'none';
                        document.getElementById("<%=txtOnHoldDscriptionChSt.ClientID%>").style.display = 'none';
                        document.getElementById("<%=lblOnHoldComments.ClientID%>").style.display = 'none';
                        document.getElementById("<%=txtOnHoldCommentsChSt.ClientID%>").style.display = 'none';
                        document.getElementById("<%=label67.ClientID%>").style.display = 'none';
                    }
                }

                if ((Left(SelectedText, 1) == 'T') || (Left(SelectedText, 1) == 'C') || (Left(SelectedText, 1) == 'E') || (Left(SelectedText, 1) == 'X') || (Left(SelectedText, 1) == 'R'))
                    document.getElementById("<%=ddlTerminationCode.ClientID%>").removeAttribute("disabled");
                else {
                    document.getElementById("<%=ddlTerminationCode.ClientID%>").setAttribute("disabled", "true");
                    DropdownList1.selectedIndex = 0;
                    document.getElementById("<%=txtAlertStatus.ClientID%>").value = "";
                    document.getElementById("<%=txtAlertStatus.ClientID%>").style.display = 'none';
                }

                //      var currentTime = new Date();

                //      var dd = currentTime.getDate();
                //       var mm = currentTime.getMonth() + 1;
                //      var y = currentTime.getFullYear();

                //      var hh = currentTime.getHours();
                //      var MM = currentTime.getMinutes();
                //      var ss = currentTime.getSeconds();

                //      if (dd < 10) {
                //         dd = "0" + dd;
                //      }
                //     if (mm < 10)
                //         mm = "0" + mm;

                //     var strct = y + "/" + mm + "/" + dd + " " + hh + ":" + MM + ":" + ss;
                //     var endct = new Date(strct);
                //     document.getElementById("<%=txtActualEndChSt.ClientID%>").value = dd + "/" + mm + "/" + y;

            }


            function statuschangerevise() {
                document.getElementById("<%=ddlTerminationCode.ClientID%>").removeAttribute("disabled");

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
            //    document.getElementById("<%=txtActualEndChSt.ClientID%>").value = dd + "/" + mm + "/" + y;
            }


            function LoadDefaultDates() {

                var dur = 1;
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

                endct.setDate(endct.getDate() - 1);

                var dd = endct.getDate();
                var mm = endct.getMonth() + 1;
                var y = endct.getFullYear() + (dur);

                if (dd < 10) {
                    dd = "0" + dd;
                }
                if (mm < 10)
                    mm = "0" + mm;

                if (mm == "02") {
                    if (y % 4 == 0) {
                        if ((dd == 30) || (dd == 31)) {
                            dd = 29;
                        }
                    }
                    else if ((dd == 29) || (dd == 30) || (dd == 31)) {
                        dd = 28;
                    }

                }

                if ((mm == "04") || (mm == "06") || (mm == "09") || (mm == "11")) {
                    if (dd == 31) {
                        dd = 30;
                    }

                }


                document.getElementById("<%=txtContractEnd.ClientID%>").value = dd + "/" + mm + "/" + y;
                document.getElementById("<%=txtServStart.ClientID%>").value = document.getElementById("<%=txtContractStart.ClientID%>").value;
                document.getElementById("<%=txtServEnd.ClientID%>").value = dd + "/" + mm + "/" + y;


                var dateone = (document.getElementById("<%=txtServStart.ClientID%>").value);
                var datetwo = (document.getElementById("<%=txtServEnd.ClientID%>").value);


                if (dateone != "") {

                    var timein = dateone.split('/');
                    var startdate = parseInt(timein[0], 10);
                    var startmonth = parseInt(timein[1], 10);
                    var startyear = parseInt(timein[2], 10);

                    var str3 = startyear + "/" + startmonth + "/" + startdate + " 10:00:00";
                    var startTime = new Date(str3);

                    var weekdaystart = startTime.getDay();

                    if (weekdaystart == 1)
                        weekdaystart = "MONDAY";
                    else if (weekdaystart == 2)
                        weekdaystart = "TUESDAY";
                    else if (weekdaystart == 3)
                        weekdaystart = "WEDNESDAY";
                    else if (weekdaystart == 4)
                        weekdaystart = "THURSDAY";
                    else if (weekdaystart == 5)
                        weekdaystart = "FRIDAY";
                    else if (weekdaystart == 6)
                        weekdaystart = "SATURDAY";
                    else if (weekdaystart == 0)
                        weekdaystart = "SUNDAY";
                    document.getElementById("<%=txtServStartDay.ClientID%>").value = weekdaystart;

                }

                if (datetwo != "") {

                    var timeout = datetwo.split('/');
                    var enddate = parseInt(timeout[0], 10);
                    var endmonth = parseInt(timeout[1], 10);
                    var endyear = parseInt(timeout[2], 10);

                    var str4 = endyear + "/" + endmonth + "/" + enddate + " 10:00:00";
                    var endTime = new Date(str4);

                    var weekdayend = endTime.getDay();

                    if (weekdayend == 1)
                        weekdayend = "MONDAY";
                    else if (weekdayend == 2)
                        weekdayend = "TUESDAY";
                    else if (weekdayend == 3)
                        weekdayend = "WEDNESDAY";
                    else if (weekdayend == 4)
                        weekdayend = "THURSDAY";
                    else if (weekdayend == 5)
                        weekdayend = "FRIDAY";
                    else if (weekdayend == 6)
                        weekdayend = "SATURDAY";
                    else if (weekdayend == 0)
                        weekdayend = "SUNDAY";
                    document.getElementById("<%=TxtServEndDay.ClientID%>").value = weekdayend;

                }

                var months;
                var year1 = startTime.getFullYear();
                var year2 = endTime.getFullYear();
                var month1 = startTime.getMonth();
                var month2 = endTime.getMonth() + 1;

                if (month1 === 0) {
                    month1++;
                    month2++;
                }
                var numberOfMonths;
                numberOfMonths = (year2 - year1) * 12 + (month2 - month1) - 1;
                (document.getElementById("<%=txtNoofMonth.ClientID%>").value) = numberOfMonths;


                // 01.02.17

                var dur = (document.getElementById("<%=txtDuration.ClientID%>").value);

                if (dur != "") {
                    var dur = parseInt(document.getElementById("<%=txtDuration.ClientID%>").value);

                    var optionselected;
                    var durationms = document.getElementById("<%=rbtLstDuration.ClientID%>");
                    var radio = durationms.getElementsByTagName("input");
                    for (var i = 0; i < radio.length; i++) {
                        if (radio[i].checked) {
                            optionselected = i;
                            break;
                        }
                    }


                    if (optionselected == 2) {
                        (document.getElementById("<%=txtNoofMonth.ClientID%>").value) = dur;
                    }

                    else if (optionselected == 3) {
                        (document.getElementById("<%=txtNoofMonth.ClientID%>").value) = dur * 12;
                    }
                }
                //01.02.17
                ViewFixedContinuous();
            }

            var defaultTextClient = "Search Here for AccountID or Client Name or Contact Person";
            function WaterMarkClient(txt, evt) {
                if (txt.value.length == 0 && evt.type == "blur") {
                    txt.style.color = "gray";
                    txt.value = defaultTextClient;
                }
                if (txt.value == defaultTextClient && evt.type == "focus") {
                    txt.style.color = "black";
                    txt.value = "";
                }
            }


            var defaultTextTeam = "Search Here for Team or In-ChargeId";
            function WaterMarkTeam(txt, evt) {
                if (txt.value.length == 0 && evt.type == "blur") {
                    txt.style.color = "gray";
                    txt.value = defaultTextTeam;
                }
                if (txt.value == defaultTextTeam && evt.type == "focus") {
                    txt.style.color = "black";
                    txt.value = "";
                }
            }
            function ConfirmSelection() {

                var origAcid = document.getElementById("<%=txtOriginalAccountId.ClientID%>").value;
                if (origAcid == '') {

                }
                else {

                    var confirm_value = document.createElement("INPUT");
                    confirm_value.type = "hidden";
                    confirm_value.name = "confirm_value";
                    if (confirm("If you select different Account ID, Contract Details will be Deleted. Do you wish to continue?")) {
                        confirm_value.value = "Yes";
                    } else {
                        confirm_value.value = "No";
                    }
                    document.forms[0].appendChild(confirm_value);
                }
            }

            function getRetentionAmount() {

                var retentionperc = document.getElementById("<%=txtRetentionPerc.ClientID%>").value;
                var agreeval = document.getElementById("<%=txtAgreeVal.ClientID%>").value;


                (document.getElementById("<%=txtRetentionValue.ClientID%>").value) = ((agreeval * retentionperc) * .01).toFixed(2);

            }


            function getContactType() {

                document.getElementById("<%=txtclientid.ClientID%>").value = '';
                //document.getElementById("<%=txtAccountId.ClientID%>").value = '';
                document.getElementById("<%=txtCustName.ClientID%>").value = '';
                document.getElementById("<%=txtOfficeAddress.ClientID%>").value = '';
                document.getElementById("<%=txtPostal.ClientID%>").value = '';
                document.getElementById("<%=ddlIndustry.ClientID%>").value = '';
                document.getElementById("<%=ddlMarketSegmentID.ClientID%>").value = '';

                var contacttype = document.getElementById("<%=ddlContactType.ClientID%>").options[document.getElementById("<%=ddlContactType.ClientID%>").selectedIndex].text;

                if (contacttype == "CORPORATE" || contacttype == "COMPANY") {
                    (document.getElementById("<%=txtContType1.ClientID%>").value) = "CORPORATE";
                    (document.getElementById("<%=txtContType2.ClientID%>").value) = "COMPANY";
                    (document.getElementById("<%=txtContType3.ClientID%>").value) = "RESIDENTIAL";
                    (document.getElementById("<%=txtContType4.ClientID%>").value) = "PERSON";
                }
                else if (contacttype == "RESIDENTIAL" || contacttype == "PERSON") {
                    (document.getElementById("<%=txtContType1.ClientID%>").value) = "RESIDENTIAL";
                    (document.getElementById("<%=txtContType2.ClientID%>").value) = "PERSON";
                    (document.getElementById("<%=txtContType3.ClientID%>").value) = "CORPORATE";
                    (document.getElementById("<%=txtContType4.ClientID%>").value) = "COMPANY";
                }

            }

            function EnableDisableWarranty() {

                var withwarranty = document.getElementById("<%=ChkWithWarranty.ClientID%>").checked;
                //alert(withwarranty);

                if (withwarranty == false) {

                    document.getElementById("<%=txtWarrDurtion.ClientID%>").setAttribute("disabled", "true");
                    document.getElementById("<%=txtWarrStart.ClientID%>").setAttribute("disabled", "true");
                    document.getElementById("<%=txtWarrEnd.ClientID%>").setAttribute("disabled", "true");
                    document.getElementById("<%=ChkRequireInspection.ClientID%>").setAttribute("disabled", "true");
                    document.getElementById("<%=ddlInspectionFrequency.ClientID%>").setAttribute("disabled", "true");
                    document.getElementById("<%=TxtInspectionStart.ClientID%>").setAttribute("disabled", "true");
                    document.getElementById("<%=TxtInspectionEnd.ClientID%>").setAttribute("disabled", "true");

                    document.getElementById("<%=ddlWarrantyFrequency.ClientID%>").setAttribute("disabled", "true");
                    document.getElementById("<%=txtWarrantyBillingAmount.ClientID%>").setAttribute("disabled", "true");

                    document.getElementById("<%=txtWarrDurtion.ClientID%>").value = "";
                    document.getElementById("<%=txtWarrStart.ClientID%>").value = "";
                    document.getElementById("<%=txtWarrEnd.ClientID%>").value = "";
                    document.getElementById("<%=ChkRequireInspection.ClientID%>").checked = false;
                    document.getElementById("<%=ddlInspectionFrequency.ClientID%>").value = "--SELECT--";
                    document.getElementById("<%=TxtInspectionStart.ClientID%>").value = "";
                    document.getElementById("<%=TxtInspectionEnd.ClientID%>").value = "";

                    document.getElementById("<%=ddlWarrantyFrequency.ClientID%>").value = "--SELECT--";
                    document.getElementById("<%=txtWarrantyBillingAmount.ClientID%>").value = "";
                }
                else if (withwarranty == true) {

                    document.getElementById("<%=txtWarrDurtion.ClientID%>").removeAttribute("disabled");
                    document.getElementById("<%=txtWarrStart.ClientID%>").removeAttribute("disabled");
                    document.getElementById("<%=txtWarrEnd.ClientID%>").removeAttribute("disabled");
                    document.getElementById("<%=ChkRequireInspection.ClientID%>").removeAttribute("disabled");

                    document.getElementById("<%=ddlWarrantyFrequency.ClientID%>").removeAttribute("disabled");
                    document.getElementById("<%=txtWarrantyBillingAmount.ClientID%>").removeAttribute("disabled");
                }


            }


            function EnableDisableInspection() {

                var withinspection = document.getElementById("<%=ChkRequireInspection.ClientID%>").checked;
                //alert(withwarranty);

                if (withinspection == false) {

                    document.getElementById("<%=ddlInspectionFrequency.ClientID%>").setAttribute("disabled", "true");
                    document.getElementById("<%=TxtInspectionStart.ClientID%>").setAttribute("disabled", "true");
                    document.getElementById("<%=TxtInspectionEnd.ClientID%>").setAttribute("disabled", "true");
                }
                else if (withinspection == true) {

                    document.getElementById("<%=ddlInspectionFrequency.ClientID%>").removeAttribute("disabled");
                    document.getElementById("<%=TxtInspectionStart.ClientID%>").removeAttribute("disabled");
                    document.getElementById("<%=TxtInspectionEnd.ClientID%>").removeAttribute("disabled");
                }


            }



            function EnableDisableStatusSearch() {

                var statussearch = document.getElementById("<%=chkSearchAll.ClientID%>").checked;

                if (statussearch == false) {

                    var cbl = document.getElementById('<%=chkStatusSearch.ClientID%>').getElementsByTagName("input");
                    for (i = 0; i < cbl.length; i++) cbl[i].checked = false;

                }
                else if (statussearch == true) {

                    var cbl = document.getElementById('<%=chkStatusSearch.ClientID%>').getElementsByTagName("input");
                    for (i = 0; i < cbl.length; i++) cbl[i].checked = true;

                }
            }


            function TabChanged(sender, e) {
                if (sender.get_activeTabIndex() == 1) {
                    if (document.getElementById("<%=txtContractNo.ClientID%>").value == '') {
                        $find('<%=tb1.ClientID%>').set_activeTabIndex(0);
                        alert("Please select a Contract record to proceed.");
                        return;
                    }

                    //document.getElementById('<%=GridView1.ClientID()%>').style.display = 'none';

                    document.getElementById("<%=txtServStartSvc.ClientID%>").value = document.getElementById("<%=txtServStart.ClientID%>").value
                    document.getElementById("<%=txtServEndSvc.ClientID%>").value = document.getElementById("<%=txtServEnd.ClientID%>").value

                    document.getElementById("<%=txtServStartDaySvc.ClientID%>").value = document.getElementById("<%=txtServStartDay.ClientID%>").value
                    document.getElementById("<%=txtServEndDaySvc.ClientID%>").value = document.getElementById("<%=TxtServEndDay.ClientID%>").value

                }
                if (sender.get_activeTabIndex() == 2) {
                    if (document.getElementById("<%=txtContractNo.ClientID%>").value == '') {
                        $find('<%=tb1.ClientID%>').set_activeTabIndex(0);
                        alert("Please select a Contract record to proceed.");
                        return;
                    }

                    //document.getElementById('<%=GridView1.ClientID()%>').style.display = 'none';


                }
                else {
                    //document.getElementById('<%=GridView1.ClientID()%>').style.display = 'inline';

                }

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




            function PopulateOtherDays() {

                var dur = document.getElementById("<%=txtDuration.ClientID%>").value;

                if (dur != "") {
                    document.getElementById("<%=txtContractStart.ClientID%>").value = document.getElementById("<%=txtContractDate.ClientID%>").value;
                    document.getElementById("<%=txtServStart.ClientID%>").value = document.getElementById("<%=txtContractStart.ClientID%>").value;
                    //     document.getElementById("<%=txtWarrStart.ClientID%>").value = document.getElementById("<%=txtContractStart.ClientID%>").value;

                    CalculateContractDates();
                    CalculateServiceDates();
                    //      CalculateWarrantyDates();

                }
                else {
                    document.getElementById("<%=txtContractStart.ClientID%>").value = document.getElementById("<%=txtContractDate.ClientID%>").value;
                    document.getElementById("<%=txtServStart.ClientID%>").value = document.getElementById("<%=txtContractStart.ClientID%>").value;
                    //      document.getElementById("<%=txtWarrStart.ClientID%>").value = document.getElementById("<%=txtContractStart.ClientID%>").value;
                }
            }


            function CalculateContractDates() {
                var dateone = (document.getElementById("<%=txtContractStart.ClientID%>").value);
                var dur1 = (document.getElementById("<%=txtDuration.ClientID%>").value);

                if (dur1 == 0)
                    document.getElementById("<%=txtDuration.ClientID%>").value = "1";

                var dur = parseInt(document.getElementById("<%=txtDuration.ClientID%>").value);


                if (dur != "") {
                    var dur = parseInt(document.getElementById("<%=txtDuration.ClientID%>").value);

                    var optionselected;
                    var durationms = document.getElementById("<%=rbtLstDuration.ClientID%>");
                    var radio = durationms.getElementsByTagName("input");
                    for (var i = 0; i < radio.length; i++) {
                        if (radio[i].checked) {
                            optionselected = i;
                            break;
                        }
                    }


                    var optionselected1;
                    var fixedcontinuous = document.getElementById("<%=rbtFixedContinuous.ClientID%>");
                    var radio1 = fixedcontinuous.getElementsByTagName("input");
                    for (var j = 0; j < radio1.length; j++) {
                        if (radio1[j].checked) {
                            optionselected1 = j;
                            break;
                        }
                    }


                    var timein = dateone.split('/');
                    var startdate = parseInt(timein[0], 10);
                    var startmonth = parseInt(timein[1], 10);
                    var startyear = parseInt(timein[2], 10);

                    var str3 = startyear + "/" + startmonth + "/" + startdate + " 10:00:00";
                    var startTime = new Date(str3);
                    var origdate = new Date(str3);


                    if (optionselected1 == 0) {
                        if (optionselected == 0) {

                            if (dur == 1) {
                                startTime.setDate(startTime.getDate());

                                var dd = startTime.getDate();


                                var mm = startTime.getMonth() + 1;
                                var y = startTime.getFullYear();
                            }
                            else {
                                startTime.setDate(startTime.getDate() + dur);

                                var dd = startTime.getDate() - 1;

                                if (dd == 0)
                                    dd = dd + 1;


                                var mm = startTime.getMonth() + 1;
                                var y = startTime.getFullYear();
                            }
                        }

                        else if (optionselected == 1) {
                            startTime.setDate(startTime.getDate() - 1);
                            startTime.setDate(startTime.getDate() + (dur * 7));

                            var dd = startTime.getDate();
                            var mm = startTime.getMonth() + 1;
                            var y = startTime.getFullYear();
                        }

                        else if (optionselected == 2) {
                           
                            startTime.setMonth(startTime.getMonth() + (dur));
                            startTime.setDate(startTime.getDate() - 1);
                            var dd = startTime.getDate();
                            var mm = startTime.getMonth() + 1;
                            var y = startTime.getFullYear();


                        }

                        else if (optionselected == 3) {
                            startTime.setDate(startTime.getDate() - 1);

                            var dd = startTime.getDate();
                            var mm = startTime.getMonth() + 1;
                            var y = startTime.getFullYear() + (dur);
                        }

                        if (dd < 10) {
                            dd = "0" + dd;
                        }
                        if (mm < 10)
                            mm = "0" + mm;

                        if (mm == "02") {
                            if (y % 4 == 0) {
                                if ((dd == 30) || (dd == 31)) {
                                    dd = 29;
                                }
                            }
                            else if ((dd == 29) || (dd == 30) || (dd == 31)) {
                                dd = 28;
                            }

                        }

                        if ((mm == "04") || (mm == "06") || (mm == "09") || (mm == "11")) {
                            if (dd == 31) {
                                dd = 30;
                            }

                        }

                        document.getElementById("<%=txtContractEnd.ClientID%>").value = dd + "/" + mm + "/" + y;
                        document.getElementById("<%=txtServStart.ClientID%>").value = document.getElementById("<%=txtContractStart.ClientID%>").value;
                        document.getElementById("<%=txtServEnd.ClientID%>").value = document.getElementById("<%=txtContractEnd.ClientID%>").value;
                    }


                    //

                    if (optionselected1 == 1) {
                        if (optionselected == 0) {

                            if (dur == 1) {
                                startTime.setDate(startTime.getDate());

                                var dd = startTime.getDate();


                                var mm = startTime.getMonth() + 1;
                                var y = startTime.getFullYear();
                            }
                            else {
                                startTime.setDate(startTime.getDate() + dur);

                                var dd = startTime.getDate() - 1;

                                if (dd == 0)
                                    dd = dd + 1;


                                var mm = startTime.getMonth() + 1;
                                var y = startTime.getFullYear();
                            }
                        }

                        else if (optionselected == 1) {
                            startTime.setDate(startTime.getDate() - 1);
                            startTime.setDate(startTime.getDate() + (dur * 7));

                            var dd = startTime.getDate();
                            var mm = startTime.getMonth() + 1;
                            var y = startTime.getFullYear();
                        }

                        else if (optionselected == 2) {
                            startTime.setDate(startTime.getDate() - 1);
                            startTime.setMonth(startTime.getMonth() + (dur));

                            var dd = startTime.getDate();
                            var mm = startTime.getMonth() + 1;
                            var y = startTime.getFullYear();


                        }

                        else if (optionselected == 3) {
                            startTime.setDate(startTime.getDate() - 1);

                            var dd = startTime.getDate();
                            var mm = startTime.getMonth() + 1;
                            var y = startTime.getFullYear() + (dur);
                        }

                        if (dd < 10) {
                            dd = "0" + dd;
                        }
                        if (mm < 10)
                            mm = "0" + mm;

                        //    alert("1");

                        document.getElementById("<%=txtContractEnd.ClientID%>").value = dd + "/" + mm + "/" + y;
                        document.getElementById("<%=txtServStart.ClientID%>").value = document.getElementById("<%=txtContractStart.ClientID%>").value;
                        document.getElementById("<%=txtServEnd.ClientID%>").value = document.getElementById("<%=txtContractEnd.ClientID%>").value;

                         //   alert("2");

                        document.getElementById("<%=txtContractEnd.ClientID%>").value = "";
                        document.getElementById("<%=txtServEnd.ClientID%>").value = "";
                        document.getElementById("<%=txtEndofLastSchedule.ClientID%>").value = dd + "/" + mm + "/" + y;
                        document.getElementById("<%=TxtServEndDay.ClientID%>").value = "";
                   //     document.getElementById("<%=txtDuration.ClientID%>").value = "0";
                    }

                    //
                }
            }

            function CalculateServiceDates() {
                var dateone = (document.getElementById("<%=txtServStart.ClientID%>").value);
                var dur1 = (document.getElementById("<%=txtDuration.ClientID%>").value);


                if (dur1 == 0)
                    document.getElementById("<%=txtDuration.ClientID%>").value = "1";
                var dur = parseInt(document.getElementById("<%=txtDuration.ClientID%>").value);

                if (dur != "") {
                    var dur = parseInt(document.getElementById("<%=txtDuration.ClientID%>").value);

                    var optionselected;
                    var durationms = document.getElementById("<%=rbtLstDuration.ClientID%>");
                    var radio = durationms.getElementsByTagName("input");
                    for (var i = 0; i < radio.length; i++) {
                        if (radio[i].checked) {
                            optionselected = i;
                            break;
                        }
                    }


                    var optionselected1;
                    var fixedcontinuous = document.getElementById("<%=rbtFixedContinuous.ClientID%>");
                    var radio1 = fixedcontinuous.getElementsByTagName("input");
                    for (var j = 0; j < radio1.length; j++) {
                        if (radio1[j].checked) {
                            optionselected1 = j;
                            break;
                        }
                    }


                    var timein = dateone.split('/');
                    var startdate = parseInt(timein[0], 10);
                    var startmonth = parseInt(timein[1], 10);
                    var startyear = parseInt(timein[2], 10);

                    var str3 = startyear + "/" + startmonth + "/" + startdate + " 10:00:00";
                    var startTime = new Date(str3);
                    var origdate = new Date(str3);


                    if (optionselected == 0) {
                        if (dur == 1) {
                            startTime.setDate(startTime.getDate() );

                            var dd = startTime.getDate() ;
                            var mm = startTime.getMonth() + 1;
                            var y = startTime.getFullYear();
                        }
                        else {
                            startTime.setDate(startTime.getDate() + dur);

                            var dd = startTime.getDate() - 1;
                            var mm = startTime.getMonth() + 1;
                            var y = startTime.getFullYear();
                        }
                    }

                    else if (optionselected == 1) {
                        startTime.setDate(startTime.getDate() - 1);
                        startTime.setDate(startTime.getDate() + (dur * 7));

                        var dd = startTime.getDate();
                        var mm = startTime.getMonth() + 1;
                        var y = startTime.getFullYear();
                    }

                    else if (optionselected == 2) {
                       
                        startTime.setMonth(startTime.getMonth() + (dur));
                        startTime.setDate(startTime.getDate() - 1);

                        var dd = startTime.getDate();
                        var mm = startTime.getMonth() + 1;
                        var y = startTime.getFullYear();


                    }

                    else if (optionselected == 3) {
                        startTime.setDate(startTime.getDate() - 1);

                        var dd = startTime.getDate();
                        var mm = startTime.getMonth() + 1;
                        var y = startTime.getFullYear() + (dur);
                    }

                    if (dd < 10) {
                        dd = "0" + dd;
                    }
                    if (mm < 10)
                        mm = "0" + mm;

                    if (mm == "02") {
                        if (y % 4 == 0) {
                            if ((dd == 30) || (dd == 31)) {
                                dd = 29;
                            }
                        }
                        else if ((dd == 29) || (dd == 30) || (dd == 31)) {
                            dd = 28;
                        }

                    }

                    if ((mm == "04") || (mm == "06") || (mm == "09") || (mm == "11")) {
                        if (dd == 31) {
                            dd = 30;
                        }

                    }


                    var str4 = y + "/" + mm + "/" + dd + " 10:00:00";
                    var endTime = new Date(str4);

                    if (optionselected1 == 0) {
                        document.getElementById("<%=txtServEnd.ClientID%>").value = dd + "/" + mm + "/" + y;
                    }

                    if (optionselected1 == 1) {
                        document.getElementById("<%=txtServEnd.ClientID%>").value = "";
                 //       document.getElementById("<%=txtDuration.ClientID%>").value = "0";
                        document.getElementById("<%=TxtServEndDay.ClientID%>").value = "";
                    }

                    getWeekDay();
                    getValuePerMonth();
                }

            }

            function CalculateWarrantyDates() {
                var dateone = (document.getElementById("<%=txtWarrStart.ClientID%>").value);
                var dur = (document.getElementById("<%=txtWarrDurtion.ClientID%>").value);


                if (dur != "") {
                    var dur = parseInt(document.getElementById("<%=txtWarrDurtion.ClientID%>").value);

                    var optionselected;
                    var durationms = document.getElementById("<%=rbtLstDuration.ClientID%>");
                    var radio = durationms.getElementsByTagName("input");
                    for (var i = 0; i < radio.length; i++) {
                        if (radio[i].checked) {
                            optionselected = i;
                            break;
                        }
                    }

                    var timein = dateone.split('/');
                    var startdate = parseInt(timein[0], 10);
                    var startmonth = parseInt(timein[1], 10);
                    var startyear = parseInt(timein[2], 10);

                    var str3 = startyear + "/" + startmonth + "/" + startdate + " 10:00:00";
                    var startTime = new Date(str3);
                    var origdate = new Date(str3);


                    if (optionselected == 0) {
                        startTime.setDate(startTime.getDate() + dur);

                        var dd = startTime.getDate();
                        var mm = startTime.getMonth() + 1;
                        var y = startTime.getFullYear();
                    }

                    else if (optionselected == 1) {
                        startTime.setDate(startTime.getDate() - 1);
                        startTime.setDate(startTime.getDate() + (dur * 7));

                        var dd = startTime.getDate();
                        var mm = startTime.getMonth() + 1;
                        var y = startTime.getFullYear();
                    }

                    else if (optionselected == 2) {
                        startTime.setDate(startTime.getDate() - 1);
                        startTime.setMonth(startTime.getMonth() + (dur));

                        var dd = startTime.getDate();
                        var mm = startTime.getMonth() + 1;
                        var y = startTime.getFullYear();


                    }

                    else if (optionselected == 3) {
                        startTime.setDate(startTime.getDate() - 1);

                        var dd = startTime.getDate();
                        var mm = startTime.getMonth() + 1;
                        var y = startTime.getFullYear() + (dur);
                    }

                    if (dd < 10) {
                        dd = "0" + dd;
                    }
                    if (mm < 10)
                        mm = "0" + mm;


                    var str4 = y + "/" + mm + "/" + dd + " 10:00:00";
                    var endTime = new Date(str4);

                    //document.getElementById("<%=txtWarrStart.ClientID%>").value = document.getElementById("<%=txtContractStart.ClientID%>").value;

                    if (dateone != "")
                        document.getElementById("<%=txtWarrEnd.ClientID%>").value = dd + "/" + mm + "/" + y;

                }

            }

            function DurationListSelect() {
                //  alert("Hi");
                var chkBoxList = document.getElementById("<%=rbtLstDuration.ClientID()%>");
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                var selected;
                for (var i = 0; i < chkBoxCount.length; i++) {
                    if (chkBoxCount[i].checked) {
                        selected = chkBoxCount[i];
                        break;
                    }
                }
                if (selected) {
                    if (selected.value == "Years") {
                        document.getElementById("<%=ddlPortfolioYN.ClientID%>").value = "1";

                        var noofyears = (document.getElementById("<%=txtDuration.ClientID%>").value);
                        var agreevalue = (document.getElementById("<%=txtAgreeVal.ClientID%>").value);

                        if (noofyears == 0)
                            document.getElementById("<%=txtPortfolioValue.ClientID%>").value = (agreevalue).toFixed(2);
                        else
                            document.getElementById("<%=txtPortfolioValue.ClientID%>").value = (agreevalue / noofyears).toFixed(2);

                    }
                    else if (selected.value == "Months") {

                        var noofmonths = (document.getElementById("<%=txtDuration.ClientID%>").value);
                        var agreevalue = (document.getElementById("<%=txtAgreeVal.ClientID%>").value);
                        var noofyears = Math.floor(noofmonths / 12);

                        if (noofmonths >= 12) {
                            document.getElementById("<%=txtPortfolioValue.ClientID%>").value = (agreevalue / noofyears).toFixed(2);
                            document.getElementById("<%=ddlPortfolioYN.ClientID%>").value = "1";
                        }
                        else {
                            document.getElementById("<%=ddlPortfolioYN.ClientID%>").value = "0";

                            document.getElementById("<%=txtPortfolioValue.ClientID%>").value = "0";
                        }
                    }
                    else if (selected.value == "Weeks") {

                        var noofweeks = (document.getElementById("<%=txtDuration.ClientID%>").value);
                        var agreevalue = (document.getElementById("<%=txtAgreeVal.ClientID%>").value);
                        var noofyears = Math.floor(noofweeks / 52);

                        if (noofweeks >= 52) {
                            document.getElementById("<%=txtPortfolioValue.ClientID%>").value = (agreevalue / noofyears).toFixed(2);
                            document.getElementById("<%=ddlPortfolioYN.ClientID%>").value = "1";
                        }
                        else {
                            document.getElementById("<%=ddlPortfolioYN.ClientID%>").value = "0";

                            document.getElementById("<%=txtPortfolioValue.ClientID%>").value = "0";
                        }
                    }
                    else if (selected.value == "Days") {

                        var noofdays = (document.getElementById("<%=txtDuration.ClientID%>").value);
                        var agreevalue = (document.getElementById("<%=txtAgreeVal.ClientID%>").value);

                        if (noofdays >= 365) {
                            var dateone = (document.getElementById("<%=txtContractStart.ClientID%>").value);
                            var timein = dateone.split('/');
                            var yr = parseInt(timein[2], 10);

                            var leapyear = check_leapyear();

                            if (leapyear == true) {
                                if (noofdays >= 366) {
                                    var noofyears = Math.floor(noofdays / 366);
                                    document.getElementById("<%=txtPortfolioValue.ClientID%>").value = (agreevalue / noofyears).toFixed(2);
                                    document.getElementById("<%=ddlPortfolioYN.ClientID%>").value = "1";
                                }
                                else {
                                    document.getElementById("<%=ddlPortfolioYN.ClientID%>").value = "0";

                                    document.getElementById("<%=txtPortfolioValue.ClientID%>").value = "0";
                                }
                            }
                            else if (leapyear == false) {
                                if (noofdays >= 365) {
                                    var noofyears = Math.floor(noofdays / 365);

                                    document.getElementById("<%=txtPortfolioValue.ClientID%>").value = (agreevalue / noofyears).toFixed(2);
                                    document.getElementById("<%=ddlPortfolioYN.ClientID%>").value = "1";
                                }
                                else {
                                    document.getElementById("<%=ddlPortfolioYN.ClientID%>").value = "0";

                                    document.getElementById("<%=txtPortfolioValue.ClientID%>").value = "0";
                                }
                            }
                        }
                        else {
                            document.getElementById("<%=ddlPortfolioYN.ClientID%>").value = "0";

                            document.getElementById("<%=txtPortfolioValue.ClientID%>").value = "0";
                        }
                    }
                }

            }

            function check_leapyear() {
                var dateone = (document.getElementById("<%=txtContractStart.ClientID%>").value);
                var timein = dateone.split('/');
                var year = parseInt(timein[2], 10);

                //three conditions to find out the leap year
                if ((0 == year % 4) && (0 != year % 100) || (0 == year % 400)) {
                    return true;
                }
                else {
                    return false;
                }
            }



            function PortfolioChange() {
                //  alert("Hi");
                var chkBoxList = document.getElementById("<%=rbtLstDuration.ClientID()%>");
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                var selected;
                for (var i = 0; i < chkBoxCount.length; i++) {
                    if (chkBoxCount[i].checked) {
                        selected = chkBoxCount[i];
                        break;
                    }
                }


                var portfolioyn = document.getElementById("<%=ddlPortfolioYN.ClientID%>").value;

                if (portfolioyn == "1") {
                    if (selected) {
                        if (selected.value == "Years") {

                            var noofyears = (document.getElementById("<%=txtDuration.ClientID%>").value);
                            var agreevalue = (document.getElementById("<%=txtAgreeVal.ClientID%>").value);

                            if (noofyears == 0)
                                document.getElementById("<%=txtPortfolioValue.ClientID%>").value = (agreevalue).toFixed(2);
                            else
                                document.getElementById("<%=txtPortfolioValue.ClientID%>").value = (agreevalue / noofyears).toFixed(2);

                        }
                        else if (selected.value == "Months") {

                            var noofmonths = (document.getElementById("<%=txtDuration.ClientID%>").value);
                            var agreevalue = (document.getElementById("<%=txtAgreeVal.ClientID%>").value);
                            var noofyears = Math.floor(noofmonths / 12);

                            if (noofmonths >= 12) {
                                document.getElementById("<%=txtPortfolioValue.ClientID%>").value = (agreevalue / noofyears).toFixed(2);
                            }
                            else {
                                document.getElementById("<%=txtPortfolioValue.ClientID%>").value = "0";
                            }
                        }
                        else if (selected.value == "Weeks") {

                            var noofweeks = (document.getElementById("<%=txtDuration.ClientID%>").value);
                            var agreevalue = (document.getElementById("<%=txtAgreeVal.ClientID%>").value);
                            var noofyears = Math.floor(noofweeks / 52);

                            if (noofweeks >= 52) {
                                document.getElementById("<%=txtPortfolioValue.ClientID%>").value = (agreevalue / noofyears).toFixed(2);
                            }
                            else {

                                document.getElementById("<%=txtPortfolioValue.ClientID%>").value = "0";
                            }
                        }
                        else if (selected.value == "Days") {

                            var noofdays = (document.getElementById("<%=txtDuration.ClientID%>").value);
                            var agreevalue = (document.getElementById("<%=txtAgreeVal.ClientID%>").value);

                            if (noofdays >= 365) {
                                var dateone = (document.getElementById("<%=txtContractStart.ClientID%>").value);
                                var timein = dateone.split('/');
                                var yr = parseInt(timein[2], 10);

                                var leapyear = check_leapyear();

                                if (leapyear == true) {
                                    if (noofdays >= 366) {
                                        var noofyears = Math.floor(noofdays / 366);
                                        document.getElementById("<%=txtPortfolioValue.ClientID%>").value = (agreevalue / noofyears).toFixed(2);
                                    }
                                    else {

                                        document.getElementById("<%=txtPortfolioValue.ClientID%>").value = "0";
                                    }
                                }
                                else if (leapyear == false) {
                                    if (noofdays >= 365) {
                                        var noofyears = Math.floor(noofdays / 365);

                                        document.getElementById("<%=txtPortfolioValue.ClientID%>").value = (agreevalue / noofyears).toFixed(2);

                                    }
                                    else {

                                        document.getElementById("<%=txtPortfolioValue.ClientID%>").value = "0";
                                    }
                                }
                            }
                            else {
                                document.getElementById("<%=txtPortfolioValue.ClientID%>").value = "0";
                            }
                        }
                    }
                }
                else {

                    document.getElementById("<%=txtPortfolioValue.ClientID%>").value = "0";
                }
            }


            function CalculateDates() {
     
                var dateone = (document.getElementById("<%=txtContractStart.ClientID%>").value);
                var dur1 = parseInt(document.getElementById("<%=txtDuration.ClientID%>").value);
      

                if (dur1 == 0)
                    document.getElementById("<%=txtDuration.ClientID%>").value = "1";
                var dur = parseInt(document.getElementById("<%=txtDuration.ClientID%>").value);


                if (dateone != "" && dur != "") {
      
                    var optionselected;
                    var durationms = document.getElementById("<%=rbtLstDuration.ClientID%>");
                    var radio = durationms.getElementsByTagName("input");
                    for (var i = 0; i < radio.length; i++) {
                        if (radio[i].checked) {
                            optionselected = i;
                            break;
                        }
                    }

                    var optionselected1;
                    var fixedcontinuous = document.getElementById("<%=rbtFixedContinuous.ClientID%>");
                    var radio1 = fixedcontinuous.getElementsByTagName("input");
                    for (var j = 0; j < radio1.length; j++) {
                        if (radio1[j].checked) {
                            optionselected1 = j;
                            break;
                        }
                    }

                    var timein = dateone.split('/');
                    var startdate = parseInt(timein[0], 10);
                    var startmonth = parseInt(timein[1], 10);
                    var startyear = parseInt(timein[2], 10);

                    var str3 = startyear + "/" + startmonth + "/" + startdate + " 10:00:00";
                    var startTime = new Date(str3);
                    var origdate = new Date(str3);

                    if (optionselected1 == 0) {
                        document.getElementById("<%=txtDuration.ClientID%>").disabled = false;
                        document.getElementById("<%=txtServEnd.ClientID%>").disabled = false;

                        //    alert("10");
                        if (optionselected == 0) {
                            if (dur == 1) {
                                startTime.setDate(startTime.getDate());

                                var dd = startTime.getDate();

                                var mm = startTime.getMonth() + 1;
                                var y = startTime.getFullYear();
                            }
                            else {
                                startTime.setDate(startTime.getDate() + dur);

                                var dd = startTime.getDate();

                                if (dd == 1)
                                    var dd = startTime.getDate();
                                else
                                    var dd = startTime.getDate() - 1;

                                var mm = startTime.getMonth() + 1;
                                var y = startTime.getFullYear();
                            }

                        }

                        else if (optionselected == 1) {
                            startTime.setDate(startTime.getDate() - 1);
                            startTime.setDate(startTime.getDate() + (dur * 7));

                            var dd = startTime.getDate();
                            var mm = startTime.getMonth() + 1;
                            var y = startTime.getFullYear();
                        }

                        else if (optionselected == 2) {
                           
                            startTime.setMonth(startTime.getMonth() + (dur));
                            startTime.setDate(startTime.getDate() - 1);

                            var dd = startTime.getDate();
                            var mm = startTime.getMonth() + 1;
                            var y = startTime.getFullYear();

                          

                            // start
                        
                            // end
                            //end
                        }

                        else if (optionselected == 3) {
                            startTime.setDate(startTime.getDate() - 1);

                            var dd = startTime.getDate();
                            var mm = startTime.getMonth() + 1;
                            var y = startTime.getFullYear() + (dur);
                        }

                        if (dd < 10) {
                            dd = "0" + dd;
                        }
                        if (mm < 10)
                            mm = "0" + mm;

                       
                        if (mm == "02") {                          
                            if (y % 4 == 0) {                          
                                if ((dd == 30) || (dd == 31)) {                            
                                    dd = 29;
                                }
                            }
                            else if ((dd == 29) || (dd == 30) || (dd == 31)) {
                                   dd = 28;
                            }
                            
                        }

                        if ((mm == "04") || (mm == "06") || (mm == "09") || (mm == "11")) {
                         if (dd == 31) {
                                dd = 30;
                            }

                        }



                        var str4 = y + "/" + mm + "/" + dd + " 10:00:00";
                        var endTime = new Date(str4);

                        //   alert("10");
                        document.getElementById("<%=txtContractEnd.ClientID%>").value = dd + "/" + mm + "/" + y;
                        document.getElementById("<%=txtServStart.ClientID%>").value = document.getElementById("<%=txtContractStart.ClientID%>").value;
                        document.getElementById("<%=txtServEnd.ClientID%>").value = dd + "/" + mm + "/" + y;
                        //alert("1");

                        document.getElementById("<%=txtEndofLastSchedule.ClientID%>").value = "";
                        //alert("2");
                        getWeekDay();
                        getValuePerMonth();
                    }

                    //      alert("ok");



                    if (optionselected1 == 1) {

                        var fixedcontinuous2 = document.getElementById("<%=rbtLstDuration.ClientID%>");
                        var radio2 = fixedcontinuous2.getElementsByTagName("input");
                        radio2.item(3).checked = true;

                        document.getElementById("<%=txtServEnd.ClientID%>").disabled = true;
                        //     var optionselected;
                        var durationms = document.getElementById("<%=rbtLstDuration.ClientID%>");
                        var radio = durationms.getElementsByTagName("input");
                        for (var i = 0; i < radio.length; i++) {
                            if (radio[i].checked) {
                                optionselected = i;
                                break;
                            }
                        }


                        if (optionselected == 0) {
                            if (dur == 1) {
                                startTime.setDate(startTime.getDate());

                                var dd = startTime.getDate();

                                var mm = startTime.getMonth() + 1;
                                var y = startTime.getFullYear();
                            }
                            else {
                                startTime.setDate(startTime.getDate() + dur);

                                var dd = startTime.getDate();

                                if (dd == 1)
                                    var dd = startTime.getDate();
                                else
                                    var dd = startTime.getDate() - 1;

                                var mm = startTime.getMonth() + 1;
                                var y = startTime.getFullYear();
                            }

                        }

                        else if (optionselected == 1) {
                            startTime.setDate(startTime.getDate() - 1);
                            startTime.setDate(startTime.getDate() + (dur * 7));

                            var dd = startTime.getDate();
                            var mm = startTime.getMonth() + 1;
                            var y = startTime.getFullYear();
                        }

                        else if (optionselected == 2) {
                            startTime.setDate(startTime.getDate() - 1);
                            startTime.setMonth(startTime.getMonth() + (dur));

                            var dd = startTime.getDate();
                            var mm = startTime.getMonth() + 1;
                            var y = startTime.getFullYear();


                        }

                        else if (optionselected == 3) {
                            startTime.setDate(startTime.getDate() - 1);

                            var dd = startTime.getDate();
                            var mm = startTime.getMonth() + 1;
                            var y = startTime.getFullYear() + (dur);
                        }

                        if (dd < 10) {
                            dd = "0" + dd;
                        }
                        if (mm < 10)
                            mm = "0" + mm;



                        if (mm == "02") {
                            if (y % 4 == 0) {
                                if ((dd == 30) || (dd == 31)) {
                                    dd = 29;
                                }
                            }
                            else if ((dd == 29) || (dd == 30) || (dd == 31)) {
                                dd = 28;
                            }

                        }

                        if ((mm == "04") || (mm == "06") || (mm == "09") || (mm == "11")) {
                            if (dd == 31) {
                                dd = 30;
                            }

                        }

                        var str4 = y + "/" + mm + "/" + dd + " 10:00:00";
                        var endTime = new Date(str4);

                        document.getElementById("<%=txtContractEnd.ClientID%>").value = dd + "/" + mm + "/" + y;
                        document.getElementById("<%=txtServStart.ClientID%>").value = document.getElementById("<%=txtContractStart.ClientID%>").value;
                        document.getElementById("<%=txtServEnd.ClientID%>").value = dd + "/" + mm + "/" + y;


                        getWeekDay();

                        getValuePerMonth();

                        document.getElementById("<%=txtContractEnd.ClientID%>").value = "";
                        document.getElementById("<%=txtServEnd.ClientID%>").value = "";
                        document.getElementById("<%=txtEndofLastSchedule.ClientID%>").value = dd + "/" + mm + "/" + y;
                        document.getElementById("<%=TxtServEndDay.ClientID%>").value = "";
                                   

                        //        document.getElementById("<%=txtDuration.ClientID%>").value = "0";
                        //     document.getElementById("<%=txtDuration.ClientID%>").disabled = true;

        
            
                    }
                }
            }





            function getWeekDay() {
                var dateone = (document.getElementById("<%=txtServStart.ClientID%>").value);
                var datetwo = (document.getElementById("<%=txtServEnd.ClientID%>").value);


                if (dateone != "") {

                    var timein = dateone.split('/');
                    var startdate = parseInt(timein[0], 10);
                    var startmonth = parseInt(timein[1], 10);
                    var startyear = parseInt(timein[2], 10);

                    var str3 = startyear + "/" + startmonth + "/" + startdate + " 10:00:00";
                    var startTime = new Date(str3);

                    var weekdaystart = startTime.getDay();

                    if (weekdaystart == 1)
                        weekdaystart = "MONDAY";
                    else if (weekdaystart == 2)
                        weekdaystart = "TUESDAY";
                    else if (weekdaystart == 3)
                        weekdaystart = "WEDNESDAY";
                    else if (weekdaystart == 4)
                        weekdaystart = "THURSDAY";
                    else if (weekdaystart == 5)
                        weekdaystart = "FRIDAY";
                    else if (weekdaystart == 6)
                        weekdaystart = "SATURDAY";
                    else if (weekdaystart == 0)
                        weekdaystart = "SUNDAY";
                    document.getElementById("<%=txtServStartDay.ClientID%>").value = weekdaystart;

                }

                if (datetwo != "") {

                    var timeout = datetwo.split('/');
                    var enddate = parseInt(timeout[0], 10);
                    var endmonth = parseInt(timeout[1], 10);
                    var endyear = parseInt(timeout[2], 10);

                    var str4 = endyear + "/" + endmonth + "/" + enddate + " 10:00:00";
                    var endTime = new Date(str4);

                    var weekdayend = endTime.getDay();


                    if (weekdayend == 1)
                        weekdayend = "MONDAY";
                    else if (weekdayend == 2)
                        weekdayend = "TUESDAY";
                    else if (weekdayend == 3)
                        weekdayend = "WEDNESDAY";
                    else if (weekdayend == 4)
                        weekdayend = "THURSDAY";
                    else if (weekdayend == 5)
                        weekdayend = "FRIDAY";
                    else if (weekdayend == 6)
                        weekdayend = "SATURDAY";
                    else if (weekdayend == 0)
                        weekdayend = "SUNDAY";
                    document.getElementById("<%=TxtServEndDay.ClientID%>").value = weekdayend;

                }

                var months;
                var year1 = startTime.getFullYear();
                var year2 = endTime.getFullYear();
                var month1 = startTime.getMonth();
                var month2 = endTime.getMonth() + 1;

                if (month1 === 0) {
                    month1++;
                    month2++;
                }
                var numberOfMonths;
                numberOfMonths = (year2 - year1) * 12 + (month2 - month1) - 1;
                if (numberOfMonths == 0)
                    numberOfMonths = 1;
                (document.getElementById("<%=txtNoofMonth.ClientID%>").value) = numberOfMonths;


                // 01.02.17

                var dur = (document.getElementById("<%=txtDuration.ClientID%>").value);

                if (dur != "") {
                    var dur = parseInt(document.getElementById("<%=txtDuration.ClientID%>").value);

                    var optionselected;
                    var durationms = document.getElementById("<%=rbtLstDuration.ClientID%>");
                    var radio = durationms.getElementsByTagName("input");
                    for (var i = 0; i < radio.length; i++) {
                        if (radio[i].checked) {
                            optionselected = i;
                            break;
                        }
                    }


                    if (optionselected == 2) {
                        (document.getElementById("<%=txtNoofMonth.ClientID%>").value) = dur;
                    }

                    else if (optionselected == 3) {
                        (document.getElementById("<%=txtNoofMonth.ClientID%>").value) = dur * 12;
                    }
                }
                //01.02.17
                getValuePerMonth();
            }

            function getValuePerMonth() {


       

                var noofmonths = (document.getElementById("<%=txtNoofMonth.ClientID%>").value);

                if (noofmonths == '') {
                    NoOfMonthsEdit();
                    var noofmonths = (document.getElementById("<%=txtNoofMonth.ClientID%>").value);
                }
                var agreevalue = (document.getElementById("<%=txtAgreeVal.ClientID%>").value);

                var valuepermonth = (document.getElementById("<%=txtValPerMnth.ClientID%>").value);


                document.getElementById("<%=txtServiceAmt.ClientID%>").value = agreevalue;
                document.getElementById("<%=txtServiceAmtBal.ClientID%>").value = agreevalue - document.getElementById("<%=txtServiceAmtActual.ClientID%>").value;

                if (noofmonths == 0)
                    document.getElementById("<%=txtValPerMnth.ClientID%>").value = (agreevalue).toFixed(2);
                else
                    document.getElementById("<%=txtValPerMnth.ClientID%>").value = (agreevalue / noofmonths).toFixed(2);


                calculateportfolio();

                var dateone = (document.getElementById("<%=txtServStart.ClientID%>").value);
                var datetwo = (document.getElementById("<%=txtServEnd.ClientID%>").value);


                var timein = dateone.split('/');
                var startdate = parseInt(timein[0], 10);
                var startmonth = parseInt(timein[1], 10);
                var startyear = parseInt(timein[2], 10);

                var str3 = startyear + "/" + startmonth + "/" + startdate + " 10:00:00";
                var startTime = new Date(str3);


                var timeout = datetwo.split('/');
                var enddate = parseInt(timeout[0], 10);
                var endmonth = parseInt(timeout[1], 10);
                var endyear = parseInt(timeout[2], 10);

                var str4 = endyear + "/" + endmonth + "/" + enddate + " 10:00:00";
                var endTime = new Date(str4);


                var ONE_DAY = 1000 * 60 * 60 * 24;
                // Convert both dates to milliseconds
                var date1_ms = startTime.getTime();
                var date2_ms = endTime.getTime();
                // Calculate the difference in milliseconds
                var difference_ms = Math.abs(date1_ms - date2_ms);
                // Convert back to days and return
                var noofdays = Math.round(difference_ms / ONE_DAY);
                noofdays = noofdays + 1;
                //alert(noofdays);

                var billingfrequency = document.getElementById("<%=ddlBillingFreq.ClientID%>").options[document.getElementById("<%=ddlBillingFreq.ClientID%>").selectedIndex].text;

                if (billingfrequency == "ANNUALLY") {
                    (document.getElementById("<%=txtBillingAmount.ClientID%>").value) = (document.getElementById("<%=txtValPerMnth.ClientID%>").value * 12).toFixed(2);
                }
                else if (billingfrequency == "HALF-ANNUALLY") {
                    if (noofmonths > 6)
                        (document.getElementById("<%=txtBillingAmount.ClientID%>").value) = (document.getElementById("<%=txtValPerMnth.ClientID%>").value * 6).toFixed(2);
                    else 
                        (document.getElementById("<%=txtBillingAmount.ClientID%>").value) = (document.getElementById("<%=txtAgreeVal.ClientID%>").value * 1).toFixed(2);

                }
                else if (billingfrequency == "BI-MONTHLY") {
                    if (noofmonths > 2)
                        (document.getElementById("<%=txtBillingAmount.ClientID%>").value) = (document.getElementById("<%=txtValPerMnth.ClientID%>").value * 2).toFixed(2);
                    else 
                        (document.getElementById("<%=txtBillingAmount.ClientID%>").value) = (document.getElementById("<%=txtAgreeVal.ClientID%>").value * 1).toFixed(2);

                }
                else if (billingfrequency == "QUARTERLY") {
                    if (noofmonths > 3)
                        (document.getElementById("<%=txtBillingAmount.ClientID%>").value) = (document.getElementById("<%=txtValPerMnth.ClientID%>").value * 3).toFixed(2);
                    else 
                        (document.getElementById("<%=txtBillingAmount.ClientID%>").value) = (document.getElementById("<%=txtAgreeVal.ClientID%>").value * 1).toFixed(2);

                }
                else if (billingfrequency == "MONTHLY") {
                    (document.getElementById("<%=txtBillingAmount.ClientID%>").value) = (document.getElementById("<%=txtValPerMnth.ClientID%>").value * 1).toFixed(2);
                }
                else if (billingfrequency == "HALF-MONTHLY") {
                    if (noofdays > 15)
                        (document.getElementById("<%=txtBillingAmount.ClientID%>").value) = (document.getElementById("<%=txtValPerMnth.ClientID%>").value / 2).toFixed(2);
                    else 
                        (document.getElementById("<%=txtBillingAmount.ClientID%>").value) = (document.getElementById("<%=txtAgreeVal.ClientID%>").value * 1).toFixed(2);

                }
                else if (billingfrequency == "DAILY") {
                    (document.getElementById("<%=txtBillingAmount.ClientID%>").value) = (document.getElementById("<%=txtAgreeVal.ClientID%>").value / noofdays).toFixed(2);
                }
                else if (billingfrequency == "WEEKLY") {
                    //alert(noofdays);

                    if (noofdays > 7)
                        (document.getElementById("<%=txtBillingAmount.ClientID%>").value) = (document.getElementById("<%=txtAgreeVal.ClientID%>").value / (noofdays / 7).toFixed(0)).toFixed(2);
                    else 
                        (document.getElementById("<%=txtBillingAmount.ClientID%>").value) = (document.getElementById("<%=txtAgreeVal.ClientID%>").value * 1).toFixed(2);

                }
                else if (billingfrequency == "BI-WEEKLY") {
                    if (noofdays > 14)
                        (document.getElementById("<%=txtBillingAmount.ClientID%>").value) = (document.getElementById("<%=txtAgreeVal.ClientID%>").value / (noofdays / (7 * 2))).toFixed(2);
                    else 
                        (document.getElementById("<%=txtBillingAmount.ClientID%>").value) = (document.getElementById("<%=txtAgreeVal.ClientID%>").value * 1).toFixed(2);

                }
            }

            function getNetAmount() {

                document.getElementById("<%=txtDisAmt.ClientID%>").value = ((document.getElementById("<%=txtConDetVal.ClientID%>").value) * (document.getElementById("<%=txtDisPercent.ClientID%>").value) * .01).toFixed(2);
                document.getElementById("<%=txtAgreeVal.ClientID%>").value = ((document.getElementById("<%=txtConDetVal.ClientID%>").value) - (document.getElementById("<%=txtDisAmt.ClientID%>").value)).toFixed(2);
                document.getElementById("<%=txtTotContVal.ClientID%>").value = (document.getElementById("<%=txtAgreeVal.ClientID%>").value);

                getWeekDay();

                var noofmonths = (document.getElementById("<%=txtNoofMonth.ClientID%>").value);
                var agreevalue = (document.getElementById("<%=txtAgreeVal.ClientID%>").value);

                if (noofmonths == 0) {

                    document.getElementById("<%=txtValPerMnth.ClientID%>").value = (agreevalue).toFixed(2);
                }
                else {

                    document.getElementById("<%=txtValPerMnth.ClientID%>").value = (agreevalue / noofmonths).toFixed(2);
                }
                calculateportfolio();
            }

            function calculateportfolio() {
                var noofmonths = (document.getElementById("<%=txtNoofMonth.ClientID%>").value);

                if (noofmonths > 11)
                    document.getElementById("<%=ddlPortfolioYN.ClientID%>").value = "1"
                else
                    document.getElementById("<%=ddlPortfolioYN.ClientID%>").value = "0";

                calculateportfoliovalues();

                if (document.getElementById("<%=txtIncludeinPortfolio.ClientID%>").value == "False") {

                    document.getElementById("<%=ddlPortfolioYN.ClientID%>").value = "0";
                    document.getElementById("<%=txtPortfolioValue.ClientID%>").value = 0.00;
                }
            }

            function calculateportfoliovalues() {
                var duration = document.getElementById("<%=txtDuration.ClientID%>").value;
                var agreevalue = (document.getElementById("<%=txtAgreeVal.ClientID%>").value);

                var portfolioyn = (document.getElementById("<%=ddlPortfolioYN.ClientID%>").value);

                var optionselected;
                var durationms = document.getElementById("<%=rbtLstDuration.ClientID%>");
                var radio = durationms.getElementsByTagName("input");
                for (var i = 0; i < radio.length; i++) {
                    if (radio[i].checked) {
                        optionselected = i;
                        break;
                    }
                }



                if ((portfolioyn == "YES") || (portfolioyn == "1")) {

                    if (optionselected == 1)
                        document.getElementById("<%=txtPortfolioValue.ClientID%>").value = (agreevalue / (duration / 52)).toFixed(2);
                    else if (optionselected == 2)
                        document.getElementById("<%=txtPortfolioValue.ClientID%>").value = (agreevalue / duration * 12).toFixed(2);
                    else if (optionselected == 3)
                        document.getElementById("<%=txtPortfolioValue.ClientID%>").value = (agreevalue / duration).toFixed(2);
                }
                else
                    document.getElementById("<%=txtPortfolioValue.ClientID%>").value = 0.00
            }

            function ValidateDatesContract() {

                var dateone = (document.getElementById("<%=txtContractStart.ClientID%>").value);
                var datetwo = (document.getElementById("<%=txtContractEnd.ClientID%>").value);

                if (dateone != "" && datetwo != "") {

                    var timein = dateone.split('/');
                    var timeout = datetwo.split('/');
                    var startdate = parseInt(timein[0], 10);
                    var startmonth = parseInt(timein[1], 10);
                    var startyear = parseInt(timein[2], 10);

                    var enddate = parseInt(timeout[0], 10);
                    var endmonth = parseInt(timeout[1], 10);
                    var endyear = parseInt(timeout[2], 10);
                }

                if (dateone != "" && datetwo != "") {

                    var str3 = startyear + "/" + startmonth + "/" + startdate + " 10:00:00";
                    var str4 = endyear + "/" + endmonth + "/" + enddate + " 10:00:00";

                    var startTime = new Date(str3);
                    var endTime = new Date(str4);

                    //        if (endTime < startTime) {
                    //            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Contract Start Date cannot be later than Contract End Date";
                    //            ResetScrollPosition();
                    //            document.getElementById("<%=txtContractStart.ClientID%>").focus();
                    //            return;
                    //         }
                }

                CalculateContractDates();
            }
        

            function calculateportfoliovaluesAgreeValueEdit() {
                var duration = document.getElementById("<%=txtDuration.ClientID%>").value;
                var agreevalue = (document.getElementById("<%=txtAgreeValueEdit.ClientID%>").value);
            
                NoOfMonthsEdit();

                //start
           
                var noofmonths = (document.getElementById("<%=txtNoofMonth.ClientID%>").value);

                //            var agreevalue = (document.getElementById("<%=txtAgreeValueEdit.ClientID%>").value);
           
                var valuepermonth = (document.getElementById("<%=txtValPerMnth.ClientID%>").value);
                      
                document.getElementById("<%=txtServiceAmt.ClientID%>").value = agreevalue;
                document.getElementById("<%=txtServiceAmtBal.ClientID%>").value = agreevalue - document.getElementById("<%=txtServiceAmtActual.ClientID%>").value;
                                

                if (noofmonths == 0)
                    document.getElementById("<%=txtValPerMnth.ClientID%>").value = (agreevalue).toFixed(2);
                else
                    document.getElementById("<%=txtValPerMnth.ClientID%>").value = (agreevalue / noofmonths).toFixed(2);
                //end



                var portfolioyn = (document.getElementById("<%=ddlPortfolioYN.ClientID%>").value);

                var optionselected;
                var durationms = document.getElementById("<%=rbtLstDuration.ClientID%>");
                var radio = durationms.getElementsByTagName("input");
                for (var i = 0; i < radio.length; i++) {
                    if (radio[i].checked) {
                        optionselected = i;
                        break;
                    }
                }



                if ((portfolioyn == "YES") || (portfolioyn == "1")) {

                    if (optionselected == 1)
                        document.getElementById("<%=txtPortfolioValueEdit.ClientID%>").value = (agreevalue / (duration / 52)).toFixed(2);
                    else if (optionselected == 2)
                        document.getElementById("<%=txtPortfolioValueEdit.ClientID%>").value = (agreevalue / duration * 12).toFixed(2);
                    else if (optionselected == 3)
                        document.getElementById("<%=txtPortfolioValueEdit.ClientID%>").value = (agreevalue / duration).toFixed(2);
                }
                else
                    document.getElementById("<%=txtPortfolioValueEdit.ClientID%>").value = 0.00
            }
            //start


            function NoOfMonthsEdit() {
                var dur = 1;
                var dateone = (document.getElementById("<%=txtServStart.ClientID%>").value);
                var datetwo = (document.getElementById("<%=txtServEnd.ClientID%>").value);

                if (dateone != "") {

                    var timein = dateone.split('/');
                    var startdate = parseInt(timein[0], 10);
                    var startmonth = parseInt(timein[1], 10);
                    var startyear = parseInt(timein[2], 10);

                    var str3 = startyear + "/" + startmonth + "/" + startdate + " 10:00:00";
                    var startTime = new Date(str3);

                    var weekdaystart = startTime.getDay();

                    if (weekdaystart == 1)
                        weekdaystart = "MONDAY";
                    else if (weekdaystart == 2)
                        weekdaystart = "TUESDAY";
                    else if (weekdaystart == 3)
                        weekdaystart = "WEDNESDAY";
                    else if (weekdaystart == 4)
                        weekdaystart = "THURSDAY";
                    else if (weekdaystart == 5)
                        weekdaystart = "FRIDAY";
                    else if (weekdaystart == 6)
                        weekdaystart = "SATURDAY";
                    else if (weekdaystart == 0)
                        weekdaystart = "SUNDAY";
                    document.getElementById("<%=txtServStartDay.ClientID%>").value = weekdaystart;
                }

                if (datetwo != "") {

                    var timeout = datetwo.split('/');
                    var enddate = parseInt(timeout[0], 10);
                    var endmonth = parseInt(timeout[1], 10);
                    var endyear = parseInt(timeout[2], 10);

                    var str4 = endyear + "/" + endmonth + "/" + enddate + " 10:00:00";
                    var endTime = new Date(str4);

                    var weekdayend = endTime.getDay();

                    if (weekdayend == 1)
                        weekdayend = "MONDAY";
                    else if (weekdayend == 2)
                        weekdayend = "TUESDAY";
                    else if (weekdayend == 3)
                        weekdayend = "WEDNESDAY";
                    else if (weekdayend == 4)
                        weekdayend = "THURSDAY";
                    else if (weekdayend == 5)
                        weekdayend = "FRIDAY";
                    else if (weekdayend == 6)
                        weekdayend = "SATURDAY";
                    else if (weekdayend == 0)
                        weekdayend = "SUNDAY";
                    document.getElementById("<%=TxtServEndDay.ClientID%>").value = weekdayend;
                }

                var months;
                var year1 = startTime.getFullYear();
                var year2 = endTime.getFullYear();
                var month1 = startTime.getMonth();
                var month2 = endTime.getMonth() + 1;

                if (month1 === 0) {
                    month1++;
                    month2++;
                }
                var numberOfMonths;
                numberOfMonths = (year2 - year1) * 12 + (month2 - month1) - 1;
                (document.getElementById("<%=txtNoofMonth.ClientID%>").value) = numberOfMonths;


                // 01.02.17

                var dur = (document.getElementById("<%=txtDuration.ClientID%>").value);

                if (dur != "") {
                    var dur = parseInt(document.getElementById("<%=txtDuration.ClientID%>").value);

                    var optionselected;
                    var durationms = document.getElementById("<%=rbtLstDuration.ClientID%>");
                    var radio = durationms.getElementsByTagName("input");
                    for (var i = 0; i < radio.length; i++) {
                        if (radio[i].checked) {
                            optionselected = i;
                            break;
                        }
                    }


                    if (optionselected == 2) {
                        (document.getElementById("<%=txtNoofMonth.ClientID%>").value) = dur;
                    }

                    else if (optionselected == 3) {
                        (document.getElementById("<%=txtNoofMonth.ClientID%>").value) = dur * 12;
                    }
                }
                //09.03.18

            }


            //end
        
            function ValidateDatesContract() {

                var dateone = (document.getElementById("<%=txtContractStart.ClientID%>").value);
                var datetwo = (document.getElementById("<%=txtContractEnd.ClientID%>").value);

                if (dateone != "" && datetwo != "") {

                    var timein = dateone.split('/');
                    var timeout = datetwo.split('/');
                    var startdate = parseInt(timein[0], 10);
                    var startmonth = parseInt(timein[1], 10);
                    var startyear = parseInt(timein[2], 10);

                    var enddate = parseInt(timeout[0], 10);
                    var endmonth = parseInt(timeout[1], 10);
                    var endyear = parseInt(timeout[2], 10);
                }

                if (dateone != "" && datetwo != "") {

                    var str3 = startyear + "/" + startmonth + "/" + startdate + " 10:00:00";
                    var str4 = endyear + "/" + endmonth + "/" + enddate + " 10:00:00";

                    var startTime = new Date(str3);
                    var endTime = new Date(str4);

                    //        if (endTime < startTime) {
                    //            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Contract Start Date cannot be later than Contract End Date";
                    //            ResetScrollPosition();
                    //            document.getElementById("<%=txtContractStart.ClientID%>").focus();
                    //            return;
                    //         }
                }

                //    CalculateContractDates();
            }

            function ValidateDatesService() {

                var dateone = (document.getElementById("<%=txtServStart.ClientID%>").value);
                var datetwo = (document.getElementById("<%=txtServEnd.ClientID%>").value);

                if (dateone != "" && datetwo != "") {

                    var timein = dateone.split('/');
                    var timeout = datetwo.split('/');
                    var startdate = parseInt(timein[0], 10);
                    var startmonth = parseInt(timein[1], 10);
                    var startyear = parseInt(timein[2], 10);

                    var enddate = parseInt(timeout[0], 10);
                    var endmonth = parseInt(timeout[1], 10);
                    var endyear = parseInt(timeout[2], 10);
                }

                if (dateone != "" && datetwo != "") {

                    var str3 = startyear + "/" + startmonth + "/" + startdate + " 10:00:00";
                    var str4 = endyear + "/" + endmonth + "/" + enddate + " 10:00:00";

                    var startTime = new Date(str3);
                    var endTime = new Date(str4);

                    //       if (endTime < startTime) {

                    //            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Service Start Date cannot be later than Service End Date";
                    //             ResetScrollPosition();
                    //             document.getElementById("<%=txtServStart.ClientID%>").focus();
                    //             return;
                    //         }
                }

                CalculateServiceDates();
                getWeekDay();
            }


            function ValidateDatesWarranty() {

                var dateone = (document.getElementById("<%=txtWarrStart.ClientID%>").value);
                var datetwo = (document.getElementById("<%=txtWarrEnd.ClientID%>").value);

                if (dateone != "" && datetwo != "") {

                    var timein = dateone.split('/');
                    var timeout = datetwo.split('/');
                    var startdate = parseInt(timein[0], 10);
                    var startmonth = parseInt(timein[1], 10);
                    var startyear = parseInt(timein[2], 10);

                    var enddate = parseInt(timeout[0], 10);
                    var endmonth = parseInt(timeout[1], 10);
                    var endyear = parseInt(timeout[2], 10);
                }

                if (dateone != "" && datetwo != "") {

                    var str3 = startyear + "/" + startmonth + "/" + startdate + " 10:00:00";
                    var str4 = endyear + "/" + endmonth + "/" + enddate + " 10:00:00";

                    var startTime = new Date(str3);
                    var endTime = new Date(str4);

                    if (endTime < startTime) {
                        //alert("Warranty Start Date cannot be later than Contract End Date");
                        document.getElementById("<%=lblAlert.ClientID%>").innerText = "Warranty Start Date cannot be later than Contract End Date";
                        ResetScrollPosition();
                        document.getElementById("<%=txtWarrStart.ClientID%>").focus();
                        return;
                    }
                }

                CalculateWarrantyDates();
            }
            ////


            function Left(str, n) {
                if (n <= 0)
                    return "";
                else if (n > String(str).length)
                    return str;
                else
                    return String(str).substring(0, n);
            }
            function Right(str, n) {
                if (n <= 0)
                    return "";
                else if (n > String(str).length)
                    return str;
                else {
                    var iLen = String(str).length;
                    return String(str).substring(iLen, iLen - n);
                }
            }

            function ConfirmChSt() {

                currentdatetime();

                var confirm_valueChSt = "";
                confirm_valueChSt = document.createElement("INPUT");
                confirm_valueChSt.type = "hidden";
                confirm_valueChSt.name = "confirm_valueChSt";

                var statusChSt = document.getElementById("<%=ddlStatusChSt.ClientID%>").options[document.getElementById("<%=ddlStatuschst.ClientID%>").selectedIndex].text;
                var status = document.getElementById("<%=txtStatus.ClientID%>").value;
                var OpenService = document.getElementById("<%=txtOpenService.ClientID%>").value;
                
                
          
                //          if ((Left(statusChSt, 1) == Left(status, 1))) {
                //          document.getElementById("<%=txtChangeStatus.ClientID%>").value = "000";
                //          return;
                //      }
               

              

            //    if ((Left(statusChSt, 1) == "E") || (Left(statusChSt, 1) == "T") || (Left(statusChSt, 1) == "X")) {
            //        if ((left(status, 1) != "O") && (left(status, 1) != "E")) {

            //            document.getElementById("<%=txtChangeStatus.ClientID%>").value = "001";
                        
            //            return;
            //        }
            //    }

           
                if (statusChSt == "--SELECT--") {
                    //alert("Status cannot be blank");
                    //document.getElementById("<%=lblAlert.ClientID%>").innerText = "Status cannot be blank";

                    document.getElementById("<%=txtChangeStatus.ClientID%>").value = "002";
                    confirm_valueChSt.value = "002";
                    document.forms[0].appendChild(confirm_valueChSt);
                    return;
                }
                else {
                    confirm_valueChSt.value = "111";
                }

            
                var actualendchst = document.getElementById("<%=txtActualEndChSt.ClientID%>").value;
                if (actualendchst == "") {
                    //alert("Actual End Date cannot be blank");
                    //document.getElementById("<%=lblAlert.ClientID%>").innerText = "Actual End Date cannot be blank";

                    document.getElementById("<%=txtChangeStatus.ClientID%>").value = "003";
                    document.forms[0].appendChild(confirm_valueChSt);
                    return;
                }
                else {
                    confirm_valueChSt.value = "112";
                }
            
           
                if (Left(statusChSt, 1) == "H") {
                    if (confirm("All Services (if any) on or after " + actualendchst + " will be put on hold. Do you want to Continue?")) {
                        confirm_valueChSt.value = "Yes";
                    } else {
                        confirm_valueChSt.value = "No";
                    }
                }

             
                if (Left(statusChSt, 1) == "O") {
                    if (confirm("Do you want to re-open this contract starting  " + actualendchst + "? Status of all services on or after " + actualendchst + " will be changed to O - Open?")) {
                        confirm_valueChSt.value = "Yes";
                    } else {
                        confirm_valueChSt.value = "No";
                    }
                }

            
                if ((Left(statusChSt, 1) == "T") &&  (Left(status, 1) == "E") && (OpenService == "N")) {
                    if (confirm("All Services (if any) on or after " + actualendchst + "  will be terminated. Do you want to Continue?")) {
                        confirm_valueChSt.value = "Yes";
                    } else {
                        confirm_valueChSt.value = "No";
                    }
                }

                if ((Left(statusChSt, 1) == "T") && (Left(status, 1) != "E")) {
                    if (confirm("All Services (if any) on or after " + actualendchst + "  will be terminated. Do you want to Continue?")) {
                        confirm_valueChSt.value = "Yes";
                    } else {
                        confirm_valueChSt.value = "No";
                    }
                }
                if (Left(statusChSt, 1) == "R") {
                    if (confirm("All Services (if any) on or after " + actualendchst + " will be void. Do you want to Continue?")) {
                        confirm_valueChSt.value = "Yes";
                    } else {
                        confirm_valueChSt.value = "No";
                    }
                }

                document.forms[0].appendChild(confirm_valueChSt);
            }


       
            function ClearTextBox() {
                document.getElementById("<%=txtClient.ClientID%>").value = '';
            }

            //function stopRKey(evt) {
            //    var evt = (evt) ? evt : ((event) ? event : null);
            //    var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            //    if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
            //}

            //document.onkeypress = stopRKey;



            function getTheDiffTime() {

                var dateone = (document.getElementById("<%=txtServTimeIn.ClientID%>").value);
                var datetwo = (document.getElementById("<%=txtServTimeOut.ClientID%>").value);

                //
                if (dateone != "") {

                    var timein = dateone.split(':');

                    if (parseInt(timein[0], 10) >= 0 && parseInt(timein[0], 10) <= 23 && parseInt(timein[1], 10) >= 0 && parseInt(timein[1], 10) <= 59) {
                    }
                    else {
                        //alert("Invalid Service In Time");
                        document.getElementById("<%=lblAlert.ClientID%>").innerText = "Invalid Service In Time";
                        ResetScrollPosition();
                        document.getElementById("<%=txtServTimeIn.ClientID%>").focus();
                        document.getElementById("<%=txtServTimeIn.ClientID%>").value = "";
                        document.getElementById("<%=txtAllocTime.ClientID%>").value = 0;
                        return;
                    }
                }


                if (datetwo != "") {

                    var timeout = datetwo.split(':');

                    if (parseInt(timeout[0], 10) >= 0 && parseInt(timeout[0], 10) <= 23 && parseInt(timeout[1], 10) >= 0 && parseInt(timeout[1], 10) <= 59) {
                    }
                    else {
                        //alert("Invalid Service Out Time..");
                        document.getElementById("<%=lblAlert.ClientID%>").innerText = "Invalid Service Out Time";
                        ResetScrollPosition();
                        document.getElementById("<%=txtServTimeOut.ClientID%>").focus();
                        document.getElementById("<%=txtServTimeOut.ClientID%>").value = "";
                        document.getElementById("<%=txtAllocTime.ClientID%>").value = 0;
                        return;
                    }
                }

            
                if (dateone != "" && datetwo != "") {
                    var format = "Minutes";
                    var str1 = "'2013/10/09 ";
                    var str2 = "'";
                    var str3 = '2013/10/09 ' + dateone;
                    var str4 = '2013/10/09 ' + datetwo;
                
                    var startTime = new Date(str3);
                    var endTime = new Date(str4);

                    if (endTime < startTime) {
                        //alert("Start Time cannot be laler than End Time");
                        document.getElementById("<%=lblAlert.ClientID%>").innerText = "Start Time cannot be later than End Time";
                        ResetScrollPosition();
                        document.getElementById("<%=txtServTimeIn.ClientID%>").focus();
                        return;
                    }

                    var difference = endTime.getTime() - startTime.getTime(); // This will give difference in milliseconds
                    var resultInMinutes = Math.round(difference / 60000);

                    resultInMinutes = 0
                    if (resultInMinutes < 0)
                        document.getElementById("<%=txtAllocTime.ClientID%>").value = 0;
                    else
                        document.getElementById("<%=txtAllocTime.ClientID%>").value = resultInMinutes;
                }
            }

               

            function CheckContactType() {
                var contactType = document.getElementById("<%=ddlContactType.ClientID%>").options[document.getElementById("<%=ddlContactType.ClientID%>").selectedIndex].text;

                if (contactType == '--Select--') {
                    //alert("Please select Contact Type");
                    document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Select Contact Type";
                    ResetScrollPosition();
                    document.getElementById("<%=ddlContactType.ClientID%>").focus();
                    $find("mdlPopUpClient").hide();
                    return false;
                }
                else {
                    return true;
                }
            }

            function checkstatus() {
                var status = document.getElementById("<%=txtStatus.ClientID%>").value;

                if (Left(status, 1) != "O") {
                    //alert("Records with status [O-Open] can be saved only");
                    document.getElementById("<%=lblAlert.ClientID%>").innerText = "Records with status [O-Open] can be saved only";
                    ResetScrollPosition();

                    valid = false;
                    return valid;
                }

                var gstatus = document.getElementById("<%=txtGS.ClientID%>").value;

                if (gstatus == "P") {
                    //alert("Service Records have been already Generated");
                    document.getElementById("<%=lblAlert.ClientID%>").innerText = "Service Records have been already Generated";
                    ResetScrollPosition();

                    valid = false;
                    return valid;
                }
                return valid;
            }


            //     function DoStatusCheck(parameter) {

            //         var valid = true;
            //          var stat = document.getElementById("<%=txtStatus.ClientID%>").value;
            //           if (stat != 'O') {
            //               //alert("Contract status should be [O-Open] ");
            //               document.getElementById("<%=lblAlert.ClientID%>").innerText = "Contract status should be [O-Open]";
            //               ResetScrollPosition();

            //              valid = false;
            //               $find("ShowHideMPE2").hide();

            //           }
            //          return valid;
            //       }


            function DoStatusCheckC(parameter) {
                var valid = true;
                var stat = document.getElementById("<%=txtStatus.ClientID%>").value;
                if (Left(stat, 1) != 'O') {
                    //alert("Contract status should be [O-Open] ");
                    document.getElementById("<%=lblAlert.ClientID%>").innerText = "Contract status should be [O-Open]";
                    ResetScrollPosition();

                    valid = false;
                    $find("ShowHideMPE4").hide();
                }
                return valid;
            }


            function DoStatusCheckT(parameter) {
                var valid = true;
                var stat = document.getElementById("<%=txtStatus.ClientID%>").value;
                if (Left(stat, 1) != 'O') {
                    //alert("Contract status should be [O-Open] ");
                    document.getElementById("<%=lblAlert.ClientID%>").innerText = "Contract status should be [O-Open]";
                    ResetScrollPosition();

                    valid = false;
                    $find("ShowHideMPE3").hide();

                }
                return valid;
            }

        
            function ResetScrollPosition() {
                setTimeout("window.scrollTo(0,0)", 0);
            }

            function ResetScrollPosition1() {
                setTimeout("window.scrollTo(10,0)", 0);
            }


            function checkmultiprint() {

                var table = document.getElementById('<%=grdViewServicesPrint.ClientID%>');
                var totbillamt = 0;


                if (table.rows.length > 0) {
                    //alert("2");

                    var input = table.rows[0].getElementsByTagName("input");
                    if (input[0].id.indexOf("chkSelectAllServicesPrintGV") > -1) {

                        //start
                        if ((input[0].checked) == false) {
                            for (var i = 1; i < table.rows.length; i++) {

                                //get all the input elements
                                var input1 = table.rows[i].getElementsByTagName("input");

                                for (var j = 0; j < input1.length; j++) {

                                    //get the textbox1
                                    input1[0].checked = false;

                                }
                            }

                            //end


                        }


                        else {
                            //loop the gridview table
                            for (var i = 1; i < table.rows.length; i++) {

                                //get all the input elements
                                var inputs = table.rows[i].getElementsByTagName("input");

                                for (var j = 0; j < inputs.length; j++) {

                                    //get the textbox1
                                    inputs[0].checked = true;


                                }
                            }
                        }
                    }
                }

            }
    
            function openInNewTab() {
                window.document.forms[0].target = '_blank';
                setTimeout(function () { window.document.forms[0].target = ''; }, 0);
            }
       </script>
 
</asp:Content>

