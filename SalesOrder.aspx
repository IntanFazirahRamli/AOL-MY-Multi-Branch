<%@ Page Title="Sales Order" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="SalesOrder.aspx.vb" Inherits="SalesOrder" Culture="en-GB" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" ValidateRequest="false"  %>



<%@ Register Assembly="Ajaxified" Namespace="Ajaxified" TagPrefix="Ajaxified" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <style type="text/css">
             .gridcell {
             word-break: break-all;
         }
       
         .ajax__tab_xp .ajax__tab_header {font-family:Calibri;font-size:15px;text-align:left;}


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

               .roundbutton1 {
               border: 2px solid #a1a1a1;   
              background: #dddddd;   
              border-radius: 25px;
              height:30px;
              width:90%;
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
      <asp:UpdatePanel ID="updPanelInvoice" runat="server" UpdateMode="Conditional">
          <ContentTemplate>

        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release" EnablePartialRendering="true">
            <ControlBundles>
                <asp:ControlBundle Name="CalendarExtender_Bundle" />
                <asp:controlBundle name="ModalPopupExtender_Bundle"/>  
                <asp:controlBundle name="MaskedEditExtender_Bundle"/>
                <asp:controlBundle name="TabContainer_Bundle"/>     
                  <asp:controlBundle name="ComboBox_Bundle"/>   
               <asp:controlBundle name="CollapsiblePanelExtender_Bundle"/>          
            </ControlBundles>
        </asp:ToolkitScriptManager>
     

    <asp:Button ID="dummy" runat="server" Text="" cssclass="dummybutton" />

     <div>
   
     <div style="text-align:center">
         
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">SALES ORDER</h3>
          
          <asp:UpdatePanel ID="updPnlMsg" runat="server" UpdateMode="Conditional">
              <ContentTemplate>


         <table border="0" style="width:100%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0%; "  >
        <tr >

                <td colspan="15"  style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      
                      <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="15"   style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
            <tr>
                 <td colspan="15"   style="width:100%;text-align:left;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';"> 
                         <asp:Label ID="lblInvoiceId" runat="server" Visible="false" Text="Account Id: " style="width:40%;text-align:left;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';" ></asp:Label>
                      <asp:Label ID="lblInvoiceId1" runat="server" Text=""  ></asp:Label>  &nbsp;  &nbsp;  &nbsp;
            
                           <asp:Label ID="txtMode" runat="server" Text="" Visible="true" CssClass="dummybutton"></asp:Label>
                      </td> 
            </tr>

              <tr>
                <td style="width:6%;text-align:center;"> 
                   <asp:Button ID="btnADD" runat="server" Font-Bold="True" Text="ADD" Width="100%" CssClass="roundbutton1" BackColor="#CFC6C0" Visible="TRUE" OnClientClick="initialize()"  />
                 
                      </td>
                  
                  <td style="width:6%;text-align:center;">
                             <asp:Button ID="btnCopy" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="COPY" Width="100%" OnClientClick="RefreshSubmit()" Visible="TRUE" />
               
                    </td>
           
                  <td style="width:6%;text-align:center;">
                             <asp:Button ID="btnEdit" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="EDIT" Width="100%" OnClientClick="RefreshSubmit()" Visible="TRUE"  />
               
                  </td>
                  <td style="width:6%;text-align:center;">
                              <asp:Button ID="btnPrint" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="PRINT" Width="100%" Visible="true" />
              
                  </td>
               
                     <td style="width:8%;text-align:center;">
                                 <asp:Button ID="btnMultiPrint" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="MULTI PRINT" Width="100%" Visible="False" />
              
                      </td>
                
                  <td style="width:6%;text-align:center;">
                               <asp:Button ID="btnPost" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime();" Text="POST" Width="100%" />
             
                  </td>
                  
                   <td style="width:7%;text-align:center;">
                           <asp:Button ID="btnReverse" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="REVERSE" Width="100%" OnClientClick="currentdatetime();" />
              
                    </td>

                    <td style="width:7%;text-align:center;">
                              <asp:Button ID="btnChangeStatus" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CH. ST." Width="100%" />
               
                  </td>

                   <td style="width:7%;text-align:center;">
                          <asp:Button ID="btnReceipts" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="RECEIPTS" OnClientClick="currentdatetimereceipt()" Visible="False" Width="100%" />
          
                               </td>
                    <td style="width:6%;text-align:center;">
                          <asp:Button ID="btnCNDN" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CN/DN"  Visible="False" Width="100%" />
          
                               </td>
                  
                       <td style="width:7%;text-align:center;">
                         <asp:Button ID="btnJournal" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="JOURNAL" Width="100%" Visible="False" />
                  </td>
                   <td style="width:7%;text-align:center;">
                          <asp:Button ID="btnRecalculate" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime()" Text="RECALCULATE" Width="100%" Visible="False" />
         
                  </td>
                   <td style="width:6%;text-align:center;">
                          <asp:Button ID="btnFilter" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime()" Text="FILTER" Width="100%" Visible="False" />
         
                  </td>


                     <td style="width:6%;text-align:center;">
                         <asp:Button ID="btnBack" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="BACK" Width="100%" />
                  </td>
                  
                 
                
                  

                  <td style="width:0%;text-align:center;">
                        <asp:Button ID="btnDelete" runat="server" Font-Bold="True" Text="DELETE" Width="2%" Visible="false"  CssClass="button" BackColor="#CFC6C0" OnClientClick="currentdatetime(); Confirm()" />
          </td>
                 
               
            </tr>

            <tr>
                 <td colspan="15"   style="width:100%;text-align:left;color:brown;font-size:16px;font-weight:bold;font-family:'Calibri';">
             
                 </td>
                
            </tr>
    </table>
       </ContentTemplate>
              </asp:UpdatePanel>
             

          <asp:UpdatePanel ID="updPnlSearch" runat="server" UpdateMode="Conditional"><ContentTemplate>

             <table id="tablesearch" border="0" runat="server" style="border: 1px solid #CC3300; text-align:right; width:100%; border-radius: 25px; width:100%; height:60px; background-color: #F3F3F3;">
            <tr>
                <td style="text-align:left;width:100%;">
                   
                    <table border="0" style="font-family: calibri; font-size: 15px; font-weight: bold; color: #000000;width:100%;padding-left:2px;">
                         <tr>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:7%; ">
                                    Sales Order No.                
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:10%; ">
                                 <asp:TextBox ID="txtInvoicenoSearch" runat="server" style="text-align:left; " MaxLength="50" Height="16px" Width="95%" AutoCompleteType="Disabled" AutoPostBack="True" ></asp:TextBox>         
                            </td>


                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:6%; ">
                                    Sales Order Date                
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:6%; ">
                                 <asp:TextBox ID="txtInvoiceDateFromSearch" runat="server" style="text-align:left; " MaxLength="50" Height="16px" Width="95%" AutoCompleteType="Disabled" ></asp:TextBox>         
                                  <asp:MaskedEditExtender runat="server" ID="MaskedEditExtender1" ClearMaskOnLostFocus="false"
                                        MaskType="Date" Mask="99/99/9999" TargetControlID="txtInvoiceDateFromSearch" UserDateFormat="DayMonthYear">
                                </asp:MaskedEditExtender>
                                      <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtInvoiceDateFromSearch" TargetControlID="txtInvoiceDateFromSearch" Enabled="True" />

                                   </td>

                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:5%; ">
                                    To                
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:7%; ">
                                 <asp:TextBox ID="txtInvoiceDateToSearch" runat="server" style="text-align:left; " MaxLength="50" Height="16px" Width="90%" AutoCompleteType="Disabled" ></asp:TextBox>         
                          <asp:MaskedEditExtender runat="server" ID="MaskedEditExtender2" ClearMaskOnLostFocus="false"
                                MaskType="Date" Mask="99/99/9999" TargetControlID="txtInvoiceDateToSearch" UserDateFormat="DayMonthYear">
                            </asp:MaskedEditExtender>
                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtInvoiceDateToSearch" TargetControlID="txtInvoiceDateToSearch" Enabled="True" />

                                    </td>

                                                   

                                  <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:5%; ">
                                      Remarks                
                            </td>
                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:13%; ">
                                 <asp:TextBox ID="txtCommentsSearch" runat="server" style="text-align:left; " MaxLength="50" Height="16px" Width="90%" AutoCompleteType="Disabled" ></asp:TextBox>         
                            </td>

                          
                           

                               <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:5%;"></td>
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:11%; ">
                                  <asp:RadioButtonList ID="rdbSearchPaidStatus" runat="server"  RepeatDirection="Horizontal" Visible="False">
                                      <asp:ListItem Selected="True">All</asp:ListItem>
                                      <asp:ListItem>O/S</asp:ListItem>
                                      <asp:ListItem>Fully Paid</asp:ListItem>
                                  </asp:RadioButtonList>
                                                     </td>

                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:6%;">
                                       <asp:Button ID="btnQuickSearch" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Search" Width="95%" />
                                 </td>
                        </tr>
                          <tr>

                              
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:7%; ">
                                    Company Group
                            </td>
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:9%; ">
                                  <asp:DropDownList ID="ddlCompanyGrpSearch" runat="server" AppendDataBoundItems="True" DataTextField="companygroup" DataValueField="companygroup" Height="20px" Width="96%">
                                     <asp:ListItem>--SELECT--</asp:ListItem>
                                  </asp:DropDownList>
                                     
                            <asp:ImageButton ID="ImageButton3" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"  Height="22px" Width="24px" ImageAlign="Top" Visible="False"     />   
                        
                            </td>
                                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:5%; ">
                                    Account Type</td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:8%; ">
                             <asp:DropDownList ID="ddlContactTypeSearch" runat="server" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="95%">
                                   <asp:ListItem>--SELECT--</asp:ListItem>
                                        <asp:ListItem  Value="COMPANY">CORPORATE</asp:ListItem>
                                     <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                 </asp:DropDownList>
                                </td>


                                                      
                          
                                  <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:5%;">
                                    Account Id</td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:9%; ">
                               
                           <asp:TextBox ID="txtAccountIdSearch" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" style="text-align:left; " Width="72%" AutoPostBack="True"></asp:TextBox>
                                  <asp:ImageButton ID="btnClientSearch" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                            </td>


                            <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:5%; ">
                                    
                                    Client Name</td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:13%; ">
                                  <asp:TextBox ID="txtClientNameSearch" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" style="text-align:left;padding-left:5px;" Width="75%" AutoPostBack="True"></asp:TextBox>
                                  &nbsp;<asp:ImageButton ID="btnClientSearch0" runat="server" CssClass="righttextbox" Height="22px"   ImageAlign="Top" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                            </td>
                            
                          

                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:5%; ">Post Status </td>
                             <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:10%; ">
                                  <asp:TextBox ID="txtSearch1Status" runat="server" Width="60%" ReadOnly="FALSE"></asp:TextBox>
                                 <asp:ImageButton ID="btnSearch1Status" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg" ImageAlign="Top"
                                    Height="22px" Width="24px" />  
                                 
                                  <asp:ModalPopupExtender ID="mdlPopupStatusSearch" runat="server" CancelControlID="btnStatusCancel" PopupControlID="pnlStatusSearch" TargetControlID="btnSearch1Status" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
                     
                            </td>
                            


                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; width:6%;">
                                       <asp:Button ID="btnQuickReset" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Reset" Width="95%" OnClientClick="ClearSearch()" />
                                   </td>
                        </tr>

                            <tr>

                              
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; text-align:right; ">
                                  <asp:Label ID="lblBranch" runat="server" Text="Branch" CssClass="CellFormat"></asp:Label>
                            </td>
                              <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black;  width:9%; ">
                                  <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" DataTextField="locationID" DataValueField="LocationID" Height="20px" Width="96%" DataSourceID="SqlDSLocation">
                                     <asp:ListItem>--SELECT--</asp:ListItem>
                                  </asp:DropDownList>
                           
                            </td>
                            <td colspan="9"></td>
                        </tr>
                    </table>
                      </td>
                
            </tr>
        </table>

                </ContentTemplate>
              </asp:UpdatePanel>
     


         <table border="0"  style="width:95%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0% " >
            <tr>
                <td colspan="12" style="text-align:left;"><asp:Label ID="Label57" runat="server" Text="View Records :" CssClass="CellFormat"></asp:Label><asp:DropDownList ID="ddlView" runat="server" AutoPostBack="True">
                    <asp:ListItem Selected="True">10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList>

                </td>

            </tr>
             
                            <tr style="text-align:center;">
                                  <td colspan="12" style="width:100%;text-align:center">
                            <div style="text-align:center; width:100%; margin-left:auto; margin-right:auto;" >
            
                                      <asp:Panel ID="pnlGrid" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="100%" ScrollBars="Auto" style="text-align:center; width:1350px; margin-left:auto; margin-right:auto;"    Visible="true" Width="1330px">
          
                                          <asp:GridView ID="GridView1" Width="100%" Font-Size="15px" runat="server" OnRowDataBound = "OnRowDataBoundg1" OnSelectedIndexChanged = "OnSelectedIndexChangedg1" AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SQLDSInvoice" ForeColor="#333333" GridLines="Vertical" AllowPaging="True"> 
                                              <AlternatingRowStyle BackColor="White"/>
                                                <Columns>
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
                                                  <asp:BoundField DataField="PostStatus" SortExpression="PostStatus" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="2%" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="PostStatus" HeaderText="Post St" SortExpression="PostStatus" >
                                                    <ItemStyle Width="2%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EmailSentStatus" HeaderText="ES" SortExpression="EmailSentStatus" />
                                                  <asp:BoundField DataField="PaidStatus" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="2%" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="SalesOrderNumber" HeaderText="SalesOrder Number" SortExpression="SalesOrderNumber">
                                                    <ControlStyle Width="6%" />
                                                  <ItemStyle Wrap="False" Width="6%" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="SalesOrderDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="SalesOrder Date" SortExpression="SalesOrderDate" >
                                                    <ItemStyle Width="5%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GLPeriod" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CompanyGroup" HeaderText="Company Group" SortExpression="CompanyGroup" >
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ContactType" HeaderText="Account Type" SortExpression="ContactType" />
                                                    <asp:BoundField DataField="AccountId" HeaderText="Account Id" SortExpression="AccountId">
                                                    <ControlStyle Width="6%" />
                                                    </asp:BoundField>
                                                 <%-- <asp:BoundField DataField="CustName" HeaderText="Client Name" SortExpression="CustName">
                                                    <ControlStyle Width="150px" />
                                                  <ItemStyle HorizontalAlign="Left" Wrap="False" Width="150px" />
                                                  </asp:BoundField>--%>
                                                 
                                                      <asp:TemplateField HeaderText="Client Name" >
                                                           <EditItemTemplate>
                                                                <asp:TextBox ID="txtClientNameConcat" runat="server" Text='<%# Eval("CustName")%>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                              <ItemTemplate> 
                                                                   <div style="width: 200px;text-align:left;height:37px;overflow-y:auto;">
                                                                 <asp:Label ID="Label2" runat="server" Text='<%# Eval("CustName")%>'></asp:Label>
                                                           </div>
                                                                          </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Width="270px" />
                                                           <ItemStyle Font-Names="Calibri"  HorizontalAlign="Left" />
                                                      </asp:TemplateField>

                                                    <asp:BoundField DataField="ValueBase" HeaderText="SO Amount" DataFormatString="{0:N2}" SortExpression="ValueBase">
                                                    <ControlStyle Width="8%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="8%" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AppliedBase" HeaderText="Net SO Amt." DataFormatString="{0:N2}" SortExpression="AppliedBase">
                                                    <HeaderStyle Wrap="True" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Receiptbase" DataFormatString="{0:N2}" SortExpression="Receiptbase" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle HorizontalAlign="Right" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CreditBase" DataFormatString="{0:N2}" SortExpression="CreditBase" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle HorizontalAlign="Right" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BalanceBase" DataFormatString="{0:N2}" SortExpression="BalanceBase" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle HorizontalAlign="Right" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                 

                                                     <asp:TemplateField HeaderText="Bill Address" >
                                                           <EditItemTemplate>
                                                                <asp:TextBox ID="txtBillAddressConcat" runat="server" Text='<%# Eval("CustAddress1") & " " & Eval("CustAddStreet") & " " & Eval("custAddBuilding") & " " & Eval("CustAddCity") & " " & Eval("CustAddCountry") & " " & Eval("CustAddPostal")%>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                              <ItemTemplate> 
                                                                   <div style="width: 175px;text-align:left;height:37px;overflow-y:auto;">
                                                                 <asp:Label ID="lblBillAddressConcat" runat="server" Text='<%# Eval("CustAddress1") & " " & Eval("CustAddStreet") & " " & Eval("custAddBuilding") & " " & Eval("CustAddCity") & " " & Eval("CustAddCountry") & " " & Eval("CustAddPostal")%>'></asp:Label>
                                                           </div>
                                                                          </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Width="250px" />
                                                           <ItemStyle Font-Names="Calibri"  HorizontalAlign="Left" />
                                                      </asp:TemplateField>

                                               
                                                   <%-- <asp:BoundField DataField="CustAddPostal" HeaderText="Postal" SortExpression="CustAddPostal" />--%>

                                                      <asp:TemplateField HeaderText="Service Address" >
                                                           <EditItemTemplate>
                                                                <asp:TextBox ID="txtServiceAddressConcat" runat="server" Text='<%# Eval("ServiceAddress")%>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                              <ItemTemplate>
                                                                   <div style="width: 175px;text-align:left;height:37px;overflow-y:auto;">
                                                                 <asp:Label ID="lblServiceAddressConcat" runat="server" Text='<%# Eval("ServiceAddress")%>'></asp:Label>
                                                                </div>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Width="250px" />
                                                           <ItemStyle Font-Names="Calibri"  HorizontalAlign="Left" />
                                                      </asp:TemplateField>


                                                    <asp:BoundField DataField="Description" HeaderText="Description">
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="Location" HeaderText="Location" />
                                                    <asp:BoundField DataField="StaffCode" HeaderText="Salesman" SortExpression="StaffCode" >
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PrintCounter" HeaderText="Print Count">
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EmailSentDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Email Sent Date" />
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
                                                    <asp:BoundField DataField="PoNo" InsertVisible="False">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="OurRef" InsertVisible="False">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="YourRef" InsertVisible="False">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Terms" InsertVisible="False">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DiscountAmount">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GSTBase">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BatchNo">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AmountWithDiscount">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TermsDay">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RecurringSalesOrder" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BillSchedule">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Comments">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" Wrap="False" />
                                                    </asp:BoundField>

                                                       <asp:TemplateField ><ItemTemplate> <asp:Button ID="btnEditHistory" runat="server"  Text="History" OnClick="btnEditHistory_Click" CssClass="righttextbox" Height="25px" Visible="true" OnClientClick="currentdatetime()" ImageAlign="Top"   Width="80px"   />
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
                                      </div>
                                  </td>
                              </tr>
             
                 <tr>
                 <td>
                      <asp:SqlDataSource ID="SQLDSInvoice" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                </asp:SqlDataSource>
                     </td>
                     
                     <td>
              <asp:SqlDataSource ID="SqlDSCompanyGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                       
            </asp:SqlDataSource>
                           </td><td>
               <asp:SqlDataSource ID="SqlDSSalesGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                       
            </asp:SqlDataSource>
                                 </td><td>
               <asp:SqlDataSource ID="SqlDSLocateGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                       
            </asp:SqlDataSource>
                   </td><td>
              <asp:SqlDataSource ID="SqlDSSalesMan" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
           </td>
                     <td>
                       <asp:SqlDataSource ID="SqlDSIndustry" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                  </td> 

                 <td>
                       <asp:SqlDataSource ID="SqlDSContractGroup" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                 </td>
                          <td>
                       <asp:SqlDataSource ID="SqlDSTerms" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                  </td> 

                      <td>
              <asp:SqlDataSource ID="SqlDSServiceFrequency" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                       
            </asp:SqlDataSource>
                           </td>

                      <td>
              <asp:SqlDataSource ID="SqlDSBillingFrequency" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                       
            </asp:SqlDataSource>
                              <asp:SqlDataSource ID="SqlDSLocation" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="select locationid from tbllocation">
                       
            </asp:SqlDataSource>
                           &nbsp;&nbsp;&nbsp;&nbsp;
                           </td>

             
                 <td>
                    
                     &nbsp;</td>     
                <td>
                    
                </td>
                           </tr>
                                  <tr>
                                      <asp:TextBox ID="txt" runat="server" Height="16px" MaxLength="50" Visible="false" Width="0px"></asp:TextBox>
                                      <asp:TextBox ID="txtInvoiceSearch" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtCustomerSearch" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtImportService" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                    
                                      <asp:TextBox ID="txtDDLText" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtPopupType" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtRcnotblServiceBillingDetail" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtcontractfrom" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtClientFrom" runat="server" Visible="False"></asp:TextBox>
                                      <asp:TextBox ID="txtCondition" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtOrderBy" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtTotReceipts" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtRcnoSelected" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtLimit" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtSelectedIndex" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtSelectedRow" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtDisplayRecordsLocationwise" runat="server" Height="16px" MaxLength="50" Visible="false" Width="5px"></asp:TextBox>
                                      <asp:TextBox ID="txtDocType" runat="server" Height="16px" MaxLength="50" Visible="false" Width="1px"></asp:TextBox>
                                      <asp:TextBox ID="txtTotDetRec" runat="server" Height="16px" MaxLength="50" Visible="false" Width="1px"></asp:TextBox>
                                        <asp:TextBox ID="txtSelect" runat="server" Height="16px" MaxLength="50" Visible="false" Width="1px"></asp:TextBox>
                                    <asp:TextBox ID="txtGroupAuthority" runat="server" Visible="False"></asp:TextBox> 
                                       <asp:TextBox ID="txtPostUponSave" runat="server" Visible="False"></asp:TextBox> 
                                          <asp:TextBox ID="txtTotalReceipts" runat="server" Visible="False"></asp:TextBox>
                                       <asp:TextBox ID="txtOnlyEditableByCreator" runat="server" Visible="False"></asp:TextBox> 
                                        <asp:TextBox ID="txtRecordCreatedBy" runat="server" Visible="False"></asp:TextBox> 
                                         <asp:TextBox ID="txtDefaultTaxCode" runat="server" Visible="False"></asp:TextBox> 
                               <asp:TextBox ID="txtContractTE" runat="server" Visible="False"></asp:TextBox> 
            <asp:TextBox ID="txtLogDocNo" runat="server" Visible="False"></asp:TextBox> 
                              </tr>
       
             </table>
      

          


                <table border="0" style="width:90%; margin:auto; border:solid; border-color:ButtonFace;">
                          
               <tr style="width:100%">
              
                   <td colspan="1" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:right; color:#800000; width:15% ">TOTAL SALES ORDER AMOUNT
                 </td>

                     <td colspan="1" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline; color:#800000; width:15% ">
                         <asp:TextBox ID="txtTotalSalesAmount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Enabled="False" Height="16px" TabIndex="20" Width="70%" style="text-align:right;" Font-Bold="true" ></asp:TextBox>
             
                              </td>

                      <td colspan="1" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:right; color:#800000; width:15% ">NET SALES ORDER AMOUNT
                 </td>

                     <td colspan="1" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline; color:#800000; width:15% ">
                         <asp:TextBox ID="txtTotalInvoiceAmount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Enabled="False" Height="16px" TabIndex="20" Width="70%" style="text-align:right;" Font-Bold="true" ></asp:TextBox>
             
                              </td>

                   <td colspan="1" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:right; color:#800000; width:15% ">&nbsp;</td>

                     <td colspan="1" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline; color:#800000; width:15% ">
                         <asp:TextBox ID="txtTotalOSAmount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Enabled="False" Height="16px" TabIndex="20" Width="70%" style="text-align:right;" Font-Bold="true" Visible="False" ></asp:TextBox>
             
                              </td>

           </tr>
               </table>
      


        <%-- start Tabcontainer--%>

         
         <table border="0" style="width:70%; margin:auto; border:solid; border-color:ButtonFace;">
         	 <tr style="text-align:left;width:80%">
                <td style="text-align:left;padding-left:1px;">

    <asp:TabContainer ID="tb1" runat="server" ActiveTabIndex="0" Font-Names="Calibri" Height="100%" Width="95%" CssClass="ajax__tab_xp"  AutoPostBack="true">
         <asp:TabPanel runat="server" HeaderText=" Billing Info" ID="TabPanel1">
             <HeaderTemplate>
Billing Info
</HeaderTemplate>
<ContentTemplate>
<asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional"><ContentTemplate>
<table border="0" style="width:95%; margin:auto; border:solid; border-color:ButtonFace;">
    
    <tr style="width:100%"><td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:1%; color:#800000; background-color: #C0C0C0;">SALES ORDER&nbsp;INFORMATION </td></tr>
    <tr>
        <td colspan="3"></td>
        <td><asp:SqlDataSource ID="SqlDSSalesDetail" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" DataSourceMode="DataReader" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"><SelectParameters><asp:ControlParameter ControlID="txtInvoiceNo" Name="@InvoiceNumber" PropertyName="Text" /></SelectParameters></asp:SqlDataSource>

        </td>

    </tr>
    
    <tr>
    <td style="font-size:15px;font-weight:bold; font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "><asp:SqlDataSource ID="SqlDSUnitMS" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT UnitMS FROM tblunitms order by UnitMS"></asp:SqlDataSource></td><td style="font-size:15px;font-weight:bold;font-family: Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; "><asp:CheckBox ID="chkRecurringInvoice" runat="server" Visible="False" /></td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
    <asp:RadioButtonList ID="rbtInvoiceType" runat="server" Font-Size="15px" ForeColor="Black" RepeatDirection="Horizontal" Width="80%" AutoPostBack="True" Visible="False" ><asp:ListItem Text="Manual Invoice" Value="M" Selected="True"></asp:ListItem><asp:ListItem Text="Service Invoice" Value="S"></asp:ListItem></asp:RadioButtonList></td><td style="font-size:15px;font-weight:bold; font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
             <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"><SelectParameters><asp:ControlParameter ControlID="txtAccountIdBilling" Name="@AccountId" PropertyName="Text" /></SelectParameters></asp:SqlDataSource></td></tr>
    
         <tr ><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; vertical-align:top ">
             <asp:Label ID="lblBranch1" runat="server" Text="Branch"></asp:Label>
             </td><td style="width:20%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;"><asp:TextBox ID="txtLocation" runat="server" Height="16px" MaxLength="50" Width="80%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td></tr>
    <tr><td style=" font-size:15px;font-weight:bold; font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "><asp:DropDownList ID="ddlDocType" runat="server" DataTextField="ContType" DataValueField="ContType" Height="20px" Visible="False" Width="10%"><asp:ListItem>--SELECT--</asp:ListItem><asp:ListItem Value="ARIN">INVOICE</asp:ListItem><asp:ListItem Value="ARCN">CREDIT NOTE</asp:ListItem>
    <asp:ListItem Value="ARDN">DEBIT NOTE</asp:ListItem></asp:DropDownList></td><td style=" font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Status</td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtPostStatus" runat="server" AutoCompleteType="Disabled" Height="16px" Width="80%" TabIndex="20" BackColor="#CCCCCC"></asp:TextBox></td><td style="font-size:15px;font-weight:bold; font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:left; ">&nbsp;</td></tr><tr><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
    <asp:SqlDataSource ID="SqlDSBillingCode" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Description FROM tblbillingproducts order by Description "></asp:SqlDataSource><asp:TextBox ID="txtRcnoServiceRecord" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" Width="1%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Sales Order No.<asp:Label ID="Label13" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
    <asp:TextBox ID="txtInvoiceNo" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" TabIndex="20" Width="80%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:left; "><asp:CheckBox ID="chkManualInvoice" runat="server" AutoPostBack="True" Text="Manual Invoice" Visible="False" /></td></tr>
    
    <tr style="display:none"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "><asp:SqlDataSource ID="SqlDSTaxType" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT TaxType FROM tblTaxType order by TaxType "></asp:SqlDataSource></td>
    <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Batch No.</td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtInvoiceSchedule" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" TabIndex="20" Width="80%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%;  "><asp:CheckBox ID="chkReceiptAccess" runat="server" AutoPostBack="True" Visible="False" /></td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
    <asp:TextBox ID="txtTotalReceiptAmount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" TabIndex="20" Visible="False" Width="10%"></asp:TextBox><asp:TextBox ID="txtRcnoServiceRecordDetail" runat="server" AutoCompleteType="Disabled" Height="16px" Width="1%" BorderStyle="None" TabIndex="101"></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Sales Order Date<asp:Label ID="Label2" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtInvoiceDate" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="21" AutoPostBack="True" ></asp:TextBox>
    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtInvoiceDate" TargetControlID="txtInvoiceDate" Enabled="True" /></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; "><asp:CheckBox ID="chkQReceiptAccess" runat="server" AutoPostBack="True" Visible="False" /></td></tr>
    
    <tr style="display:none"><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "><asp:TextBox ID="txtTotalCNAmount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" TabIndex="20" Visible="False" Width="10%"></asp:TextBox></td>
    <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Billing Period</td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtBillingPeriod" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="18px" Width="81%" TabIndex="109" BorderStyle="None"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&#160;</td></tr><tr><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
    <asp:TextBox ID="txtFrom" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" TabIndex="20" Visible="False" Width="5%"></asp:TextBox><asp:TextBox ID="txtFrom1" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" TabIndex="20" Visible="False" Width="5%"></asp:TextBox><asp:TextBox ID="txtFrom2" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" TabIndex="20" Visible="False" Width="5%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Account Type</td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
    <asp:DropDownList ID="txtAccountType" runat="server" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="81%" AutoPostBack="True" TabIndex="22"><asp:ListItem>--SELECT--</asp:ListItem><asp:ListItem Value="COMPANY">CORPORATE</asp:ListItem><asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem></asp:DropDownList></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&#160;</td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "><asp:TextBox ID="txtBalanceBase" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" TabIndex="20" Visible="False" Width="10%"></asp:TextBox>
    <asp:TextBox ID="txtRcnoInvoice" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" TabIndex="102" Width="1%"></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Account ID<asp:Label ID="Label46" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtAccountIdBilling" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="22" AutoPostBack="True" ></asp:TextBox><asp:ImageButton ID="btnClient1" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/searchbutton.jpg" Width="24px" /></td>
    <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "><asp:TextBox ID="txtContractNo" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" TabIndex="103" Width="1%"></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Account Name </td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
    <asp:TextBox ID="txtAccountName" runat="server" Height="18px" Width="80%" AutoCompleteType="Disabled" TabIndex="23" ></asp:TextBox><asp:ImageButton ID="btnEditBillingName" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" /></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "><asp:TextBox ID="txtNetInvoiceAmount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" TabIndex="20" Visible="False" Width="10%"></asp:TextBox></td>
    <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Company Group<asp:Label ID="Label59" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtCompanyGroup" runat="server" Height="16px" Width="81%" BackColor="#CCCCCC" AutoCompleteType="Disabled" TabIndex="110" BorderStyle="None" ></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&#160;</td></tr><tr>
    <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&#160;</td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Contact Person</td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtContactPerson" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="24" Width="80%" MaxLength="100"></asp:TextBox>&nbsp;<asp:ImageButton ID="btnEditBillingDetails" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" /></td>
    <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "><asp:TextBox ID="txtLocationIdSelected" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" TabIndex="20" Visible="False" Width="10%"></asp:TextBox></td></tr><tr><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Billing Address<asp:Label ID="Label60" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td>
    <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtBillAddress" runat="server" AutoCompleteType="Disabled" Height="18px" TabIndex="25" Width="80%" MaxLength="100"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&#160;</td></tr><tr><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "><asp:TextBox ID="txtRowSelected" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" TabIndex="130" Width="1%"></asp:TextBox></td>
    <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtBillStreet" runat="server" Height="18px" MaxLength="100" Width="80%" TabIndex="26"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&#160;</td></tr><tr><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
    <asp:TextBox ID="txtBatchNo" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" TabIndex="104" Width="1%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtBillBuilding" runat="server" Height="18px" MaxLength="100" Width="80%" TabIndex="27"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&#160;</td></tr><tr>
    <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&#160;</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">City</td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
    <asp:TextBox ID="txtBillCity" runat="server" Height="18px" MaxLength="100" TabIndex="27" Width="80%"></asp:TextBox>
    </td>
    <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td></tr>
    <tr>
        <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
        <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">State</td>
        <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
            <asp:TextBox ID="txtBillState" runat="server" Height="18px" MaxLength="100" TabIndex="27" Width="80%"></asp:TextBox>
        </td>
        <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
    </tr>
    <tr>
        <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;<asp:TextBox ID="txtRcnoServiceBillingDetail" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" TabIndex="105" Width="1%"></asp:TextBox>
        </td>
        <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Country<asp:Label ID="Label61" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
        </td>
        <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
            <asp:TextBox ID="txtBillCountry" runat="server" Height="18px" MaxLength="100" TabIndex="28" Width="80%"></asp:TextBox>
        </td>
        <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
    </tr>
    <tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "><asp:TextBox ID="txtServiceName" runat="server" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" TabIndex="106" Width="1%"></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Postal </td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
    <asp:TextBox ID="txtBillPostal" runat="server" Height="18px" Width="80%" AutoCompleteType="Disabled" TabIndex="29" MaxLength="20" ></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Our Reference </td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
    <asp:TextBox ID="txtOurReference" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="30" MaxLength="25" ></asp:TextBox><asp:ImageButton ID="btnEditPONo" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" /></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "><asp:TextBox ID="txtRecordNo" runat="server" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" TabIndex="107" Width="1%"></asp:TextBox></td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
    <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Your Reference </td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtYourReference" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="31" MaxLength="25" ></asp:TextBox></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
    <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">PO No. </td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
             <asp:TextBox ID="txtPONo" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="32" MaxLength="100" ></asp:TextBox>&nbsp;</td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Credit Terms<asp:Label ID="Label44" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
             <asp:DropDownList ID="ddlCreditTerms" runat="server" AppendDataBoundItems="True" DataTextField="UPPER(Terms)" DataValueField="UPPER(Terms)" Height="25px" Width="80.5%" TabIndex="27" AutoPostBack="True"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td></tr><tr><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Credit Days</td>
    <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtCreditDays" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="65" Width="80%"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td></tr><tr><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Due Date</td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
    <asp:TextBox ID="txtDueDate" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" TabIndex="28" Width="80%"></asp:TextBox>
        
                                      </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "><asp:TextBox ID="txtCreatedOn" runat="server" BorderStyle="None" ForeColor="White" TabIndex="108" Width="1%"></asp:TextBox></td>
    <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Salesman<asp:Label ID="Label45" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
             <asp:DropDownList ID="ddlSalesmanBilling" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="StaffId" DataValueField="StaffId" Height="25px" Width="80.5%" TabIndex="29"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList><asp:ImageButton ID="btnEditSalesman" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" /></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td></tr><tr><td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 20%; text-align: right;">&nbsp;</td>
                 
                 <td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 10%; text-align: right;">Contract Group<asp:Label ID="Label58" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label></td><td style="width: 20%; font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left;"><asp:DropDownList ID="ddlContractGroupBilling" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="contractgroup" DataValueField="contractgroup" Height="25px" Value="-1" Width="80.6%"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td><td style="font-size: 15px; font-weight: bold; font-family: Calibri; text-align: left; color: black; padding-left: 1%; width: 20%; text-align: right;">&nbsp;</td></tr>
    <tr><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; vertical-align:top ">Description</td><td style="width:20%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;"><asp:TextBox ID="txtDescription" runat="server" AutoCompleteType="Disabled" Font-Names="Calibri" Font-Size="15px" Height="50px" MaxLength="200" TabIndex="30" TextMode="MultiLine" Width="79.5%"></asp:TextBox><asp:ImageButton ID="btnEditComments" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/Edit.png" Width="24px" /></td>
        <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td></tr><tr><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; vertical-align:top ">Remarks</td><td style="width:20%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;"><asp:TextBox ID="txtComments" runat="server" AutoCompleteType="Disabled" Font-Names="Calibri" Font-Size="15px" Height="100px" TabIndex="31" TextMode="MultiLine" Width="79.5%"></asp:TextBox>&#160;</td>
    <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;Total Price </td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtInvoiceAmount" runat="server" Text="0.00" style="text-align:right" Height="18px" Width="81%" AutoCompleteType="Disabled" BackColor="#CCCCCC" Rows="119" BorderStyle="None" ></asp:TextBox></td>
    <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&#160;</td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Discount Amount</td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtDiscountAmount" runat="server" Text="0.00" style="text-align:right" Height="18px" Width="81%" AutoCompleteType="Disabled" BackColor="#CCCCCC" Rows="120" BorderStyle="None" ></asp:TextBox></td>
    <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Amount with Discount</td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtAmountWithDiscount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="18px" Rows="120" style="text-align:right" Text="0.00" Width="81%" BorderStyle="None"></asp:TextBox></td>
    <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td></tr><tr><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
    <asp:TextBox ID="txtGSTSelected" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Rows="121" style="text-align:right" Width="20%" Visible="False"></asp:TextBox>
    </td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Tax Code</td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:DropDownList ID="txtGST" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="TaxType" DataValueField="TaxType" Height="25px" Value="-1" Width="80.6%" AutoPostBack="True" onclientlclick="ConfirmChangeTaxCode" ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></td>
    <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td></tr><tr><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Tax Rate</td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtGST1" runat="server" style="text-align:right" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" TabIndex="20" Width="81%" ></asp:TextBox></td>
    <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td></tr><tr><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">GST Amount</td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtGSTAmount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Rows="121" style="text-align:right" Text="0.00" Width="81%"></asp:TextBox></td>
    <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td></tr><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&#160;</td><td  style="font-size:15px;font-weight:bold;font-family:'Calibri'; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Net Amount</td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:TextBox ID="txtNetAmount" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="18px" Rows="121" style="text-align:right" Text="0.00" Width="81%" BorderStyle="None"></asp:TextBox></td>
    <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td></tr>
            
             <tr style="display:none" >
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; vertical-align:top ">GST Inclusive</td>
                 <td style="width:20%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                     <asp:CheckBox ID="chkGSTInclusive" runat="server" AutoPostBack="True" />
                 </td>
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
             </tr>
             </table>
</contenttemplate>
</asp:UpdatePanel>







<table border="0" style="width:90%; margin:auto; border:solid; border-color:ButtonFace;"><tr><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; "></td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"><asp:Button ID="btnImport" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="IMPORT SERVICES" Width="50%" TabIndex="32" />







<asp:ModalPopupExtender ID="mdlImportServices" runat="server" BackgroundCssClass="modalBackground" CancelControlID="ImageButton4" PopupControlID="pnlImportServices" TargetControlID="btnDummyImportService" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>







</td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                 <asp:Button ID="btnDeleteAll" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="Confirm()" TabIndex="31" Text="DELETE ALL" Width="40%"></asp:Button>







</td></tr></table>
                 
                 <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional"><ContentTemplate>
<asp:Panel ID="Panel2" runat="server" >
                         <table border="1" style="width:98%; padding-right:4%;  border:solid; border-color:ButtonFace;"><tr style="width:100%">
                             <td colspan="2" style="width:70%; text-align:left;color:#800000;padding-left:5%; background-color: #C0C0C0;">
                     <asp:Label ID="Label41" runat="server" style="font-size:15px;font-weight:bold;font-family:Calibri;  text-decoration: underline;" Text="SALES ORDER DETAILS"></asp:Label></td>
                             <td colspan="1" style="width:18%; text-align:right;color:#800000;padding-left:0%; background-color: #C0C0C0;"><asp:Label ID="Label21" runat="server" style="font-size:15px;font-weight:bold;font-family:Calibri;  text-decoration: underline;" Text="NO.OF RECORDS DISPLAYING : "></asp:Label></td>
                             <td colspan="1" style="width:11%;text-align:center;color:#800000;padding-left:0%; background-color: #C0C0C0;"><asp:Label ID="lbltotalservices" runat="server" style="font-size:15px;font-weight:bold;font-family:Calibri; " Text="0"></asp:Label></td></tr></table></asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>








                 
                 <asp:Panel ID="Panel5" runat="server" ScrollBars="Auto" style="overflow:scroll" Wrap="False"  BackColor="White" Width="100%" Height="300px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Left"><asp:UpdatePanel runat="server" ID="updPnlBillingRecs" UpdateMode="Conditional"><ContentTemplate>
<asp:Button ID="btnDummyGL" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Text="L" Width="24px" BorderStyle="None" ForeColor="White" /><asp:ModalPopupExtender ID="imgBtnGL_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlGLClose" PopupControlID="pnlPopUpGL" TargetControlID="btnDummyGL" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender><table border="1" style="width:100%; margin:auto"><tr><td colspan="12" style="text-align:left;"><asp:Label ID="Label22" runat="server" Text="View Records :" CssClass="CellFormat"></asp:Label>
    
    <asp:DropDownList ID="ddlViewServiceRecs" runat="server" AutoPostBack="True">
        <asp:ListItem >15</asp:ListItem>
        <asp:ListItem Selected="True">25</asp:ListItem>
        <asp:ListItem>50</asp:ListItem>
        <asp:ListItem>100</asp:ListItem>
        <asp:ListItem>500</asp:ListItem>
        <asp:ListItem>1000</asp:ListItem>
        <asp:ListItem>2000</asp:ListItem></asp:DropDownList></td></tr>
    <tr style="width:100%"><td colspan="20" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:96% "><asp:UpdatePanel runat="server" ID="updPnlBillingRecsNew" UpdateMode="Conditional"><ContentTemplate><asp:GridView ID="grvBillingDetailsNew"  runat="server" AllowSorting="True" 
               AutoGenerateColumns="False" CellPadding="1"  Height="12px" Font-Size="13px" Style="text-align: left" Width="99%" DataSourceID="SqlDSSalesDetail" GridLines="Horizontal" BorderStyle="None">
             <Columns><asp:TemplateField><ItemTemplate><asp:CheckBox ID="chkSelectRecB" runat="server" Font-Size="12px" Enabled="true" Height="18px" Visible="true"  Width="4%" AutoPostBack="false" Checked="true"    onchange="checkselectrec()"  CommandName="CHECK" ></asp:CheckBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText=" Item Type"><ItemTemplate><asp:TextBox ID="txtItemTypeGVB" runat="server" Text='<%# Bind("SuBcode")%>' Font-Size="11px" Height="15px" ReadOnly="true" Width="65px" Enabled="False" AppendDataBoundItems="True" AutoPostBack="True"  ></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Ref. No."><ItemTemplate>
                 <asp:TextBox ID="txtServiceRecordGVB" runat="server" Text='<%# Bind("RefType")%>' Font-Size="11px" style="text-align:left" Height="15px" Width="126px" Align="left" Enabled="False" AutoPostBack="false" AppendDataBoundItems="True"    ></asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField><asp:TemplateField HeaderText="ST"><ItemTemplate><asp:TextBox ID="txtServiceStatusGVB" runat="server" Text='<%# Bind("ServiceStatus")%>' Visible="true" Height="15px" Width="10px" Font-Size="11px" Enabled="false"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Contract No."><ItemTemplate><asp:TextBox ID="txtContractNoGVB" runat="server" Text='<%# bind("CostCode") %>' Height="15px" Width="129px" Font-Size="11px" Enabled="False"> </asp:TextBox></ItemTemplate>
                 <HeaderStyle HorizontalAlign="Center" /></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:ImageButton ID="BtnContractGVB" runat="server" OnClick="BtnContractGVB_Click" CssClass="righttextbox" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" /></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Item Desc."><ItemTemplate><asp:DropDownList ID="txtItemCodeGVB" runat="server" SelectedValue='<%# Bind("ItemDescription")%>' DataSourceID="SQLDSBillingCode" DataTextField="Description" Text='<%# Bind("ItemDescription")%>'  Font-Size="11px"  Height="20px" AutoPostBack="True"  Width="68px" AppendDataBoundItems="True"  ><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></ItemTemplate></asp:TemplateField>
                 <asp:TemplateField HeaderText="GL Code"   ><ItemTemplate><asp:TextBox ID="txtOtherCodeGVB" runat="server" Text='<%# Bind("LedgerCode")%>' Font-Size="11px" Visible="true" Enabled="false" Height="15px" Width="40px"> </asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:ImageButton ID="BtnGLGVB" runat="server" OnClick="BtnGLB_Click" CssClass="righttextbox" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" /></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="GL Desc."><ItemTemplate><asp:TextBox ID="txtGLDescriptionGVB" runat="server" Text='<%# Bind("LedgerName")%>' Font-Size="11px" Height="15px"  Width="60px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                 <asp:TemplateField HeaderText="Description"><ItemTemplate><asp:TextBox ID="txtDescriptionGVB" runat="server" Text='<%# Bind("Description")%>' Font-Size="11px" Height="15px"  Width="110px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="UOM"><ItemTemplate><asp:DropDownList ID="txtUOMGVB" runat="server" Text='<%# Bind("UnitMS")%>' SelectedValue='<%# Bind("UnitMS")%>' DataSourceID="SQLDSUnitMS" DataTextField="UnitMS" Font-Size="11px" Height="20px" Width="53px" AppendDataBoundItems="True"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField><asp:TemplateField HeaderText="Qty."><ItemTemplate>
                 <asp:TextBox ID="txtQtyGVB" runat="server" Text='<%# Bind("Quantity", "{0:n2}")%>' Font-Size="11px"  style="text-align:right" Height="15px"  Width="28px" AutoPostBack="true" OnTextChanged="txtQtyGVB_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Unit Price"><ItemTemplate><asp:TextBox ID="txtPricePerUOMGVB" runat="server" Text='<%# Bind("UnitOriginal", "{0:n2}")%>'  Font-Size="11px"  style="text-align:right" Height="15px"  Width="50px" AutoPostBack="false" OnTextChanged="txtPricePerUOMGVB_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Total Price"><ItemTemplate>
                 <asp:TextBox ID="txtTotalPriceGVB" runat="server" Text='<%# Bind("TotalPrice")%>' Font-Size="11px"   Enabled="false" style="text-align:right" Height="15px" Width="65px" Align="right"></asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField><asp:TemplateField HeaderText="Disc %"><ItemTemplate><asp:TextBox ID="txtDiscPercGVB" runat="server" Text='<%# Bind("DiscP", "{0:n2}")%>' Font-Size="11px"  style="text-align:right" Height="15px"  Width="38px" AutoPostBack="true" OnTextChanged="txtDiscPercGVB_TextChanged"></asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                 
                 <asp:TemplateField HeaderText="Disc Amt."><ItemTemplate>     <asp:TextBox ID="txtDiscAmountGVB" runat="server" Text='<%# Bind("DiscAmount", "{0:n2}")%>' Font-Size="11px"  style="text-align:right" Height="15px" Enabled="false"  Width="48px" AutoPostBack="true" OnTextChanged="txtDiscAmountGVB_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Price W Disc"><ItemTemplate><asp:TextBox ID="txtPriceWithDiscGVB" runat="server" Text='<%# Bind("ValueBase")%>' Font-Size="11px"   Enabled="false" style="text-align:right" Height="15px" Width="65px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
                 
                 <asp:TemplateField HeaderText="Tax Code"><ItemTemplate>     <asp:TextBox ID="txtTaxTypeGVB" runat="server" Text='<%# bind("GST") %>' DataSourceID="SQLDSTaxType" DataTextField="TaxType" Font-Size="11px" style="text-align:center" Height="15px" Width="46px" AutoPostBack="False"></asp:TextBox></ItemTemplate></asp:TemplateField>
                 <asp:TemplateField HeaderText="GST %"><ItemTemplate><asp:TextBox ID="txtGSTPercGVB" runat="server" Text='<%# Bind("GstRate", "{0:n2}")%>'   Font-Size="11px" Enabled="false" style="text-align:right" Height="15px" Width="35px"></asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                 <asp:TemplateField HeaderText="GST Amt."><ItemTemplate>
                 <asp:TextBox ID="txtGSTAmtGVB" runat="server" Text='<%# Bind("GSTBase")%>' Font-Size="11px" Enabled="true" AutoPostBack="true"  style="text-align:right" Height="15px" Width="47px" Align="right" OnTextChanged="txtGSTAmtGVB_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
                 
                 <asp:TemplateField HeaderText="Net Price"><ItemTemplate><asp:TextBox ID="txtTotalPriceWithGSTGVB" runat="server" Text='<%# Bind("AppliedBase")%>'  Font-Size="11px" Enabled="false" style="text-align:right" Height="15px" Width="65px" Align="right"></asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField><asp:TemplateField><ItemTemplate>
                 <asp:TextBox ID="txtLocationIdGVB" runat="server" Text='<%# Bind("LocationID")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField ><ItemTemplate><asp:ImageButton ID="btnDeleteDetail" runat="server" CssClass="righttextbox" Height="18px" ImageAlign="Top" ImageUrl="~/Images/delete_icon_color.gif" Width="20px" OnClick="btnDeleteDetail_Click" OnClientClick="currentdatetime(); Confirm()"  /></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtServiceLocationGroupGVB" runat="server" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                 <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtBillingFrequencyGVB" runat="server" Text='<%# Bind("BillingFrequency")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="0px"></asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField><asp:TemplateField ><ItemTemplate><asp:TextBox ID="txtServiceByGVB" runat="server" Text='<%# Bind("ServiceBy")%>' Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" Visible="false" BorderStyle="None" Height="18px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate>
                 <asp:TextBox ID="txtRcnoServiceRecordGVB" runat="server" Visible="false" Height="15px" Text='<%# Bind("RcnoServiceRecord")%>' Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoInvoiceGVB" runat="server" Visible="false" Height="15px" Text='<%# Bind("Rcno")%>' Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtServiceDateGVB" runat="server" Visible="false" Text='<%# Bind("ServiceDate")%>' Height="15px" Width="10px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate>
                 <asp:TextBox ID="txtItemDescriptionGVB" runat="server" Text='<%# Bind("ItemCode")%>' Font-Size="11px" Height="15px" Enabled="false" Width="0px" AutoPostBack="False" visible="false"></asp:TextBox></ItemTemplate></asp:TemplateField></Columns><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri"/><PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" /><RowStyle BackColor="#E4E4E4" ForeColor="#666666" Font-Names="Calibri"/><SelectedRowStyle BackColor="#738A9C" Font-Bold="False" ForeColor="White" /></asp:GridView></ContentTemplate></asp:UpdatePanel></td></tr></table><asp:UpdatePanel ID="updpnlBillingDetails" runat="server" UpdateMode="Conditional" ClientIDMode="AutoID"><ContentTemplate><table border="1" style="width:100%; margin:auto">
                         <tr style="width:100%"><td colspan="20" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:96% ">
                     <asp:GridView ID="grvBillingDetails"  runat="server" AllowSorting="True" 
             AutoGenerateColumns="False" CellPadding="1" 
             GridLines="None" Height="20px" Font-Size="13px"
            ShowFooter="True" Style="text-align: left" Width="99%" BorderStyle="None" ShowHeader="False" ><Columns>
                <asp:TemplateField><HeaderTemplate>
                    <asp:CheckBox ID="chkSelecAllRec" runat="server" AutoPostBack="false" onchange="checkselectallrecs()" TextAlign="Right" Width="5%"></asp:CheckBox></HeaderTemplate>
                    <ItemTemplate><asp:CheckBox ID="chkSelectRecGV" runat="server" AutoPostBack="false" Checked="false" Enabled="true" Font-Size="12px" Height="18px" onchange="checkselectrec()" Width="4%"></asp:CheckBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText=" Item Type"><ItemTemplate><asp:DropDownList ID="txtItemTypeGV" runat="server" Font-Size="11px" Height="20px" ReadOnly="true" Width="70px"  AppendDataBoundItems="True" AutoPostBack="True"  onselectedindexchanged="txtItemTypeGV_SelectedIndexChanged"><asp:ListItem Text="--SELECT--" Value="-1" /><asp:ListItem Text="SERVICE" Value="SERVICE" /><asp:ListItem Text="OTHERS" Value="OTHERS" /></asp:DropDownList></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Ref. No."><ItemTemplate><asp:TextBox ID="txtServiceRecordGV" runat="server" Font-Size="11px" Enabled="true" style="text-align:left" Height="15px" Width="128px" Align="right"  AutoPostBack="True" AppendDataBoundItems="True" OnTextChanged="txtServiceRecordGV_TextChanged" ></asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                <asp:TemplateField HeaderText="ST."><ItemTemplate><asp:TextBox ID="txtServiceStatusGV" runat="server" Visible="true" Height="15px" Width="10px" Font-Size="11px" Enabled="false"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Contract No."><ItemTemplate><asp:TextBox ID="txtContractNoGV" runat="server" Height="15px" Width="128px" Font-Size="11px" Enabled="False"  AutoPostBack="True" OnTextChanged="txtContractNoGV_TextChanged"> </asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:ImageButton ID="BtnContractGV" runat="server" OnClick="BtnContractGV_Click" CssClass="righttextbox" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" /></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Item Desc."><ItemTemplate><asp:DropDownList ID="txtItemCodeGV" runat="server" Font-Size="11px"  Height="20px" AutoPostBack="True"  Width="68px" AppendDataBoundItems="True"  onselectedindexchanged="txtItemCodeGV_SelectedIndexChanged"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="GL Code"   ><ItemTemplate><asp:TextBox ID="txtOtherCodeGV" runat="server" Font-Size="11px" Visible="true" Enabled="false" Height="15px" Width="40px"> </asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:ImageButton ID="BtnGL" runat="server" OnClick="BtnGL_Click" CssClass="righttextbox" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" /></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="GL Desc."><ItemTemplate><asp:TextBox ID="txtGLDescriptionGV" runat="server" Font-Size="11px" Height="15px"  Width="60px">

                     </asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Description"><ItemTemplate><asp:TextBox ID="txtDescriptionGV" runat="server" Font-Size="11px" Height="15px"  Width="110px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="UOM"><ItemTemplate><asp:DropDownList ID="txtUOMGV" runat="server" Font-Size="11px" Height="20px" Width="53px" AppendDataBoundItems="True"><asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                <asp:TemplateField HeaderText="Qty."><ItemTemplate><asp:TextBox ID="txtQtyGV" runat="server" Font-Size="11px" Text="1" style="text-align:right" Height="15px"  Width="28px" AutoPostBack="true" OnTextChanged="txtQtyGV_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Unit Price"><ItemTemplate><asp:TextBox ID="txtPricePerUOMGV" runat="server" Font-Size="11px" Text="0.00" style="text-align:right" Height="15px"  Width="50px" AutoPostBack="false" OnTextChanged="txtPricePerUOMGV_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Total Price"><ItemTemplate><asp:TextBox ID="txtTotalPriceGV" runat="server" Font-Size="11px" Text="0.00"  Enabled="false" style="text-align:right" Height="15px" Width="65px" Align="right"></asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                <asp:TemplateField HeaderText="Disc %"><ItemTemplate><asp:TextBox ID="txtDiscPercGV" runat="server"  Font-Size="11px" Text="0.00" style="text-align:right" Height="15px"  Width="38px" AutoPostBack="true" OnTextChanged="txtDiscPercGV_TextChanged"></asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField><asp:TemplateField HeaderText="Disc Amt."><ItemTemplate><asp:TextBox ID="txtDiscAmountGV" runat="server" Font-Size="11px" Text="0.00" style="text-align:right" Height="15px" Enabled="false"  Width="48px" AutoPostBack="true" OnTextChanged="txtDiscAmountGV_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Price W Disc"><ItemTemplate><asp:TextBox ID="txtPriceWithDiscGV" runat="server" Font-Size="11px" Text="0.00"  Enabled="false" style="text-align:right" Height="15px" Width="65px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
                
                <asp:TemplateField HeaderText="Tax Code"><ItemTemplate><asp:TextBox ID="txtTaxTypeGV" runat="server" Font-Size="11px" style="text-align:center" Height="15px" Width="46px" Enabled="false" AutoPostBack="True" ></asp:TextBox></ItemTemplate></asp:TemplateField>
              
                  <asp:TemplateField HeaderText="GST %"><ItemTemplate><asp:TextBox ID="txtGSTPercGV" runat="server" Text="0.00"  Font-Size="11px" Enabled="false" style="text-align:right" Height="15px" Width="35px"></asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                <asp:TemplateField HeaderText="GST Amt."><ItemTemplate><asp:TextBox ID="txtGSTAmtGV" runat="server" Font-Size="11px" Enabled="true" Text="0.00" style="text-align:right" Height="15px" Width="47px" Align="right" AutoPostBack="true" OnTextChanged="txtGSTAmtGV_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Net Price"><ItemTemplate><asp:TextBox ID="txtTotalPriceWithGSTGV" runat="server" Text="0.00" Font-Size="11px" Enabled="false" style="text-align:right" Height="15px" Width="65px" Align="right"></asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Right" /></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtLocationIdGV" runat="server" Font-Size="11px" Visible="false" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:CommandField ButtonType="Image"  DeleteImageUrl="~/Images/delete_icon_color.gif" ShowDeleteButton="True"   ><FooterStyle VerticalAlign="Top" /><ItemStyle Height="15px" /></asp:CommandField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtServiceLocationGroupGV" runat="server" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtBillingFrequencyGV" runat="server" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="0px"></asp:TextBox></ItemTemplate><HeaderStyle HorizontalAlign="Center" /></asp:TemplateField>
                <asp:TemplateField ><ItemTemplate><asp:TextBox ID="txtServiceByGV" runat="server" Font-Size="11px" ReadOnly="true" BackColor="#CCCCCC" Visible="false" BorderStyle="None" Height="18px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoServiceRecordGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoInvoiceGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtServiceDateGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:TextBox ID="txtItemDescriptionGV" runat="server" Font-Size="11px" Height="15px" Enabled="false" Width="0px" AutoPostBack="False" visible="false"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtOriginalBillAmountGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField><FooterStyle HorizontalAlign="Left" /><FooterTemplate><asp:Button ID="btnAddDetail" runat="server" OnClick="btnAddDetail_Click" Text="Add New Row" ValidationGroup="VGroup" Visible="false" /></FooterTemplate><ItemStyle ForeColor="#507CD1" /></asp:TemplateField></Columns><FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Font-Names="Calibri"/><PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                         <RowStyle BackColor="#E4E4E4" ForeColor="#8C4510" Font-Names="Calibri"/><SelectedRowStyle BackColor="#738A9C" Font-Bold="False" ForeColor="White" /></asp:GridView></td></tr></table></ContentTemplate></asp:UpdatePanel><asp:UpdatePanel ID="updPanelSave" runat="server" UpdateMode="Conditional"><ContentTemplate><table border="0" style="width:100%; margin:auto" ><tr><td colspan="1"  style="width:2%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right; "></td><td colspan="1" style="width:70px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"></td><td colspan="1" style="width:113px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"></td>
                         <asp:TextBox ID="txtxRow" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" Width="5%"></asp:TextBox><td colspan="10" style="width:10px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"></td><asp:TextBox ID="txtTotal" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Visible="False" Width="100%"></asp:TextBox><td></td><td colspan="1"  style="width:70px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"><asp:Button ID="btnSave" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="return DoValidation()" Text="SAVE" Visible="False" Width="85%" /></td>
                         </td colspan="1" style="width:40px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"><asp:TextBox ID="txtTotalDiscAmt" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align:right" Visible="False" Width="100%"></asp:TextBox><td colspan="1" style="width:80px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"><asp:TextBox ID="txtTotalWithDiscAmt" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Font-Size="12px"  Height="18px" style="text-align:right" Width="100%" Visible="True"></asp:TextBox></td><td colspan="1" style="width:40px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"></td>
                         <td colspan="1" style="width:30px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"></td><td colspan="1" style="width:50px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"><asp:TextBox ID="txtTotalGSTAmt" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Font-Size="12px"  Height="18px" style="text-align:right" Width="100%" Visible="True"></asp:TextBox></td><td colspan="1" style="width:80px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"><asp:TextBox ID="txtTotalWithGST" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Font-Size="12px"  Height="18px" style="text-align:right" Width="100%" Visible="True"></asp:TextBox></td>
                         <td colspan="1" style="width:25px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"></td></tr></table></ContentTemplate></asp:UpdatePanel>
</ContentTemplate>
</asp:UpdatePanel>







</asp:Panel>





     <div id="grvBillingDetailsdiv" runat="server" >

<asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional"><ContentTemplate>
<table border="0" style="width:90%; margin:auto; border:solid; border-color:ButtonFace;    "><tr style="width:100%"><td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">AMOUNT&nbsp;SUMMARY </td></tr><tr><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&#160;</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; vertical-align:top ">&nbsp;</td><td style="width:20%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;"></td>
    <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td></tr><tr><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&#160;</td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; width:90%; text-align:left; " colspan="2"><asp:GridView ID="grvGL" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " GridLines="None" Height="12px" ShowFooter="True" Style="text-align: left" Width="95%"><Columns><asp:TemplateField HeaderText="GL Code"><ItemTemplate>
    <asp:TextBox ID="txtGLCodeGV" runat="server" AppendDataBoundItems="True" AutoPostBack="True" BackColor="#CCCCCC" Font-Size="12px" Height="15px" onselectedindexchanged="txtItemTypeGV_SelectedIndexChanged" ReadOnly="true" Width="80px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Description"><ItemTemplate><asp:TextBox ID="txtGLDescriptionGV" runat="server" BackColor="#CCCCCC" Font-Size="12px" Height="15px" ReadOnly="true" Visible="true" Width="250px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Debit Amount"><ItemTemplate>
    <asp:TextBox ID="txtDebitAmountGV" runat="server" AppendDataBoundItems="True" AutoPostBack="True" BackColor="#CCCCCC" Font-Size="12px" Height="15px" onselectedindexchanged="txtItemCodeGV_SelectedIndexChanged" ReadOnly="true" style="text-align:right" Width="80px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Credit Amount"><ItemTemplate><asp:TextBox ID="txtCreditAmountGV" runat="server" BackColor="#CCCCCC" Font-Size="12px" Height="15px" ReadOnly="true" style="text-align:right" Width="80px"></asp:TextBox></ItemTemplate></asp:TemplateField><asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/delete_icon_color.gif" ShowDeleteButton="True" Visible="false"><FooterStyle VerticalAlign="Top" /><ItemStyle Height="15px" /></asp:CommandField><asp:TemplateField>
    <FooterStyle HorizontalAlign="Left" /><FooterTemplate><asp:Button ID="btnAddDetail" runat="server" Text="Add New Row" ValidationGroup="VGroup" Visible="false" /></FooterTemplate><ItemStyle ForeColor="#507CD1" /></asp:TemplateField></Columns><FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" /><RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" /><SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /><PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" /></asp:GridView></td>
    <td style="text-align: left; width:20%;font-size:15px; font-weight:bold; font-family:'Calibri'; color:black;text-align:right; padding-left: 1%;">&#160;</td></tr><tr><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "><asp:TextBox ID="txtARCode" runat="server" BorderStyle="None" ForeColor="White" Width="1%" TabIndex="108"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; "><asp:TextBox ID="txtARDescription" runat="server" BorderStyle="None" ForeColor="White" TabIndex="108" Width="1%"></asp:TextBox></td><td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
    <asp:TextBox ID="txtGSTOutputCode" runat="server" BorderStyle="None" ForeColor="White" Width="1%" TabIndex="108"></asp:TextBox></td><td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "><asp:TextBox ID="txtGSTOutputDescription" runat="server" BorderStyle="None" ForeColor="White" Width="1%" TabIndex="108"></asp:TextBox></td></tr><tr><td colspan="3" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"></td><td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%;  ">
    <asp:Button ID="btnSaveInvoice" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="if(this.value === 'Saving...') { return false; } else { this.value = 'Saving...'; } ; return DoValidation();" TabIndex="33" Text="SAVE SALES ORDER" Width="45%" /><asp:Button ID="btnCancel" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="45%" /></td></tr></table></div>
</ContentTemplate>
</asp:UpdatePanel>

         </div>





<div style="text-align:center"><asp:LinkButton ID="btnTop" runat="server" Font-Names="Calibri" Font-Bold="True" ForeColor="Brown">Go to Top</asp:LinkButton>







</div>
</ContentTemplate>
</asp:TabPanel>
                   
         <asp:TabPanel ID="TabPanel5" runat="server" HeaderText="Notes"><HeaderTemplate>
Notes
</HeaderTemplate>
<ContentTemplate>
<div style="text-align:center"><h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">Notes</h3><table style="width:100%;text-align:center;"><tr><td colspan="2" style="text-align:left;"><asp:Button ID="btnAddNotesMaster" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" /><asp:Button ID="btnEditNotesMaster" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" /><asp:Button ID="btnDeleteNotesMaster" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()"/><asp:Button ID="btn" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="False" /><asp:Button ID="btnQuitNotesMaster" runat="server" Visible="False"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CLOSE" Width="100px" /></td></tr><tr><td colspan="2"><br /></td></tr><tr class="Centered"><td colspan="2"><asp:GridView ID="gvNotesMaster" runat="server" DataSourceID="SqlDSNotesMaster" OnRowDataBound = "OnRowDataBoundgNotes" OnSelectedIndexChanged = "OnSelectedIndexChangedgNotes" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="15px" HorizontalAlign="Center" AllowSorting="True" GridLines="Vertical" ForeColor="#333333" ><AlternatingRowStyle BackColor="White" /><Columns><asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="False" ><ControlStyle Width="150px" /><ItemStyle Width="150px" /></asp:CommandField><asp:BoundField DataField="StaffID" HeaderText="StaffID" SortExpression="StaffID"><ControlStyle Width="150px" /><HeaderStyle Font-Size="12pt" /><ItemStyle Width="150px" HorizontalAlign="Left" /></asp:BoundField><asp:BoundField DataField="Notes" HeaderText="Notes" SortExpression="Notes"><ControlStyle Width="300px" /><HeaderStyle Font-Size="12pt" /><ItemStyle Width="300px" HorizontalAlign="Left" /></asp:BoundField><asp:BoundField DataField="CreatedBy" HeaderText="EntryStaff" SortExpression="CreatedBy" /><asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False"><EditItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label></EditItemTemplate><ItemTemplate><asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EntryDate" SortExpression="CreatedOn" /><asp:BoundField DataField="LastModifiedBy" HeaderText="EditedBy" SortExpression="LastModifiedBy" /><asp:BoundField DataField="LastModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="EditedOn" SortExpression="LastModifiedOn" /></Columns><EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" /><HeaderStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle ForeColor="White" HorizontalAlign="Center" BackColor="#507CD1" /><RowStyle BackColor="#EFF3FB" Font-Names="Calibri" /><SelectedRowStyle BackColor="#00CCFF" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle BackColor="#E9EBEF" /><SortedDescendingHeaderStyle BackColor="#4870BE" /></asp:GridView></td></tr><tr><td colspan="2"><br /></td></tr><tr><td class="CellFormat" style="text-align:right;padding-right:5px;">Invoice No.</td><td class="CellTextBox"><asp:Label ID="lblNotesKeyField" runat="server" BackColor="#CCCCCC" Height="18px" MaxLength="100" ReadOnly="True" Width="40%"></asp:Label></td></tr><tr><td class="CellFormat" style="text-align:right;padding-right:5px;">StaffID </td><td class="CellTextBox"><asp:Label ID="lblNotesStaffID" runat="server" MaxLength="100" Height="18px" Width="40%" ReadOnly="True" BackColor="#CCCCCC"></asp:Label></td></tr><tr><td class="CellFormat" style="text-align:right;padding-right:5px;">Notes<asp:Label ID="Label23" runat="server" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td><td class="CellTextBox"><asp:TextBox ID="txtNotes" runat="server" MaxLength="50" Height="60px" Width="80%" TextMode="MultiLine"></asp:TextBox></td></tr><tr><td colspan="2" style="text-align:right"><asp:Button ID="btnSaveNotesMaster" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/><asp:Button ID="btnCancelNotesMaster" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td></tr></table>
             <asp:SqlDataSource ID="SqlDSNotesMaster" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"><SelectParameters><asp:ControlParameter ControlID="lblNotesKeyField" Name="@keyfield" PropertyName="Text" /></SelectParameters></asp:SqlDataSource></div><asp:TextBox ID="txtNotesRcNo" runat="server" CssClass="dummybutton"></asp:TextBox><asp:TextBox ID="txtNotesMode" runat="server" CssClass="dummybutton"></asp:TextBox>
</ContentTemplate>
</asp:TabPanel>
           </asp:TabContainer>
            </td>
            </tr>
           </table>


         <%-- end Tabcontainer--%>
            

     
                              
        
          <table  style="width:90%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0% " >
             <tr style="text-align:center;width:90%">
                <td colspan="8" style="text-align:left;padding-left:5px;">
                   </td>
                                  <td colspan ="1" style="text-align:left; padding-right:2%">
                                    <asp:TextBox ID="txtDuplicateServiceRecord" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox> 
                                      </td>     
                  <td colspan ="1" style="text-align:left; padding-right:2%">
                                   <asp:TextBox ID="txtRecordAdded" runat="server" AutoCompleteType="Disabled" Height="16px" Width="40%" Visible="False"></asp:TextBox>   </td>              
                    <asp:TextBox ID="txtRecordDeleted" runat="server" AutoCompleteType="Disabled" Height="16px" Width="40%" Visible="False"></asp:TextBox>   </td>              
                                               
                    <td style="width:10%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;"> 
                                  <asp:TextBox ID="txtIsPopup" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox>
                                   </td>
                                  <td style="text-align:left;width:8%">
                                      <asp:Button ID="btnDummy" runat="server" CssClass="roundbutton" Font-Bold="True" Text=" " Width="24px" BackColor="White" BorderStyle="None" />
                                      &nbsp;&nbsp;&nbsp;
                                      <asp:Button ID="btnDummyImportService" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" />
                                   
                                      <asp:Button ID="btnDummyT" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" />
                                      &nbsp;<asp:Button ID="btnDummyC" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" />
                                         <asp:Button ID="btndummyPrint" runat="server" cssclass="dummybutton" /></td>
                  <asp:TextBox ID="txtRowCount" runat="server" CssClass="dummybutton"></asp:TextBox>
                                   <td style="width:10%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                           <asp:TextBox ID="txtSearch" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox>
                         </td>
                                  <td style="text-align:left;width:10%">
                           
                         </td>
                                  <td style="text-align: left">
                           
                                      </td>
                                   <td style="text-align: left">
                                        
                                  </td>
                            
                              </tr>

    </table>
         </div>

  

           <asp:Panel ID="pnlImportServices" runat="server" BackColor="White" Width="99%" Height="100%" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">
                     
        <table border="0"  style="width:100%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0%; border:solid; border-color:ButtonFace; "  >

                 <tr style="width:100%">
                 <td colspan="6" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:3%; color:#800000; background-color: #C0C0C0;"> SEARCH SERVICE RECORDS 
                     
                     </td>
                  </tr>
             
                    
                           <tr>
                          <td colspan="6"   style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert1" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td>
                         
                          </tr>
                                      
                                        
                     
            
                     <tr>

                          <td style="width:10%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> &nbsp;</td>

                            <td style="width:8%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">&nbsp;</td>

                          <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;" colspan="4"> Display Services&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                              <asp:RadioButton ID="rdbCompleted" runat="server" GroupName="ServiceStatus" Height="20px" Checked="True" Text="Completed" />
                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                              <asp:RadioButton ID="rdbNotCompleted" runat="server" GroupName="ServiceStatus" Height="20px" Text="Not Completed" />
                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                              <asp:RadioButton ID="rdbAll" runat="server"  GroupName="ServiceStatus" Height="20px" Text="All" />
                        
                               
                            </td>

                            <tr>
                                <td style="width:6%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Company Group</td>
                                <td style="width:9%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Account Type </td>
                                <td style="width:6%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Account Id </td>
                                <td colspan="2" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Client Name </td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Location </td>
                            </tr>
                            <tr>
                                <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:DropDownList ID="ddlCompanyGrp" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="companygroup" DataValueField="companygroup" Height="25px" Value="-1" Width="90%">
                                        <asp:ListItem Text="--SELECT--"  />
                                    </asp:DropDownList>
                                </td>
                                <td style="width:9%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:DropDownList ID="ddlContactType" runat="server" DataTextField="ContType" DataValueField="ContType" AppendDataBoundItems="True" Height="20px" Width="95%">
                                    <asp:ListItem Selected="True">--SELECT--</asp:ListItem>
                                             <asp:ListItem  Value="COMPANY">CORPORATE</asp:ListItem>
                                        <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtAccountId" runat="server" AutoCompleteType="Disabled" Height="16px" Width="80%"></asp:TextBox>
                                    <asp:ImageButton ID="btnClient" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="top" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                                    <asp:ModalPopupExtender ID="mdlPopUpClient" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlClientClose" Enabled="True" PopupControlID="pnlPopUpClient" TargetControlID="btnDummyClient">
                                    </asp:ModalPopupExtender>
                                </td>
                                <td colspan="2" style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtClientName" runat="server" AutoCompleteType="Disabled" Height="16px" Width="95%"></asp:TextBox>
                                </td>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtLocationId" runat="server" AutoCompleteType="Disabled" Height="16px" Width="80%"></asp:TextBox>
                                    <asp:ImageButton ID="BtnLocation" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top" ImageUrl="~/Images/searchbutton.jpg" Width="24px" Visible="False" />
                                    <asp:ModalPopupExtender ID="mdlPopupLocation" runat="server" BackgroundCssClass="modalBackground" CancelControlID="ImageButton4" PopupControlID="pnlPopUpLocation" TargetControlID="btnDummyLocation">
                                    </asp:ModalPopupExtender>
                                </td>
                            </tr>
                            <tr>
                                </td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Contract No</td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Service Frequency</td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Service From</td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Service To</td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Billing Frequency</td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Contract Group</td>
                            </tr>
                            <tr>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="ddlContractNo" runat="server" AutoCompleteType="Disabled" Height="16px" Width="80%"></asp:TextBox>
                               <asp:ImageButton ID="ImageButton2" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Top"  ImageUrl="~/Images/searchbutton.jpg" Width="24px" i  />
                                     </td>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:DropDownList ID="ddlServiceFrequency" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="Frequency" DataValueField="Frequency" Height="25px" Value="-1" Width="95%">
                                        <asp:ListItem Text="--SELECT--" Value="-1" />
                                    </asp:DropDownList>
                                </td>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtDateFrom" runat="server" AutoCompleteType="Disabled" Height="16px" Width="90%"></asp:TextBox>
                                    <asp:CalendarExtender ID="calConDate" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtDateFrom" TargetControlID="txtDateFrom" />
                                </td>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtDateTo" runat="server" AutoCompleteType="Disabled" Height="16px" Width="90%"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtDateTo" TargetControlID="txtDateTo" />
                                </td>
                                <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:DropDownList ID="ddlBillingFrequency" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="Frequency" DataValueField="Frequency" Height="25px" Value="-1" Width="90%">
                                        <asp:ListItem Text="--SELECT--" Value="-1" />
                                    </asp:DropDownList>
                                </td>
                                <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:DropDownList ID="ddlContractGroup" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="UPPER(contractgroup)" DataValueField="UPPER(contractgroup)" Height="25px" Value="-1" Width="95%">
                                        <asp:ListItem Text="--SELECT--" Value="-1" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                           
                        
                               
                            
                          
                          <tr>
                              <td colspan="1" style="width:10%;text-align:left;color:black;font-size:14px;font-weight:bold;font-family:'Calibri';">Service By</td>
                              <td style="width:8%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                  Service Address</td>
                              <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                  &nbsp;</td>
                              <td colspan="2" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                  &nbsp;</td>
                              <td colspan="1" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                  &nbsp;</td>
                          </tr>
                        
                               
                            
                          
                          <tr>
                              <td colspan="1" style="width:10%;text-align:left;color:black;font-size:14px;font-weight:bold;font-family:'Calibri';">
                                  <asp:TextBox ID="txtServiceBy" runat="server" AutoCompleteType="Disabled" Height="16px" Width="80%"></asp:TextBox>
                                  
                                  <asp:ImageButton ID="ImageButton1" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                              </td>
                              <td style="width:8%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                  <asp:TextBox ID="txtServiceAddress" runat="server" AutoCompleteType="Disabled" Height="16px" Width="95%"></asp:TextBox>
                            
                                    </td>
                              <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:Button ID="btnDummyClient" runat="server" BackColor="White" BorderStyle="None" CssClass="roundbutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
                                  <asp:Button ID="btnDummyLocation" runat="server" BackColor="White" BorderStyle="None" CssClass="roundbutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
                           
                                            <asp:SqlDataSource ID="SqlDSServices" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
                                  <asp:TextBox ID="txtTaxRatePct" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" Width="35%"></asp:TextBox>
                                  <asp:Button ID="btnDummyStatusSearch" runat="server" BackColor="White" BorderStyle="None" CssClass="roundbutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
                              </td>
                              <td colspan="2" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                  <asp:RadioButtonList ID="rdbGrouping" runat="server" CausesValidation="True" CellPadding="5" CellSpacing="3" Font-Size="12px" Height="10px" RepeatDirection="Horizontal" Visible="False" Width="100%">
                                      <asp:ListItem Selected="True">Group by Contract No.</asp:ListItem>
                                      <asp:ListItem Value="LocationID">Group by Location</asp:ListItem>
                                      <asp:ListItem Value="ContractNo">Group by Account ID</asp:ListItem>
                                      <asp:ListItem Value="ServiceLocationCode">Group by Service Location</asp:ListItem>
                                  </asp:RadioButtonList>
                              </td>
                              <td colspan="1" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                  <asp:Button ID="btnShowRecords" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="return DoValidationShowRecords()" Text="SHOW RECORDS" Width="80%" />
                              </td>
                          </tr>
                        
                               
                            
                          
                        </tr>

                        </table>
    
            
        

             <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" style="overflow:scroll" Wrap="false"  BackColor="White" Width="100%" Height="340px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">
       
         
         <table border="1" style="width:100%; margin:auto; border:solid; border-color:ButtonFace;">
             
               <tr style="width:100%">
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:5%; color:#800000; background-color: #C0C0C0;">
                     <asp:Label ID="Label43" runat="server" Text="SERVICE BILLING DETAILS"></asp:Label>
            </td>

               </tr>
               
             <tr style="width:100%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:100% ">
        
        
        <asp:UpdatePanel ID="updpnlServiceRecs" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <asp:GridView ID="grvServiceRecDetails" runat="server" AllowSorting="True"  
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="12px"  Font-Size="14px"
            ShowFooter="True" Style="text-align: left" Width="100%">
                
                  <Columns> 
                              
                <asp:TemplateField> <HeaderTemplate><asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="false" TextAlign="Right" onchange="checkallservicerecs()"  Width="5%" ></asp:CheckBox></HeaderTemplate>
                <ItemTemplate><asp:CheckBox ID="chkSelectGV" runat="server" Font-Size="12px" Enabled="true" Height="18px"  Width="5%" AutoPostBack="false"    onchange="checkoneservicerec()"  CommandName="CHECK" ></asp:CheckBox></ItemTemplate></asp:TemplateField>            
               <asp:TemplateField HeaderText="Service Record"><ItemTemplate><asp:TextBox ID="txtServiceRecordNoGV" runat="server" Text='<%# Bind("RecordNo")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="140px"></asp:TextBox></ItemTemplate></asp:TemplateField>
          
                            <asp:TemplateField HeaderText="Company"><ItemTemplate><asp:TextBox ID="txtCompanyGroupGV" runat="server" Text='<%# Bind("CompanyGroup")%>' Visible="true" Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="65px"></asp:TextBox></ItemTemplate></asp:TemplateField>
             <asp:TemplateField HeaderText="Service Date"><ItemTemplate><asp:TextBox ID="txtServiceDateGV" runat="server" Text='<%# Bind("ServiceDate", "{0:dd/MM/yyyy}")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="70px" ></asp:TextBox></ItemTemplate></asp:TemplateField>
            
                            <asp:TemplateField HeaderText="Acct. Id" HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtAccountIdGV" runat="server" Text='<%# Bind("AccountId")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="60px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Client Name"><ItemTemplate><asp:TextBox ID="txtClientNameGV" runat="server" Text='<%# Bind("CustName")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="160px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Location Id"><ItemTemplate><asp:TextBox ID="txtLocationIdGV" runat="server" Text='<%# Bind("LocationID")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="88px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Service Address"><ItemTemplate><asp:TextBox ID="txtServiceAddressGV" runat="server" Text='<%# Bind("Address1")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="200px"></asp:TextBox></ItemTemplate></asp:TemplateField>
            
               <asp:TemplateField HeaderText="Cont. Grp."><ItemTemplate><asp:TextBox ID="txtDeptGV" runat="server" Text='<%# Bind("ContractGroup")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="56px"></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText="Contract No."><ItemTemplate><asp:TextBox ID="txtContractNoGV" runat="server" Text='<%# Bind("ContractNo")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="143px"></asp:TextBox></ItemTemplate></asp:TemplateField>                             
               <asp:TemplateField HeaderText="St."><ItemTemplate><asp:TextBox ID="txtStatusGV" runat="server" Text='<%# Bind("Status")%>' Style="text-align: center" Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="20px"></asp:TextBox></ItemTemplate></asp:TemplateField>        
          
               <asp:TemplateField HeaderText="Loc.Group"><ItemTemplate><asp:TextBox ID="txtServiceLocationGroupGV" runat="server"  Text='<%# Bind("ServiceLocationGroup")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="63px"></asp:TextBox></ItemTemplate></asp:TemplateField>
          
               <asp:TemplateField HeaderText="Service By" HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtServiceByGV" runat="server" Text='<%# Bind("ServiceBy")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" Visible="true" BorderStyle="None" Height="18px" Width="80px"></asp:TextBox></ItemTemplate></asp:TemplateField>
             
                <asp:TemplateField HeaderText="Billing Freq." HeaderStyle-HorizontalAlign="Center"><ItemTemplate><asp:TextBox ID="txtBillingFrequencyGV" runat="server" Text='<%# Bind("BillingFrequency")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="80px"></asp:TextBox></ItemTemplate>
                
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
           
                        <asp:TemplateField HeaderText="Bill Amt."><ItemTemplate><asp:TextBox ID="txtToBillAmtGV" runat="server" Text='<%# Bind("BillAmount")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" style="text-align:right" Height="18px" Width="70px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
              
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoServicebillingdetailGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoServiceRecordGV" runat="server"  Text='<%# Bind("Rcno")%>' Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoServiceRecordDetailGV" runat="server" Visible="false" Height="1px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoInvoiceGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtContactTypeGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                             
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtInvoiceDateGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtDiscAmountGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtGSTAmountGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtNetAmountGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              
               <asp:TemplateField><ItemTemplate>
                    <asp:Button ID="btnViewEdit" runat="server" Visible="false" Text="E" Width="0px" OnClick="btnViewEdit_Click"  />
                   </ItemTemplate>
                   </asp:TemplateField>
                                                                
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtAccountNameGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtBillAddress1GV" runat="server" Visible="false" Height="1px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtBillBuildingGV" runat="server" Visible="false" Height="1px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtBillStreetGV" runat="server" Visible="false" Height="1px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtBillCountryGV" runat="server" Visible="false" Height="1px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtBillPostalGV" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtOurReferenceGV" runat="server" Text='<%# Bind("OurRef")%>' Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtYourReferenceGV" runat="server" Text='<%# Bind("YourRef")%>' Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtPoNoGV" runat="server" Visible="false" Height="15px" Width=".5px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtCreditTermsGV" runat="server"  Text='<%# Bind("ARTerm")%>' Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtSalesmanGV" runat="server" Text='<%# Bind("Salesman")%>' Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtNotesGV" runat="server" Text='<%# Bind("Notes")%>' Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
            
                         </Columns>

        <FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" /><RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" /></asp:GridView>
            </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="grvServiceRecDetails" EventName="SelectedIndexChanged" /></Triggers></asp:UpdatePanel>&nbsp;</td></tr>
               
            
        </table>


                 </asp:Panel>


                <table border="0" style="width:99%; margin:auto; border:solid; border-color:ButtonFace;">
                     <tr>
                        <td  style="width:30%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
                 <asp:UpdatePanel ID="UpdateSelect" runat="server" UpdateMode="Conditional"><ContentTemplate>
                            <asp:Button ID="btnSelect" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="SELECT" Width="10%" Visible="False" />
                </ContentTemplate></asp:UpdatePanel>
                       </td>
                         
                    <td  style="width:30%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right">
                            Total
                       </td>

                   <td  style="width:30%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left">
                   <asp:TextBox ID="txtTotalServiceSelected" runat="server" style="text-align:right" AutoCompleteType="Disabled"  ForeColor="Black" Height="16px" Width="35%" BackColor="#CCCCCC"></asp:TextBox>
                             
                       </td>
                 
            </tr>
                    </table>

                <table border="0" style="width:99%; margin:auto; border:solid; border-color:ButtonFace;">
                     <tr>
                      
                         
                  <td  style="width:80%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
                       <asp:Button ID="btnImportService" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="IMPORT SERVICES" Width="15%" Visible="True" />
                       &nbsp;  
                           <asp:Button ID="btnClose" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="CANCEL" Width="15%" Visible="True" />
                    
                      
                        </td>
                 
            </tr>
                    </table>
         </asp:Panel>



    
         
<asp:Panel ID="pnlPopUpClient" runat="server" BackColor="White" Width="98%" Height="80%" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center" ScrollBars="Horizontal">
    <table style="margin-left:auto; margin-right:auto; "><tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Customer</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlClientClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" />
    </td></tr>
        
        <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri; color:black;text-align:center;padding-left:40px;">&#160;Search Name &#160;&#160;
         <asp:TextBox ID="txtPopUpClient" runat="server" Height="16px" MaxLength="50" Width="400px" ForeColor="Gray" onblur = "WaterMarkClient(this, event);" onfocus = "WaterMarkClient(this, event);" AutoPostBack="True">Search Here for AccountID or Client Name or Contact Person</asp:TextBox>
    <asp:ImageButton ID="btnPopUpClientSearch" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" OnClick="btnPopUpClientSearch_Click" Width="24px" Visible="False"></asp:ImageButton>
    <asp:ImageButton ID="btnPopUpClientReset" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/resetbutton.jpg" OnClick="btnPopUpClientReset_Click" Width="24px"></asp:ImageButton>


</td><td>
                <asp:TextBox ID="txtPopupClientSearch" runat="server" Visible="False"></asp:TextBox>

            </td></tr>


    </table><div style="text-align:center; padding-left: 20px; padding-bottom: 5px;"><div class="AlphabetPager">
          <asp:Repeater ID="rptClientAlphabets" runat="server" Visible="False"><ItemTemplate>
        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("Value")%>' OnClick="ClientAlphabet_Click" ForeColor="Black" />
        </ItemTemplate>
        </asp:Repeater>
                
</div><br />
                    <asp:GridView ID="gvClient" runat="server" DataSourceID="SqlDSClient" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                       CellPadding="2" GridLines="Vertical" Font-Size="15px" Width="97%" OnRowDataBound = "OnRowDataBoundgClient" OnSelectedIndexChanged = "OnSelectedIndexChangedgClient" CellSpacing="6">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
        <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
        <ControlStyle Width="5%" />

        <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle Width="5%" />
        </asp:CommandField>
            <asp:BoundField DataField="AccountType" HeaderText="Account Type" />
        <asp:BoundField DataField="AccountID" HeaderText="Account ID" SortExpression="AccountID" >
        <ControlStyle Width="8%" />

        <HeaderStyle Wrap="False" HorizontalAlign="Left" />

        </asp:BoundField>
        <asp:BoundField DataField="LocationID" HeaderText="Location ID" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Wrap="False" />
        </asp:BoundField>

    <asp:BoundField DataField="ContractGroup" HeaderText="Contract Group" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Wrap="False" />
        </asp:BoundField>


        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID">
        <ControlStyle Width="8%" />

        <HeaderStyle Width="100px" HorizontalAlign="Left" />

        <ItemStyle Width="10%" Wrap="False" />
        </asp:BoundField>
        <asp:BoundField DataField="BillingNameSvc" HeaderText="Client Name" SortExpression="Name">
        <ControlStyle Width="35%" />

        <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle Wrap="False" />
        </asp:BoundField>
        <asp:BoundField DataField="ServiceName" HeaderText="Service Name" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Wrap="False" />
        </asp:BoundField>
        <asp:BoundField DataField="BillContact1Svc" HeaderText="Contact Person" SortExpression="BillContact1Svc">

        <HeaderStyle Wrap="False" HorizontalAlign="Left" />

        <ItemStyle Wrap="False" />
        </asp:BoundField>
        <asp:BoundField DataField="Address1" HeaderText="Service Address" >

            <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle Wrap="False" HorizontalAlign="Left" />
        </asp:BoundField>
            <asp:BoundField DataField="BillAddressSvc" HeaderText="Bill Address">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BillStreetSvc" HeaderText="Street">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BillBuildingSvc" HeaderText="Building">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BillCitySvc" HeaderText="City">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BillStateSvc" HeaderText="State">
            </asp:BoundField>
            <asp:BoundField DataField="BillCountrySvc" HeaderText="Country">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="BillPostalSvc" HeaderText="Postal">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
        <asp:BoundField DataField="Telephone" HeaderText="Telephone" >

            <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle Wrap="False" />
        </asp:BoundField>
        <asp:BoundField DataField="Mobile" HeaderText="Mobile" >

            <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle Wrap="False" />
            <ItemStyle Wrap="False" />
        </asp:BoundField>
            <asp:BoundField DataField="ARTermSvc">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="SalesmanSvc">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="ContactPerson">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" Wrap="False" />
            </asp:BoundField>
            <asp:BoundField DataField="CompanyGroup">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" />
            </asp:BoundField>
            <asp:BoundField DataField="CompanyGroupD">
            <ControlStyle CssClass="dummybutton" />
            <HeaderStyle CssClass="dummybutton" />
            <ItemStyle CssClass="dummybutton" />
            </asp:BoundField>
            <asp:BoundField DataField="Location" HeaderText="Location">
            <ItemStyle Wrap="False" />
            </asp:BoundField>
        </Columns>

        <EditRowStyle BackColor="#999999" />

        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />

        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Left" />

        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" />

        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />

        <SortedAscendingCellStyle BackColor="#E9E7E2" />

        <SortedAscendingHeaderStyle BackColor="#506C8C" />

        <SortedDescendingCellStyle BackColor="#FFFDF8" />

        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>



                <asp:SqlDataSource ID="SqlDSClient" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                        ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"><FilterParameters>
        <asp:ControlParameter Name="ContType" ControlID="ddlContactType" PropertyName="SelectedValue" Type="String" />
        </FilterParameters>
        </asp:SqlDataSource>
                
                <asp:SqlDataSource ID="SqlDSPerson" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                        ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"><FilterParameters>
        <asp:ControlParameter Name="ContType" ControlID="ddlContactType" PropertyName="SelectedValue" Type="String" />
        </FilterParameters>
        <SelectParameters>
        <asp:ControlParameter ControlID="ddlContactType" Name="@contType" PropertyName="Text" />
      
        </SelectParameters>
        </asp:SqlDataSource>
        </div></asp:Panel>


       <asp:Panel ID="pnlPopUpLocation" runat="server" BackColor="White" Width="1000px" Height="700px" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">

        <table>
            <tr>
                <td style="text-align: center;">
                    <h4 style="color: #000000">Select Location</h4>
                </td>
                <td style="width: 1%; text-align: right;">
    <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
            </tr>
        </table>

     
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="gvLocation" runat="server" DataSourceID="SqlDSCompanyLocation" OnRowDataBound = "OnRowDataBoundgLoc" OnSelectedIndexChanged = "OnSelectedIndexChangedgLoc" ForeColor="#333333" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="800px"  Font-Size="15px" PageSize="8">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                    <asp:BoundField DataField="LocationID" HeaderText="LocationID" SortExpression="LocationID" />
                    <asp:BoundField DataField="ServiceName" HeaderText="Service Name" SortExpression="ServiceName" />
                    <asp:BoundField DataField="Address1" HeaderText="Address" />
                    <asp:BoundField DataField="AddBuilding" HeaderText="Building" />
                    <asp:BoundField DataField="AddStreet" HeaderText="Street" />
                    <asp:BoundField DataField="AddCity" HeaderText="City" />
                    <asp:BoundField DataField="LocateGRP" HeaderText="Zone" />
                    <asp:BoundField DataField="ContactPerson" >

                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>

                    <asp:BoundField DataField="ContactPerson2"  >
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Contact1Position" >
                    <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Contact2Position" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Telephone" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Telephone2" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Fax" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Contact2Fax" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Contact2Tel" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Contact2Tel2" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Mobile" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Contact2Mobile" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Email" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                    <asp:BoundField DataField="Contact2Email" >
                         <ControlStyle CssClass="dummybutton" />
                    <HeaderStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField >
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Names="Calibri" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDSCompanyLocation" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtAccountId" Name="@Accountid" PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>
      
            <asp:SqlDataSource ID="SqlDSPersonLocation" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                <SelectParameters>
                    <asp:ControlParameter ControlID="txtAccountId" Name="@Accountid" PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>
              </div>
    </asp:Panel>
       



         <asp:Panel ID="pnlPopUpGL" runat="server" BackColor="White" Width="1000px" Height="600px" BorderColor="#003366" BorderWidth="1px"    HorizontalAlign="Left" ScrollBars="Vertical">

                     <table>
           <tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Select GL Code</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlTeamClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                           
         <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;padding-left:40px;">&nbsp;Search &nbsp; <asp:TextBox ID="txtPopUpGL" runat="server" MaxLength="50" Height="16px" Width="400px" ForeColor="Gray" onblur = "WaterMarkGL(this, event);" onfocus = "WaterMarkGL(this, event);" AutoPostBack="True">Search Here for GL Code or Description</asp:TextBox>
    <asp:ImageButton ID="btnPopUpGLSearch"  runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle" Visible="False" /><asp:ImageButton ID="btnPopUpGLReset"  runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/></td><td>
                               <asp:TextBox ID="txtPopupTeamSearch" runat="server" Visible="False" Width="20%"></asp:TextBox>
                           </td></tr>


        </table>

     
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="GrdViewGL" runat="server" DataSourceID="SqlDSGL" OnRowDataBound = "OnRowDataBoundgGL" OnSelectedIndexChanged = "OnSelectedIndexChangedgGL" ForeColor="#333333" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="800px"  Font-Size="15px" PageSize="8">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                    <asp:BoundField DataField="COACode" HeaderText="Code" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="GLType" >
                    <ControlStyle CssClass="dummybutton" />
                    <ItemStyle CssClass="dummybutton" />
                    </asp:BoundField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Names="Calibri" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDSGL" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
            </asp:SqlDataSource>
      
              </div>
    </asp:Panel>


         
             <asp:Panel ID="pnlStatusSearch" runat="server" BackColor="White" Width="40%" Height="40%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          <br /><br />    <table style="font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:left;padding-left:20px;">
                  <tr>
                      <td>
                          <asp:RadioButtonList ID="rdbStatusSearch" runat="server" AutoPostBack="True" Visible="False">
                              <asp:ListItem Value="ALL">ALL</asp:ListItem>
                              <asp:ListItem Value="Status">STATUS</asp:ListItem>
                          </asp:RadioButtonList>
                     <asp:checkboxlist ID="chkStatusSearch" runat="server" Width="100%" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="3" CellPadding="2" CellSpacing="2" TextAlign="Right">
                                   <asp:ListItem Value="O">O - Open/Pending</asp:ListItem>
                                   <asp:ListItem Value="P">P - Posted</asp:ListItem>   
                                   <asp:ListItem Value="V">V - Void</asp:ListItem>  
                                  
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


        <asp:Panel ID="pnlPrint" runat="server" BackColor="White" Width="50%" Height="98%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
            
                     <table border="0" style="width:100%;padding-left:15px;">
                         <tr>
                              <td> <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099; padding-left: 0%;">Print Invoice</h4></td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnpnlPrintClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                
                        
                           <%-- <tr>
                                 <td class="CellFormat" style="text-align:left;padding-left:20%;">
                                     <asp:RadioButtonList ID="RadioButtonList1" runat="server" CellSpacing="5" CellPadding="5" RepeatDirection="Horizontal">
                                         <asp:ListItem Selected="True"> Format 1</asp:ListItem>
                                         <asp:ListItem>Format 2</asp:ListItem>
                                           <asp:ListItem>Format 3</asp:ListItem>
                                     </asp:RadioButtonList>
                              </td>
                        </tr>--%>
                        

                         <tr>
                             <td>
        <table style="border: 1px solid #CC3300; text-align:center; border-radius: 25px;padding: 5px; width:90%; height:350px; background-color: #F3F3F3;padding-left:15px;">
                         <tr>
                             
                 <td colspan="5" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:2%;">INVOICE</td></tr>
                           
            
            
            
            
             <tr style="padding-top:40px;">
                 <td colspan="1" style="text-align:left;padding-left:50PX;">
                                  <a href="RV_TaxInvoice_Format1.aspx?Export=PDF" target="_blank" id="btnPrintInvoice1" runat="server" ><imagebutton style="border:none;" type="button"  ><img src="Images/Print.jpg" width="25" height="20"/></imagebutton></a>
                                 </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                                  <a href="RV_TaxInvoice_Format1.aspx?Export=Word" target="_blank" id="btnExportInvoice1" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                      </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">          
                        <a href="RV_TaxInvoice_Format1.aspx?Export=View" target="_blank" id="btnViewInvoice1" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/view.jpg" width="28" height="20" /></imagebutton></a>
                      </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">  
                                <a href="Email.aspx?Type=InvoiceFormat1" target="_blank" id="btnEmailInvoice1" runat="server"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                      </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">             
                     
                      <label style="font-weight:bold;color:black;width:100px;font-family:Calibri;font-size:15px; vertical-align: super;">Invoice with Service Record No</label><br />

                        </td>
                 </tr>



            <tr style="padding-top:40px;">
                 <td colspan="1" style="text-align:left;padding-left:50PX;">
                           <a href="RV_TaxInvoice_Format2.aspx?Export=PDF" target="_blank" id="btnPrintInvoice2" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/Print.jpg" width="25" height="20" /></imagebutton></a>
                             </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">      
                            <a href="RV_TaxInvoice_Format2.aspx?Export=Word" target="_blank" id="btnExportInvoice2" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                       </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">            
                      <a href="RV_TaxInvoice_Format2.aspx?Export=View" target="_blank" id="btnViewInvoice2" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/view.jpg" width="28" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                               <a href="Email.aspx?Type=InvoiceFormat2" target="_blank" id="btnEmailInvoice2" runat="server"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                       </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">            
                      <label style="font-weight:bold;color:black;width:100px;font-family:Calibri;font-size:15px">For Annual Billing</label><br />

                             </td>
                 </tr>



            <tr style="padding-top:40px;">
                 <td colspan="1" style="text-align:left;padding-left:50PX;">
                                    <a href="RV_TaxInvoice_Format3.aspx?Export=PDF" target="_blank" id="btnPrintInvoice3" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/Print.jpg" width="25" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                                  <a href="RV_TaxInvoice_Format3.aspx?Export=Word" target="_blank" id="btnExportInvoice3" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                       </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">            
                      <a href="RV_TaxInvoice_Format3.aspx?Export=View" target="_blank" id="btnViewInvoice3" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/view.jpg" width="28" height="20" /></imagebutton></a>
                                  </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                               <a href="Email.aspx?Type=InvoiceFormat3" target="_blank" id="btnEmailInvoice3" runat="server"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                               </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">    
                      <label style="font-weight:bold;color:black;width:100px;font-family:Calibri;font-size:15px">No Service Address and No Billing Frequency</label><br />

                             </td>
                 </tr>



            <tr style="padding-top:40px;">
                 <td colspan="1" style="text-align:left;padding-left:50PX;">
                                    <a href="RV_TaxInvoice_Format4.aspx?Export=PDF" target="_blank" id="btnPrintInvoice4" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/Print.jpg" width="25" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                                  <a href="RV_TaxInvoice_Format4.aspx?Export=Word" target="_blank" id="btnExportInvoice4" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                       </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">            
                     <a href="RV_TaxInvoice_Format4.aspx?Export=View" target="_blank" id="btnViewInvoice4" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/view.jpg" width="28" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                               <a href="Email.aspx?Type=InvoiceFormat4" target="_blank" id="btnEmailInvoice4" runat="server"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                       </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">            
                      <label style="font-weight:bold;color:black;width:100px;font-family:Calibri;font-size:15px">No System Contract No</label><br />


                             </td>
                 </tr>
           
            
             <tr style="padding-top:40px;">
                 <td colspan="1" style="text-align:left;padding-left:50PX;">
                                    <a href="RV_TaxInvoice_Format5.aspx?Export=PDF" target="_blank" id="btnPrintInvoice5" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/Print.jpg" width="25" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                                  <a href="RV_TaxInvoice_Format5.aspx?Export=Word" target="_blank" id="btnExportInvoice5" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                                <a href="RV_TaxInvoice_Format5.aspx?Export=View" target="_blank" id="btnViewInvoice5" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/view.jpg" width="28" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                               <a href="Email.aspx?Type=InvoiceFormat5" target="_blank" id="btnEmailInvoice5" runat="server"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                      <label style="font-weight:bold;color:black;width:100px;font-family:Calibri;font-size:15px">Sales Invoice W/O Service Description</label><br />


                             </td>
                 </tr>


            <tr style="padding-top:40px;">
                 <td colspan="1" style="text-align:left;padding-left:50PX;">
                                    <a href="RV_TaxInvoice_Format6.aspx?Export=PDF" target="_blank" id="btnPrintInvoice6" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/Print.jpg" width="25" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                                  <a href="RV_TaxInvoice_Format6.aspx?Export=Word" target="_blank" id="btnExportInvoice6" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;"> 
                                <a href="RV_TaxInvoice_Format6.aspx?Export=View" target="_blank" id="btnViewInvoice6" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/view.jpg" width="28" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                               <a href="Email.aspx?Type=InvoiceFormat6" target="_blank" id="btnEmailInvoice6" runat="server" ><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                      <label style="font-weight:bold;color:black;width:100px;font-family:Calibri;font-size:15px">Sales Invoice W/O Billing Frequency</label><br />


                             </td>
                 </tr>


            <tr style="padding-top:40px;">
                 <td colspan="1" style="text-align:left;padding-left:50PX;">
                                    <a href="RV_TaxInvoice_Format7.aspx?Export=PDF" target="_blank" id="btnPrintInvoice7" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/Print.jpg" width="25" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                                  <a href="RV_TaxInvoice_Format7.aspx?Export=Word" target="_blank" id="btnExportInvoice7" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;"> 
                                <a href="RV_TaxInvoice_Format7.aspx?Export=View" target="_blank" id="btnViewInvoice7" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/view.jpg" width="28" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                               <a href="Email.aspx?Type=InvoiceFormat7" target="_blank" id="btnEmailInvoice7" runat="server" ><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                      <label style="font-weight:bold;color:black;width:100px;font-family:Calibri;font-size:15px">Sales Invoice - Lump Sum</label><br />

                             </td>
                 </tr>


            <tr style="padding-top:40px;">
                 <td colspan="1" style="text-align:left;padding-left:50PX;">
                                    <a href="RV_TaxInvoice_Format8.aspx?Export=PDF" target="_blank" id="btnPrintInvoice8" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/Print.jpg" width="25" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                                  <a href="RV_TaxInvoice_Format8.aspx?Export=Word" target="_blank" id="btnExportInvoice8" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                                    <a href="RV_TaxInvoice_Format8.aspx?Export=View" target="_blank" id="btnViewInvoice8" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/view.jpg" width="28" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                               <a href="Email.aspx?Type=InvoiceFormat8" target="_blank" id="btnEmailInvoice8" runat="server"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                                  </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;"> <label style="font-weight:bold;color:black;width:100px;font-family:Calibri;font-size:15px">Simple Sales Invoice Format</label><br />


                             </td>
                 </tr>


            <tr style="padding-top:40px;">
                 <td colspan="1" style="text-align:left;padding-left:50PX;">
                                 <a href="RV_TaxInvoice_Format10.aspx?Export=PDF" target="_blank" id="btnPrintInvoice10" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/Print.jpg" width="25" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                                  <a href="RV_TaxInvoice_Format10.aspx?Export=Word" target="_blank" id="btnExportInvoice10" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                                <a href="RV_TaxInvoice_Format10.aspx?Export=View" target="_blank" id="btnViewInvoice10" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/view.jpg" width="28" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                               <a href="Email.aspx?Type=InvoiceFormat10" target="_blank" id="btnEmailInvoice10" runat="server"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;"> <label style="font-weight:bold;color:black;width:100px;font-family:Calibri;font-size:15px">Location Based Sales Invoice</label><br />

                             </td>
                 </tr>


            <tr style="padding-top:40px;">
                 <td colspan="1" style="text-align:left;padding-left:50PX;">
                                    <a href="RV_TaxInvoice_Format9.aspx?Export=PDF" target="_blank" id="btnPrintInvoice9" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/Print.jpg" width="25" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                                  <a href="RV_TaxInvoice_Format9.aspx?Export=Word" target="_blank" id="btnExportInvoice9" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                      <a href="RV_TaxInvoice_Format9.aspx?Export=View" target="_blank" id="btnViewInvoice9" runat="server"><imagebutton style="border:none;" type="button"><img src="Images/view.jpg" width="28" height="20" /></imagebutton></a>
                                  </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                               <a href="Email.aspx?Type=InvoiceFormat9" target="_blank" id="btnEmailInvoice9" runat="server"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                                   </td>
                 <td colspan="1" style="text-align:left;padding-left:2PX;">
                      <label style="font-weight:bold;color:black;width:100px;font-family:Calibri;font-size:15px">For PDF Export</label><br />

                                 
                                     <%-- <a href="RV_TaxInvoice_Format11.aspx?Export=PDF" target="_blank"><imagebutton style="border:none;" type="button"><img src="Images/Print.jpg" width="25" height="20" /></imagebutton></a>
                                 &nbsp;&nbsp;
                               <a href="RV_TaxInvoice_Format11.aspx?Export=Word" target="_blank"><imagebutton style="border:none;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                                &nbsp;&nbsp;  <a href="RV_TaxInvoice_Format11.aspx?Export=View" target="_blank"><imagebutton style="border:none;" type="button"><img src="Images/view.jpg" width="28" height="20" /></imagebutton></a>
                                 &nbsp;&nbsp; &nbsp;&nbsp;--%>&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                 &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                    
                   </td>
                 </tr>


            <tr style="padding-top:40px;">
                 <td colspan="5" style="text-align:left;padding-left:50PX;">
                              <a href="Email.aspx?Type=InvoiceFormat11" target="_blank" id="btnEmailInvoice11" runat="server"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                                 &nbsp;&nbsp; <label style="font-weight:bold;color:black;width:100px;font-family:Calibri;font-size:15px">Invoice with Service Report</label>




                       
                                     <br /><br />
                                 <%--<asp:Button ID="btnPrintReport" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px"/>--%>
                              
                         </tr>
                          </table>   </td>
                         </tr>
                          <tr>
                             <td>

                   <table style="border: 1px solid #CC3300; text-align:right; border-radius: 25px;padding: 5px; width:90%; height:90px; background-color: #F3F3F3;padding-left:15px;">
                         <tr><td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:2%;">SERVICE REPORT</td></tr>
                            <tr style="padding-top:20px;">
                             <td colspan="2" style="text-align:left;padding-left:50PX;">
                                <%--  <a href="RV_Export_ServiceRecordPrinting.aspx" target="_blank"><imagebutton style="border:none;background-color:white;" type="button"><img src="Images/word.jpg" width="20" height="20" /></imagebutton></a>
                             --%>        <a href="Email.aspx?Type=ServiceReportWithPhotos" target="_blank" id="A1" runat="server"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                           &nbsp;&nbsp;
                                   <a href="RV_Export_ServiceRecordPrinting.aspx?Type=PrintWithPhotosInvoice" target="_blank"><%-- <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/bullet.jpg" Width="12px" Height="12px"></asp:Image>--%>   <linkbutton style="font-weight:bold;color:black;width:100px;font-family:Calibri;font-size:15px" type="button">Service Reports with Photos</linkbutton></a>
                                 </td>
                                </tr>
                         <tr>
                             <td colspan="2" style="text-align:left;padding-left:50PX;">
                                        <a href="Email.aspx?Type=ServiceReportWithoutPhotos" target="_blank" id="A2" runat="server"><button style="border:none;background-color:white;" type="button"><img src="Images/email1.png" width="20" height="20" /></button></a>
                          &nbsp;&nbsp; 
                                   <a href="RV_Export_ServiceRecordPrinting1.aspx?Type=Invoice" target="_blank"><%-- <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/bullet.jpg" Width="12px" Height="12px"></asp:Image>--%>   <linkbutton style="font-weight:bold;color:black;width:100px;font-family:Calibri;font-size:15px" type="button">Service Reports without Photos</linkbutton></a>
                                 
                                 </td>
                                </tr>
                       <tr>
                           <td colspan="2" style="text-align:center;">
                                   <table style="border: 1px solid #CC3300; text-align:right; border-radius: 25px;padding: 5px; width:100%; height:150px; background-color: #F3F3F3;padding-left:15px;">
                         <tr><td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:2%;">Uploaded Service Reports
                             </td>
                             <td>
                                  <asp:ImageButton ID="btnImgDownloadAll" runat="server" Height="10px" Visible="False" Width="10px" ImageUrl="~/Images/downloadbutton.png" />
                             </td>
                         </tr> 
                                       <tr><td colspan="2"> <asp:Label ID="Label26" runat="server" ForeColor="BLACK" Visible="False"></asp:Label>
                                <asp:GridView ID="grdManualReports" runat="server" DataSourceID="SqlDSManualReports" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
         CellPadding="2" GridLines="None" Width="95%" AllowSorting="True"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns><asp:BoundField DataField="ServiceDate" HeaderText="ServiceDate" DataFormatString="{0:dd/MM/yyyy}" SortExpression="ServiceDate">
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                
                <asp:BoundField DataField="RecordNo" HeaderText="RecordNo">
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                  <asp:BoundField DataField="FileDescription" HeaderText="FileDescription" >
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                                                                     
                 <asp:BoundField DataField="FileName" HeaderText="FileName">
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
               <asp:HyperLinkField 
         DataTextField="Filenamelink"                 
         DataNavigateUrlFields="Filenamelink" 
         DataNavigateUrlFormatString="ViewManualReport.aspx?Filenamelink={0}" HeaderText="View" DataTextFormatString="View" target="_blank" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="White" ItemStyle-ForeColor="Black" />
                <%-- <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%# Eval("FileNameLink")%>' OnClick="DownloadFile" Text="Download"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
               <%-- <asp:HyperLinkField 
         DataTextField="Type"                 
         DataNavigateUrlFields="Type,VoucherNumber" 
         DataNavigateUrlFormatString="{0}.aspx?VoucherNumber={1}&CustomerFrom=Corporate" HeaderText="View" DataTextFormatString="View" target="_blank" ItemStyle-HorizontalAlign="Center" ItemStyle-BackColor="White" ItemStyle-ForeColor="Black" />
    --%>         <%--   <asp:HyperLinkField 
      DataTextField="ProductName" 
      HeaderText="Product Name" 
      SortExpression="ProductName" 
      DataNavigateUrlFields="ProductID" 
      DataNavigateUrlFormatString="ProductDetails.aspx?ProductID={0}" />--%>
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

        </asp:GridView>
                                   
                                           <asp:SqlDataSource ID="SqlDSManualReports" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">   
                                              <%-- <SelectParameters>
                          <asp:ControlParameter ControlID="txtInvoiceNo" Name="@AccountID" PropertyName="Text" />
                      </SelectParameters>--%>

                                           </asp:SqlDataSource>
                                      
                                        </td>
                         </tr>

        </table>
                           </td>
                       </tr>
                       </table>   </td>
                         </tr>

        </table>
           </asp:Panel>

            <asp:ModalPopupExtender ID="mdlPopupPrint" runat="server" CancelControlID="btnpnlPrintClose" PopupControlID="pnlPrint" TargetControlID="btndummyPrint" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>



            <asp:Panel ID="pnlStatus" runat="server" BackColor="White" Width="40%" Height="40%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Status Change</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageStatus" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertStatus" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                        
                         
                          <tr>
                               <td class="CellFormat">Change Status
                               </td>
                              <td class="CellTextBox" colspan="1"> <asp:DropDownList ID="ddlNewStatus" runat="server" Width="51%" Height="25px">
                                    <asp:ListItem Text="--SELECT--" Value="-1" />
                                  <asp:ListItem Value="O">O - Open</asp:ListItem>
                                   <asp:ListItem Value="V">V - Void</asp:ListItem> 
                                    
                               </asp:DropDownList></td>
                           </tr>
                           
                         <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnUpdateStatus" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Update Status" Width="120px" OnClientClick="currentdatetime()"/>
                                 <asp:Button ID="btnCancelStatus" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                        
        </table>
           </asp:Panel>

           <asp:ModalPopupExtender ID="mdlPopupStatus" runat="server" CancelControlID="btnCancelStatus" PopupControlID="pnlStatus" TargetControlID="btndummy1" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
   
         
          <asp:Button ID="btndummy1" runat="server" CssClass="dummybutton" />


         <%--start: Multi Print--%>


      <asp:Panel ID="pnlMultiPrint" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1"  ScrollBars="auto"  style="text-align:left; width:1300px; height:600px; margin-left:auto; margin-right:auto;"  Visible="true" >

                   <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: black;text-align:center;padding-left:10px; width:100%;">
                           
         <tr style="width:100%"><td  style="width:80%;font-size:18px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:10px;">&nbsp;Select Records to Print&nbsp; </td>
             
                 <td  style="width:20%;font-size:18px;font-weight:bold;font-family:Calibri;color:black;text-align:center;padding-left:0px;">
                           <asp:CheckBox ID="chkShowUnprinted" runat="server" Font-Size="12px" Text="Show Unprinted Invoices Only" AutoPostBack="True" />
                           </td></tr>


        </table>

     
        <div style="text-align: center; padding-left: 10px; padding-bottom: 5px;">
            <br />
            <div style="text-align:center; width:100%; margin-left:auto; margin-right:auto;" >
            
                                      <asp:Panel ID="Panel3" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1"  ScrollBars="auto"  style="text-align:left; width:1300px; height:300px; margin-left:auto; margin-right:auto;"  Visible="true" >

            <asp:GridView ID="grdViewMultiPrint" Width="100%" Font-Size="15px" runat="server"  AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SqlDSMultiPrint" ForeColor="#333333" GridLines="Vertical"> 
                                              <AlternatingRowStyle BackColor="White"/>
                                                <Columns>

                                                          <asp:TemplateField> 
               <HeaderTemplate><asp:CheckBox ID="chkSelectAllMultiPrintGV" runat="server" AutoPostBack="false" TextAlign="left" onchange="checkmultiprint()" Width="5%" ></asp:CheckBox></HeaderTemplate>    <HeaderStyle HorizontalAlign="Left" />
               <ItemTemplate><asp:CheckBox ID="chkSelectMultiPrintGV" runat="server" Font-Size="12px" Enabled="true" Height="18px"  Width="5%" AutoPostBack="false" CommandName="CHECK" ></asp:CheckBox></ItemTemplate>
                                                              <ItemStyle HorizontalAlign="Left" />
                                                          </asp:TemplateField>      
                                                          <asp:BoundField DataField="PrintCounter" HeaderText="Print Count" />
                                                      <asp:TemplateField HeaderText="Invoice Number" SortExpression="InvoiceNumber">
                                                              <EditItemTemplate>
                                                                  <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("SalesOrderNumber")%>'></asp:TextBox>
                                                              </EditItemTemplate>
                                                              <ItemTemplate>
                                                                  <asp:Label ID="lblInvNo" runat="server" Text='<%# Bind("SalesOrderNumber")%>'></asp:Label>
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
                                                  <asp:BoundField DataField="PostStatus" SortExpression="PostStatus" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="2%" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="PostStatus" HeaderText="Post St" SortExpression="PostStatus" >
                                                    <ItemStyle Width="2%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="PaidStatus" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="2%" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                        
                                                  <asp:BoundField DataField="SalesOrderDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Sales Order Date" SortExpression="SalesOrderDate" >
                                                    <ItemStyle Width="5%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GLPeriod" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CompanyGroup" HeaderText="Company Group" SortExpression="CompanyGroup" >
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AccountId" HeaderText="Account Id" SortExpression="AccountId">
                                                    <ControlStyle Width="6%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ContactType" HeaderText="Account Type" SortExpression="ContactType" />
                                                  <asp:BoundField DataField="CustName" HeaderText="Client Name" SortExpression="CustName">
                                                    <ControlStyle Width="20%" />
                                                  <ItemStyle HorizontalAlign="Left" Wrap="False" Width="20%" />
                                                  </asp:BoundField>
                                                 
                                                    <asp:BoundField DataField="ValueBase" HeaderText="Sales Amount" DataFormatString="{0:N2}">
                                                    <ControlStyle Width="8%" />
                                                    <ItemStyle HorizontalAlign="Right" Width="8%" Wrap="True" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AppliedBase" HeaderText="Net Inv. Amt." DataFormatString="{0:N2}">
                                                    <HeaderStyle Wrap="False" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Receiptbase" HeaderText="Receipt Amt." DataFormatString="{0:N2}" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CreditBase" HeaderText="Credit Amt." DataFormatString="{0:N2}" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BalanceBase" HeaderText="OS Amount" DataFormatString="{0:N2}" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                 
                                                  <asp:BoundField DataField="CustAddress1" HeaderText="Bill Address">
                                                    <ControlStyle Width="17%" />
                                                    <ItemStyle Width="17%" Wrap="False" HorizontalAlign="Left" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="BillStreet" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BillBuilding">
                                                    <ControlStyle Width="8%" CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle HorizontalAlign="Right" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CustAddPostal" HeaderText="Postal" SortExpression="CustAddPostal" />
                                                    <asp:BoundField DataField="StaffCode" HeaderText="Salesman" SortExpression="StaffCode" >
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" >
                                                          <ItemStyle Wrap="False" />
                                                          </asp:BoundField>
                                                  <asp:BoundField DataField="CreatedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="CreatedOn" SortExpression="CreatedOn" />
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="Edited By" SortExpression="LastModifiedBy" >
                                                          <ItemStyle Wrap="False" />
                                                          </asp:BoundField>
                                                  <asp:BoundField DataField="LastModifiedOn" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Edited On" SortExpression="LastModifiedOn" />
                                                    <asp:BoundField DataField="BillCountry" InsertVisible="False">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PoNo" InsertVisible="False">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="OurRef" InsertVisible="False">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="YourRef" InsertVisible="False">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Terms" InsertVisible="False">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DiscountAmount">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GSTBase">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BatchNo">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Comments">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AmountWithDiscount">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TermsDay">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RecurringSalesOrder" >
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BillSchedule">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
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
                                 <asp:Button ID="btnPrintMultiPrint" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Print" Width="100px" />
                                 <asp:Button ID="btnCancelMultiPrint" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>


        </table>
           
            <asp:SqlDataSource ID="SqlDSMultiPrint" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
            </asp:SqlDataSource>
      
              </div>
    </asp:Panel>

          <asp:ModalPopupExtender ID="mdlPopupMultiPrint" runat="server" CancelControlID="btnCancelMultiPrint" PopupControlID="pnlMultiPrint" TargetControlID="btnDummyMultiPrint" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
   
         
          <asp:Button ID="btnDummyMultiPrint" runat="server" CssClass="dummybutton" />

         <%--End: Multi print--%>



        <%-- Start--%>


     <asp:Panel ID="pnlQuickReceipt" runat="server" BackColor="White" Width="94%" Height="100%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
                <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">RECEIPT</h3>
    
                     <asp:UpdatePanel ID="updPnlMsgQR" runat="server" UpdateMode="Conditional">
              <ContentTemplate>
       <table style="width:100%;text-align:center;">
          
            <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageQR" runat="server"></asp:Label>
                      </td> 
            </tr>
                 <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertQR" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                       <asp:Label ID="Label15" runat="server" Text="" CssClass="dummybutton"></asp:Label>
</td>
                     
            </tr>
            </Table>

                   </ContentTemplate>
              </asp:UpdatePanel>
         <%--  start fields--%>

                 <asp:UpdatePanel runat="server" ID="updPnlQR" UpdateMode="Conditional">
                  <ContentTemplate>
                      

                     <%-- Start: Existing Receipt--%>

                      <table  style="width:100%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0% " >
           
               <tr style="width:100%">
                 <td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">EXISTING RECEIPTS
                 </td>
           </tr>
                            <tr style="text-align:center;">
                                  <td colspan="11" style="width:100%;text-align:center">
                                     <div style="text-align:center; width:100%; margin-left:auto; margin-right:auto;" >
     
                                      <asp:Panel ID="Panel6" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="100%" ScrollBars="Auto" style="overflow:scroll; margin-left:auto; margin-right:auto;" Wrap="False"    Visible="true" Width="1330px">

                                          <asp:GridView ID="GridView2" Width="100%" Font-Size="15px" runat="server"  OnRowDataBound = "OnRowDataBoundg2" OnSelectedIndexChanged = "OnSelectedIndexChangedg2" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="2" CellSpacing="2" DataKeyNames="Rcno" DataSourceID="SQLDSReceipt" ForeColor="#333333" GridLines="Vertical"> 
                                              <AlternatingRowStyle BackColor="White"/>
                                                <Columns>
                                                  <asp:CommandField ShowHeader="True" ShowSelectButton="True" SelectText="View">
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
                                                  <asp:BoundField DataField="PostStatus" HeaderText="Print St" SortExpression="PostStatus" >
                                                      <ControlStyle CssClass="dummybutton" />
                                                      <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="2%" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="PostStatus" HeaderText="Post St" SortExpression="PostStatus" >
                                                    <ItemStyle Width="2%" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="BankStatus" HeaderText="Paid St" SortExpression="BankStatus" >
                                                      <ControlStyle CssClass="dummybutton" />
                                                      <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="2%" CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="ReceiptNumber" HeaderText="Receipt Number" SortExpression="ReceiptNumber">
                                                    <ControlStyle Width="6%" />
                                                  <ItemStyle Wrap="False" Width="6%" />
                                                  </asp:BoundField>
                                                  <asp:BoundField DataField="ReceiptDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Receipt Date" SortExpression="ReceiptDate" >
                                                    <ItemStyle Width="5%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Cheque" HeaderText="Cheque No." SortExpression="Cheque">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AccountId" HeaderText="Account Id" SortExpression="AccountId">
                                                    <ControlStyle Width="6%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ContactType" HeaderText="Account Type" />
                                                  <asp:BoundField DataField="ReceiptFrom" HeaderText="Client Name" SortExpression="ReceiptFrom">
                                                    <ControlStyle Width="30%" />
                                                  <ItemStyle HorizontalAlign="Left" Wrap="False" Width="30%" />
                                                  </asp:BoundField>
                                                 
                                                  <asp:BoundField DataField="BaseAmount" HeaderText="Receipt Amount">
                                                    <ControlStyle Width="8%" />
                                                    <ItemStyle Width="8%" HorizontalAlign="Right" Wrap="True" />
                                                  </asp:BoundField>
                                                    <asp:BoundField DataField="BankId" HeaderText="Bank" SortExpression="BankId">
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PaymentType" HeaderText="Payment Mode" SortExpression="PaymentType" />
                                                    <asp:BoundField DataField="GLPeriod" HeaderText="Period" SortExpression="GLPeriod" />
                                                    <asp:BoundField DataField="CompanyGroup" HeaderText="Company Group" SortExpression="CompanyGroup" />
                                                    <asp:BoundField DataField="ChequeDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Cheque Date" SortExpression="ChequeDate">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LedgerCode">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LedgerName">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Comments">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" Wrap="False" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" >
                                                    <ItemStyle Wrap="False" />
                                                    </asp:BoundField>
                                                  <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" />
                                                  <asp:BoundField DataField="LastModifiedBy" HeaderText="Edited By" SortExpression="LastModifiedBy" />
                                                  <asp:BoundField DataField="LastModifiedOn" HeaderText="Edited On" SortExpression="Edited On" />
                                                    <asp:BoundField DataField="GSTAmount">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NetAmount">
                                                    <ControlStyle CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle CssClass="dummybutton" Wrap="False" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Salesman">
                                                    <ControlStyle Width="8%" CssClass="dummybutton" />
                                                    <HeaderStyle CssClass="dummybutton" />
                                                    <ItemStyle Width="8%" CssClass="dummybutton" Wrap="False" />
                                                    </asp:BoundField>
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
                                  </td>
                              </tr>
             
                 <tr>
                 <td>
                      <asp:SqlDataSource ID="SQLDSReceipt" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                </asp:SqlDataSource>
                     </td>
                  </tr>
                                 
       
             </table>

                      <%--End: Existing Receipts--%>

       
          <table border="0" style="width:90%; margin:auto; border:solid; border-color:ButtonFace;">
                           
          
               <tr style="width:100%">
                 <td colspan="4" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">NEW RECEIPTS INFORMATION
                 </td>
           </tr>
         
                    <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                   <asp:TextBox ID="txtGLFrom" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" CssClass="dummybutton" Height="16px" TabIndex="112" Width="20%"></asp:TextBox>
                                   <asp:TextBox ID="txtSalesmanQR" runat="server" AppendDataBoundItems="True" BorderStyle="None" Height="16px" Width="20%" Enabled="False" Visible="False"></asp:TextBox>
               
                       </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">   
                                Account ID 
                            </td>
                            
                       <td style="width:50%;font-size:14px; font-family:'Calibri';color:black;text-align:left;"> 
                               <asp:TextBox ID="txtAccountIdBillingQR" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" TabIndex="112" BackColor="#CCCCCC" ></asp:TextBox>
                              
                     
        
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                    
                        </tr>

               <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                         </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                                Account Name
                            </td>
                            
                          <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtAccountNameQR" runat="server" Height="16px" Width="80%" BackColor="#CCCCCC" AutoCompleteType="Disabled" TabIndex="113" ></asp:TextBox>
                              
                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                    
                        </tr>
              
               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Receipt No.<asp:Label ID="Label3" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                   </td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtReceiptNoQR" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" Width="80%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
               </tr>
              
               <tr>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                           <asp:TextBox ID="txtReceiptPeriodQR" runat="server" AutoCompleteType="Disabled" Height="16px" Width="40%" TabIndex="109" BorderStyle="None" Enabled="False" Visible="False"></asp:TextBox>
                 
                               </td>
                            <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">   
                                Receipt Date<asp:Label ID="Label4" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            
                          <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> 
                            <asp:TextBox ID="txtReceiptDateQR" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled" AutoPostBack="True" ></asp:TextBox>
                              <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtReceiptDateQR" TargetControlID="txtReceiptDateQR" Enabled="True" />

                            </td>
                       <td  style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">   
                         </td>
                     
                </tr>

   
              
               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">
                       &nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Amount</td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtReceivedAmountQR" runat="server" AutoCompleteType="Disabled" Height="16px" Width="80%" style="text-align:right" TabIndex="1"></asp:TextBox></td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
               </tr>
            
                   <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; "></td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Payment Mode<asp:Label ID="Label6" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:DropDownList ID="ddlPaymentModeQR" runat="server" AppendDataBoundItems="True" AutoPostBack="True" Height="20px" Width="81%" TabIndex="2">
                           <asp:ListItem>--SELECT--</asp:ListItem>
                       </asp:DropDownList>
                   </td>
               </tr>
                      <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">
                       <asp:TextBox ID="txtRecordNoQR" runat="server" BorderStyle="None" TabIndex="107" Width="1%" AutoCompleteType="Disabled" Height="16px"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Select Bank<asp:Label ID="Label7" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                   </td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:DropDownList ID="ddlBankCodeQR" runat="server" AppendDataBoundItems="True" AutoPostBack="True" Height="20px" Width="81%" TabIndex="3">
                           <asp:ListItem>--SELECT--</asp:ListItem>
                       </asp:DropDownList>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                   </td>
               </tr>
               <tr style="display:none">
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">
                        <asp:TextBox ID="txtBankGLCodeQR" runat="server" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" TabIndex="24" Width="20%" ForeColor="White"></asp:TextBox>
             </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left; ">
                       <asp:TextBox ID="txtBankIDQR" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Height="16px" TabIndex="24" Width="81%"></asp:TextBox>
                   </td>
                   <td rowspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:15%; text-align:right; ">
                       &nbsp;</td>
               </tr>
                   <tr style="display:none">
                    <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">
                        <asp:TextBox ID="txtLedgerNameQR" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="23" Width="20%" Enabled="False" Visible="False" BorderStyle="None"></asp:TextBox>
                     
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtBankNameQR" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="4" Width="80%"></asp:TextBox>
                   </td>
                       </tr>
             
               
               
               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">
                         <asp:TextBox ID="txtRecvPrefixQR" runat="server" Visible="false" AutoCompleteType="Disabled" BorderStyle="None" Height="16px" TabIndex="24" Width="20%" Enabled="False"></asp:TextBox>
                
                          </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">Cheque No./Ref. No. </td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                       <asp:TextBox ID="txtChequeNoQR" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="5" Width="80%"></asp:TextBox>
                   </td>
               </tr>
               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; display:none ">&nbsp;</td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;  display:none; ">
                       <asp:TextBox ID="txtChequeDateQR" runat="server" AutoCompleteType="Disabled" Height="16px" TabIndex="6" Width="80%"></asp:TextBox>
                   
                         </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; "></td>
               </tr>
               <tr>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; ">&nbsp;</td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:10%; text-align:right; vertical-align:top">Remarks</td>
                   <td style="width:20%;font-size:14px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                       <asp:TextBox ID="txtCommentsQR" runat="server" AutoCompleteType="Disabled" Font-Names="Calibri" Font-Size="15px"  TabIndex="7" TextMode="SingleLine" Width="80%"></asp:TextBox>
                   </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">&nbsp;</td>
               </tr>
              
               <tr>
                   <td colspan="3" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                       <asp:Button ID="btnShowInvoicesQR" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  TabIndex="28" Text="IMPORT INVOICE" Width="25%" />
                 
                       
          
                         </td>
                   <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; color:black; padding-left:1%; width:20%; text-align:right; ">
                       <asp:TextBox ID="txtTaxRatePctQR" runat="server" AutoCompleteType="Disabled" BorderStyle="None" ForeColor="White" Height="16px" Width="35%"></asp:TextBox>
                   </td>





               </tr>

                  

              </table>

         


                      
                         </ContentTemplate>
                   </asp:UpdatePanel>



                            <%--grid--%>

                       <table border="1" style="width:90%; margin:auto">

               
               <tr style="width:100%; padding-left:10%;">
                 <td colspan="5" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:10%; color:#800000; background-color: #C0C0C0;">
                     RECEIPTS DETAILS

                 </td>

           </tr>


                  <tr style="width:95%; padding-left:10%;">
                     <td colspan="10" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:90% ">
               <asp:UpdatePanel ID="updpnlBillingDetailsQR" runat="server" UpdateMode="Conditional"><ContentTemplate>
             <asp:GridView ID="grvBillingDetailsQR"  runat="server" AllowSorting="True" 
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="12px"   
            ShowFooter="True" Style="text-align: left;  " Width="70%"><Columns>
              <asp:TemplateField HeaderText=""><ItemTemplate><asp:CheckBox ID="chkSelectGVQR" runat="server" Enabled="true" Height="15px"  Width="20px" Visible="false"  CommandName="CHECK" AutoPostBack="false"  ></asp:CheckBox></ItemTemplate></asp:TemplateField>                        
             <asp:TemplateField HeaderText=" Item Type">

                           <ItemTemplate><asp:DropDownList ID="txtItemTypeGVQR" runat="server" Font-Size="11px" Height="20px" ReadOnly="true" Width="70px"  AppendDataBoundItems="True" AutoPostBack="True"  onselectedindexchanged="txtItemTypeGVQR_SelectedIndexChanged"><asp:ListItem Text="--SELECT--" Value="-1" /><asp:ListItem Text="ARIN" Value="ARIN" />
                               <asp:ListItem Text="OTHERS" Value="OTHERS" /></asp:DropDownList></ItemTemplate>
            </asp:TemplateField>
                   <asp:TemplateField HeaderText=" Invoice No."><ItemTemplate><asp:TextBox ID="txtInvoiceNoGVQR" runat="server" Enabled="false" Height="15px"  Font-Size="12px" ReadOnly="true" Width="120px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText=" Invoice Date"><ItemTemplate><asp:TextBox ID="txtInvoiceDateGVQR" runat="server" Enabled="false" Height="15px"  Font-Size="12px" ReadOnly="true" Width="70px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="GL Code"><ItemTemplate><asp:TextBox ID="txtOtherCodeGVQR" runat="server" Visible="True" Enabled="false" Height="15px"   Font-Size="12px" Width="60px"></asp:TextBox></ItemTemplate></asp:TemplateField>                     
                
                   <asp:TemplateField HeaderText=""><ItemTemplate><asp:ImageButton ID="BtnGLQR" runat="server" OnClick="BtnGLQR_Click" Visible="true" CssClass="righttextbox" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                        
                    </ItemTemplate></asp:TemplateField>
             <asp:TemplateField HeaderText="GL Desc."><ItemTemplate><asp:TextBox ID="txtGLDescriptionGVQR" runat="server" Visible="True" Enabled="false" Height="15px"   Font-Size="12px" Width="160px"></asp:TextBox></ItemTemplate></asp:TemplateField>
             <asp:TemplateField HeaderText="Description"><ItemTemplate><asp:TextBox ID="txtRemarksGVQR" runat="server" Visible="True" Enabled="true" Height="15px"  Font-Size="12px"  Width="350px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
          
              <asp:TemplateField HeaderText="Invoice Amount" HeaderStyle-HorizontalAlign="Right"><ItemTemplate><asp:TextBox ID="txtTotalPriceWithGSTGVQR" runat="server" Enabled="false" style="text-align:right" Height="15px"   Font-Size="12px" Width="72px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="OS Amount" HeaderStyle-HorizontalAlign="Right"><ItemTemplate><asp:TextBox ID="txtTotalReceiptAmtGVQR" runat="server" Enabled="false" style="text-align:right" Height="15px"   Font-Size="12px" Width="72px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Total CN Amount" HeaderStyle-HorizontalAlign="Right"><ItemTemplate><asp:TextBox ID="txtTotalCreditNoteAmtGVQR" runat="server" Enabled="false" style="text-align:right" Height="15px"  Font-Size="12px" Width="70px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="Receipt Amount" HeaderStyle-HorizontalAlign="Right"><ItemTemplate><asp:TextBox ID="txtReceiptAmtGVQR" runat="server" Enabled="true" style="text-align:right" Height="15px"  Font-Size="12px" Width="80px" Align="right" AutoPostBack="true" OnTextChanged="txtReceiptAmtGVQR_TextChanged"></asp:TextBox></ItemTemplate></asp:TemplateField>
             
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtLocationGVQR" runat="server" Visible="false" Height="15px"  Font-Size="12px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
            
              <asp:CommandField ButtonType="Image"  DeleteImageUrl="~/Images/delete_icon_color.gif" ShowDeleteButton="True" Visible="false">
                   <FooterStyle VerticalAlign="Top" /><ItemStyle Height="15px" />

                 </asp:CommandField>
             <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtContractNoGVQR" runat="server" Visible="false" Enabled="false" Height="15px"  Font-Size="12px" ReadOnly="true" Width="0px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtServiceNoGVQR" runat="server" Visible="false" Enabled="false" Height="15px"  Font-Size="12px" ReadOnly="true" Width="0px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtInvoiceTypeGVQR" runat="server" Visible="false" Enabled="false" Height="15px"  Font-Size="12px" ReadOnly="true" Width="0px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                          
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtItemCodeGVQR" runat="server" Visible="false" Height="15px"  Font-Size="12px" Width="0px" AppendDataBoundItems="True" AutoPostBack="False" > </asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtItemDescriptionGVQR" runat="server" Visible="false" Height="15px"  Font-Size="12px"  Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:DropDownList ID="txtUOMGVQR" runat="server" Visible="false" Height="20px"   Font-Size="12px" Width="0px" AppendDataBoundItems="True"> <asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtQtyGVQR" runat="server" Visible="false" style="text-align:right" Height="15px"  Width="0px" AutoPostBack="true" ></asp:TextBox></ItemTemplate></asp:TemplateField>
             
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtTaxTypeGVQR" runat="server" Visible="false" style="text-align:right" Height="20px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtGSTPercGVQR" runat="server" Visible="false" Enabled="false" style="text-align:right" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                          
               <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoInvoiceGVQR" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtRcnoReceiptGVQR" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>                        
               <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtARCodeGVQR" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
               <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtGSTCodeGVQR" runat="server" Visible="false" Height="15px" Width="0px"></asp:TextBox></ItemTemplate></asp:TemplateField>
              
                  <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Right"><ItemTemplate><asp:TextBox ID="txtPriceWithDiscGVQR" runat="server" Text="0.00" Visible="false" Enabled="false" style="text-align:right" Height="15px"  Font-Size="12px" Width="0px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
              <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Right"><ItemTemplate><asp:TextBox ID="txtGSTAmtGVQR" runat="server" Visible="false" Enabled="false" style="text-align:right" Height="15px"  Font-Size="12px" Width="0px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
                            
                <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtAccoountIdGVQR" runat="server" Visible="false" Enabled="false" Height="15px"  Font-Size="12px" ReadOnly="true" Width="0px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtCustomerNameGVQR" runat="server" Visible="false" Enabled="false" Height="15px"  Font-Size="12px" ReadOnly="true" Width="0px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtLocatioIDGVQR" runat="server" Visible="false" Enabled="false" Height="15px"  Font-Size="12px" ReadOnly="true" Width="0px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtLocationGroupGVQR" runat="server" Visible="false" Enabled="false" Height="15px"  Font-Size="12px" ReadOnly="true" Width="0px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtRcnoInvoiceDetailGVQR" runat="server" Visible="false" Enabled="false" Height="15px"  Font-Size="12px" ReadOnly="true" Width="0px"   AutoPostBack="false" ></asp:TextBox></ItemTemplate></asp:TemplateField>
                        
                     <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtDocTypeGVQR" runat="server" Enabled="true" Visible="false" style="text-align:right" Height="15px"  Font-Size="12px" Width="10px" Align="right"></asp:TextBox></ItemTemplate></asp:TemplateField>
                  <asp:TemplateField><ItemTemplate><asp:TextBox ID="txtSourceRcnoGVQR" runat="server" Visible="false" Height="15px" Width="10px"></asp:TextBox></ItemTemplate></asp:TemplateField>
          
                            
                <asp:TemplateField>
                <FooterStyle HorizontalAlign="Left" />
                <FooterTemplate><asp:Button ID="btnAddDetailQR" runat="server" OnClick="btnAddDetailQR_Click" Text="Add New Row" ValidationGroup="VGroup" Visible="false" />

                  </FooterTemplate><ItemStyle ForeColor="#507CD1" /></asp:TemplateField>
                          </Columns>
             <FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" />
                <RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" />
             </asp:GridView>
                   
                   </ContentTemplate>
                   </asp:UpdatePanel>
            </td></tr>

                            </table>    


                     <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional"><ContentTemplate>
                       <table border="0" style="width:90%; margin-left:3%;  border:solid;  " >
                         <tr style="width:100%; padding-left:5%;">
              
                               <td colspan="1"  style="width:20px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right; ">
                                  
                                 </td>
                               <td colspan="1" style="width:120px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                   </td>
                              <td colspan="1" style="width:82px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               
                               </td>
                               <td colspan="1" style="width:160px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                                 </td>
                              <td colspan="1" style="width:125px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                            
                                 </td>
                              <td colspan="1" style="width:100px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    &nbsp;</td>

                               <td colspan="1" style="width:75px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                                   &nbsp;</td>

                              <td colspan="1" style="width:80px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                             
                                  &nbsp;</td>
                              <td colspan="1" style="width:100px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                    &nbsp;</td>

                              <td colspan="1" style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                  &nbsp;</td>

                              <td colspan="1" style="width:110px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                  Total</td>


                              <td colspan="1" style="width:95px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                  <asp:TextBox ID="txtReceiptAmt" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" BorderStyle="None" Font-Bold="true" Height="18px" style="text-align: right" Width="100%"></asp:TextBox>
                               </td>

                      
                      
                        </tr>
                        
                           
                         
                          
                         
                           <tr style="width:100%; padding-left:2%;">
                               <td colspan="1" style="width:20px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right; padding-left:1%;"></td>
                               <td colspan="1" style="width:120px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"></td>
                               <td colspan="1" style="width:82px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               </td>
                               <td colspan="1" style="width:160px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"></td>
                               <td colspan="1" style="width:125px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"></td>
                               <td colspan="1" style="width:110px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"></td>
                               <td colspan="1" style="width:75px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                               </td>
                               <td colspan="1" style="width:60px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">&nbsp;</td>
                               <td colspan="1" style="width:100px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;</td>
                               <td colspan="1" style="width:90px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;"></td>
                               <td colspan="1" style="width:110px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                   <asp:Button ID="btnSaveQR" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick=" return DoValidationSaveQR();" Text="SAVE" Width="95%" TabIndex="8" />
                               </td>
                               <td colspan="1" style="width:95px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">
                                   <asp:Button ID="btnCancelQR" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="CANCEL" Width="95%" />
                               </td>
                           </tr>
                        
            </table>
                      </ContentTemplate></asp:UpdatePanel>
                     <%-- grid--%>

           <%--end fields--%>
  
       
       
    </div>
                     
                  
           </asp:Panel>
                 <asp:ModalPopupExtender ID="mdlQuickReceipt" runat="server" CancelControlID="btnCancelNotesMaster" PopupControlID="pnlQuickReceipt" TargetControlID="btndummyNotesMaster" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
                  <asp:Button ID="btndummyNotesMaster" runat="server" cssclass="dummybutton" />

        <%-- end--%>


        <%-- start--%>

         <div>
    <asp:ModalPopupExtender ID="mdlImportInvoices" runat="server" BackgroundCssClass="modalBackground" CancelControlID="ImageButton5" PopupControlID="pnlImportInvoices" TargetControlID="btnDummyImportInvoices">
            </asp:ModalPopupExtender>    
  
   <asp:Button ID="btnDummyImportInvoices" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" /> 
      
              <%--  start--%>


        <asp:Panel ID="pnlImportInvoices" runat="server" BackColor="White" Width="95%" Height="600px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">
   
             <asp:UpdatePanel runat="server" ID="updpnlImportInovice" UpdateMode="Conditional">
                  <ContentTemplate>                  
        <table border="0"  style="width:100%;text-align:center; padding-top:2px; margin-left: 0%; margin-right:0%; border:solid; border-color:ButtonFace; "  >

                 <tr style="width:100%">
                 <td colspan="6" style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:5%; color:#800000; background-color: #C0C0C0;"> SEARCH INVOICE RECORDS 
                     
                     </td>
                  </tr>
             
                     <tr>
                          <td colspan="6"   style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="Label5" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td>
                         
                          
                                        
                     
            
                     <tr>
                            <td style="width:10%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"></td>

                          <td style="width:8%; font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;"> &nbsp;</td>

                          <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;" colspan="4"> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                          
                        
                               
                            </td>

                            <tr>
                                <td style="width:6%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">&nbsp;</td>
                                <td style="width:9%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Account Type </td>
                                <td style="width:6%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Account Id </td>
                                <td colspan="2" style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Client Name </td>
                                <td style="width:10%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:DropDownList ID="ddlCompanyGrpII" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="companygroup" DataValueField="companygroup" Height="25px" Value="-1" Width="90%" Visible="False">
                                        <asp:ListItem Text="--SELECT--" Value="-1" />
                                    </asp:DropDownList>
                                </td>
                                <td style="width:9%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:DropDownList ID="ddlContactTypeII" runat="server" DataTextField="ContType" DataValueField="ContType" Height="20px" Width="95%">
                                        <asp:ListItem Selected="True" Value="COMPANY">CORPORATE</asp:ListItem>
                                        <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtAccountIdII" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Height="16px" Width="80%"></asp:TextBox>
                                 
                                </td>
                                <td colspan="2" style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtClientNameII" runat="server" AutoCompleteType="Disabled" Height="16px" Width="95%"></asp:TextBox>
                                </td>

                                     <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibridummy';color:black;text-align:left;">
                                    <asp:Button ID="btnSearchII" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="SEARCH" Visible="True" Width="80%" />
                                </td>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibridummy';color:black;text-align:left;">
                                    <asp:Button ID="btnShowRecordsII" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="return DoValidation()" Text="SHOW RECORDS" Visible="False" Width="80%" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    &nbsp;</td>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtInvoiceNoII" runat="server" AutoCompleteType="Disabled" Height="16px" Width="95%" Visible="False"></asp:TextBox>
                                </td>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtDateFromII" runat="server" AutoCompleteType="Disabled" Height="16px" Width="90%" Visible="False"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender7" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtDateFrom" TargetControlID="txtDateFromII" />
                                </td>
                                <td style="width:10%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <asp:TextBox ID="txtDateToII" runat="server" AutoCompleteType="Disabled" Height="16px" Width="90%" Visible="False"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender8" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupButtonID="txtDateTo" TargetControlID="txtDateToII" />
                                </td>
                                <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    &nbsp;</td>
                                <td style="width:6%;font-size:12px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    &nbsp;</td>
                            </tr>
                            </tr>

                        </table>
       </ContentTemplate>
              </asp:UpdatePanel>
            
        

             <asp:Panel ID="Panel4" runat="server" ScrollBars="Auto" style="overflow:scroll" Wrap="false"  BackColor="White" Width="99%" Height="350px" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center">
       
         
         <table border="1" style="width:100%; margin:auto; border:solid; border-color:ButtonFace;">
             
               <tr style="width:100%">
                 <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; text-decoration: underline;padding-left:5%; color:#800000; background-color: #C0C0C0;">
                     <asp:Label ID="Label9" runat="server" Text="INVOICE DETAILS"></asp:Label>
            </td>

               </tr>
               
             <tr style="width:100%">
                     <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left;  width:100% ">
        
        
        <asp:UpdatePanel ID="updpnlInvoiceRecs" runat="server" UpdateMode="Conditional"><ContentTemplate>
            <asp:GridView ID="grvInvoiceRecDetails" runat="server" AllowSorting="True"  
             AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " 
             GridLines="None" Height="12px"  Font-Size="14px"
            ShowFooter="True" Style="text-align: left" Width="100%">
                
                <Columns> 
                              
                <asp:TemplateField> <HeaderTemplate><asp:CheckBox ID="chkSelectInvoiceGV" runat="server" AutoPostBack="false" TextAlign="Right" onchange="checkallinvoicerecs()"  Width="5%" ></asp:CheckBox></HeaderTemplate>
                <ItemTemplate><asp:CheckBox ID="chkSelectGVII" runat="server" Font-Size="12px" Enabled="true" Height="18px"  Width="5%" AutoPostBack="false" onchange="checkoneinvoicerec()"   ></asp:CheckBox></ItemTemplate></asp:TemplateField>            
              
                    
                       <asp:TemplateField HeaderText="Invoice Number" ><ItemTemplate><asp:TextBox ID="txtInvoiceNumberGVII" runat="server" Text='<%# Bind("InvoiceNumber")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="125px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Inv. Date"><ItemTemplate><asp:TextBox ID="txtSalesDateGVII" runat="server" Text='<%# Bind("SalesDate")%>'  DataFormatString="{0:dd/MM/yyyy}"  Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="65px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Inv. Amount" ><ItemTemplate><asp:TextBox ID="txtAppliedBaseGVII" runat="server" Text='<%# Bind("AppliedBase")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="75px" style="text-align:right" ></asp:TextBox></ItemTemplate>
                  
                    </asp:TemplateField>
            
                <asp:TemplateField HeaderText="Rcpt. Amt." ><ItemTemplate><asp:TextBox ID="txtTotalReceiptAmountGVII" runat="server" Text='<%# Bind("ReceiptBase")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="75px" style="text-align:right"></asp:TextBox></ItemTemplate>
                   
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="CN Amt."><ItemTemplate><asp:TextBox ID="txtTotalCNAmountGVII" runat="server" Text='<%# Bind("Creditbase")%>'  Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="75px" style="text-align:right" ></asp:TextBox></ItemTemplate>
               </asp:TemplateField>
                          <asp:TemplateField HeaderText="OS Amt."><ItemTemplate><asp:TextBox ID="txtOSAmountGVII" runat="server" Text='<%# Bind("BalanceBase")%>'  Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="75px" style="text-align:right" ></asp:TextBox></ItemTemplate>
                  
                    </asp:TemplateField>
            
                      <asp:TemplateField HeaderText="Company"><ItemTemplate><asp:TextBox ID="txtCompanyGroupGVII" runat="server" Text='<%# Bind("CompanyGroup")%>' Visible="true" Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="65px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Acct. Id" ><ItemTemplate><asp:TextBox ID="txtAccountIdGVII" runat="server" Text='<%# Bind("AccountId")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px" Width="65px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                <asp:TemplateField HeaderText="Client Name"><ItemTemplate><asp:TextBox ID="txtClientNameGVII" runat="server" Text='<%# Bind("CustName")%>' Font-Size="12px" ReadOnly="true" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="535px"></asp:TextBox></ItemTemplate></asp:TemplateField>      
             
            <asp:TemplateField HeaderText=""><ItemTemplate><asp:TextBox ID="txtLocationIDGVII" runat="server"  Font-Size="11px" ReadOnly="true" Visible="false" BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="85px"></asp:TextBox></ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate><asp:TextBox ID="txtDocTypeGVII" runat="server"  Font-Size="11px" ReadOnly="true" Visible="True" Text='<%# Bind("DocType")%>'  BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="5px"></asp:TextBox></ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate><asp:TextBox ID="txtRcnoGVII" runat="server"  Font-Size="11px" ReadOnly="true" Visible="True" Text='<%# Bind("Rcno")%>'  BackColor="#CCCCCC" BorderStyle="None" Height="18px"  Width="5px"></asp:TextBox></ItemTemplate></asp:TemplateField>
           
               </Columns>

        <FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" /><RowStyle BackColor="#EFF3FB" Height="17px" /><EditRowStyle BackColor="#2461BF" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" /><AlternatingRowStyle BackColor="White" /></asp:GridView>
            </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="grvInvoiceRecDetails" EventName="SelectedIndexChanged" /></Triggers></asp:UpdatePanel>&nbsp;</td></tr>
             

             
              <tr>
                  <td  style="width:80%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"></asp:UpdatePanel>
                       </td>
                  
            </tr>

               
            
        </table>


                 </asp:Panel>


             <table border="0" style="width:99%; margin:auto; border:solid; border-color:ButtonFace;">
                     <tr>
                        <td  style="width:30%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
             
                       </td>
                         
                    <td  style="width:30%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:right">
                            Total
                       </td>

                   <td  style="width:30%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left">
                   <asp:TextBox ID="txtTotalInvoiceSelected" runat="server" style="text-align:right" AutoCompleteType="Disabled"  ForeColor="Black" Height="16px" Width="35%" BackColor="#CCCCCC"></asp:TextBox>
                             
                       </td>
                 
            </tr>
                    </table>

                <table border="0" style="width:99%; margin:auto; border:solid; border-color:ButtonFace;">
                     <tr>
                    
                         
                  <td  style="width:80%;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:center">
                 
                            <asp:SqlDataSource ID="SqlDSOSInvoice" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ddlCompanyGrpII" Name="@CompanyGroup" PropertyName="SelectedValue" />
                                    <asp:ControlParameter ControlID="txtAccountIdII" Name="@AccountID" PropertyName="Text" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                       <asp:Button ID="btnImportInvoiceII" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="IMPORT INVOICES" Width="15%" Visible="True" />
                       &nbsp;  
                           <asp:Button ID="Button4" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True"  Text="CANCEL" Width="15%" Visible="True" />
                     
                   
                        </td>
    
                         
                 
            </tr>
                    </table>
         </asp:Panel>

                     
          <%--end--%>



           <%--  popup--%>


                 <asp:Panel ID="pnlConfirmPost" runat="server" BackColor="White" Width="400px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="lblEvent" runat="server" Text="Confirm POST"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="lblQuery" runat="server" Text="Are you sure to POST the Sales Order?"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmYes" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="if(this.value === 'Posting...') { return false; } else { this.value = 'Posting...'; } ;"/>
                                 <asp:Button ID="btnConfirmNo" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupConfirmPost" runat="server" CancelControlID="btnConfirmNo" PopupControlID="pnlConfirmPost" TargetControlID="btndummyPost" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyPost" runat="server" CssClass="dummybutton" />
  
         <%--start--%>

            <asp:Panel ID="pnlConfirmReverse" runat="server" BackColor="White" Width="400px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label8" runat="server" Text="Confirm REVERSE"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label10" runat="server" Text="Are you sure to REVERSE the Sales Order?"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmYesReverse" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="if(this.value === 'Reversing...') { return false; } else { this.value = 'Reversing...'; } ;"/>
                                 <asp:Button ID="btnConfirmNoReverse" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupConfirmReverse" runat="server" CancelControlID="btnConfirmNoReverse" PopupControlID="pnlConfirmReverse" TargetControlID="btndummyReverse" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyReverse" runat="server" CssClass="dummybutton" />
         <%--end--%>


              <%--start--%>

            <asp:Panel ID="pnlConfirmSavePost" runat="server" BackColor="White" Width="400px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label11" runat="server" Text="Confirm POST/UPDATE "></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label12" runat="server" Text="Do you want to POST/UPDATE the Sales Order?"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmYesSavePost" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px" OnClientClick="if(this.value === 'Posting...') { return false; } else { this.value = 'Posting...'; } ;"/>
                                 <asp:Button ID="btnConfirmNoSavePost" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupConfirmSavePost" runat="server" TargetControlID="btndummySavePost" CancelControlID="btnConfirmNoSavePost" PopupControlID="pnlConfirmSavePost"  BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummySavePost" runat="server" CssClass="dummybutton" />
         <%--end--%>

            <%-- update ServiceRecord--%>

                     <asp:Panel ID="pnlUpdateServiceRecord" runat="server" BackColor="White" Width="400px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label14" runat="server" Text="Update Service Record "></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label16" runat="server" Text="Do you wish to change the Bill Amount of the Service Record?"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmYesUpdateServiceRecord" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                                 <asp:Button ID="btnConfirmNoUpdateServiceRecord" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlUpdateServiceRecord" runat="server" TargetControlID="btnUpdateServiceRecord"   PopupControlID="pnlUpdateServiceRecord" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnUpdateServiceRecord" runat="server" CssClass="dummybutton" />
         <%--end--%>


             ''''''''''''

         <%-- start: Contract T/E --%>

                     <asp:Panel ID="pnlContractTE" runat="server" BackColor="White" Width="400px" Height="140px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label28" runat="server" Text="Not Active Contract "></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label29" runat="server" Text="The contract that you've selected is NOT ACTIVE."></asp:Label>
                        
                      </td>
                           </tr>
              <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label30" runat="server" Text="Do you still wish to bill?"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmYesContractTE" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                                 <asp:Button ID="btnConfirmNoContractTE" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlContractTE" runat="server" TargetControlID="btnContractTE"   PopupControlID="pnlContractTE" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnContractTE" runat="server" CssClass="dummybutton" />

        <%--end: Contract T/E --%>


                     <%-- update GST Code--%>

      <asp:Panel ID="pnlUpdateGSTCode" runat="server" BackColor="White" Width="400px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label24" runat="server" Text="Change Tax Code "></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label25" runat="server" Text="Do you want to CHANGE the Tax Code for this Invoice?"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmYesUpdateGSTCode" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Yes" Width="100px"/>
                                 <asp:Button ID="btnConfirmNoUpdateGSTCode" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlUpdateGSTCode" runat="server" TargetControlID="btnUpdateGSTCode"   PopupControlID="pnlUpdateGSTCode" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnUpdateGSTCode" runat="server" CssClass="dummybutton" />
         <%--end--%>

             ''''''''''

      <asp:Panel ID="pnlLockedServiceRecord" runat="server" BackColor="White" Width="400px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
              <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label17" runat="server" Text="Service Record Locked"></asp:Label>
                        
                      </td>
                           </tr>
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label18" runat="server" Text="Service Record is Locked.. Cannot modify Bill Amount"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                         <td colspan="2" style="text-align:center">         <asp:Button ID="btnCancelLockedServiceRecord" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px" />
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlLockedServiceRecord" runat="server" TargetControlID="btnLockedServiceRecord"  CancelControlID="btnCancelLockedServiceRecord" PopupControlID="pnlLockedServiceRecord" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btnLockedServiceRecord" runat="server" CssClass="dummybutton" />



                      <%--popup1--%>


               <asp:Panel ID="pnlEditPONo" runat="server" BackColor="White" Width="40%" Height="55%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2" style="text-align:center;"    >
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit PO No.</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessagePONO" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertPONO" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                          <tr>
                      <td class="CellFormat" style="text-align:right;" >Our Reference</td>
                    <td class="CellTextBox">  
                        <asp:TextBox ID="txtOurRefEdit" runat="server" Height="16px" MaxLength="25" Width="50%"></asp:TextBox>
                          
              
                      </td>
                                         
                  </tr>

             
   
                        
                         <tr>
                             <td class="CellFormat" style="text-align: right;">Your Reference</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtYourRefEdit" runat="server" Height="16px" MaxLength="25" Width="50%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat" style="text-align: right;">PO No.</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtPONOEdit" runat="server" Height="16px" MaxLength="100" Width="50%"></asp:TextBox>
                             </td>
                         </tr>

             
   
                        
                         <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnEditPONoSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime();"/>
                                 <asp:Button ID="btnEditPONoCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupEditPONo" runat="server" CancelControlID="btnEditPONoCancel" PopupControlID="pnlEditPONo" TargetControlID="btndummyEditPOno" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
              <asp:Button ID="btndummyEditPOno" runat="server" cssclass="dummybutton" />
          
             
               <%-- popup1--%>

                   <asp:Panel ID="pnlEditRemarks" runat="server" BackColor="White" Width="40%" Height="60%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2" style="text-align:center;"    >
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Remarks</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageRemarks" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertRemarks" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>

                                <tr>
                      <td class="CellFormat" style="text-align:right;" >Description</td>
                    <td class="CellTextBox">  
                    <asp:TextBox ID="txtDescriptionEdit" runat="server" AutoCompleteType="Disabled" Height="100px" Width="79.5%" TextMode="MultiLine" TabIndex="30" Font-Names="Calibri" Font-Size="15px"></asp:TextBox>      
              
                      </td>
                                         
                  </tr>
                          <tr>
                      <td class="CellFormat" style="text-align:right;" >Remarks</td>
                    <td class="CellTextBox">  
                    <asp:TextBox ID="txtRemarksEdit" runat="server" AutoCompleteType="Disabled" Height="100px" Width="79.5%" TextMode="MultiLine" TabIndex="30" Font-Names="Calibri" Font-Size="15px"></asp:TextBox>      
              
                      </td>
                                         
                  </tr>
                                  
                         
                         <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:10px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnEditRemarksSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime();"/>
                                 <asp:Button ID="btnEditRemarksCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                            
        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupEditRemarks" runat="server" CancelControlID="btnEditRemarksCancel" PopupControlID="pnlEditRemarks" TargetControlID="btndummyEditRemarks" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
              <asp:Button ID="btndummyEditRemarks" runat="server" cssclass="dummybutton" />


                  <%-- start--%>

                <asp:Panel ID="pnlEditBillingName" runat="server" BackColor="White" Width="40%" Height="55%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2" style="text-align:center;"    >
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Billing Name</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageBillingName" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertBillingName" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                          <tr>
                      <td class="CellFormat" style="text-align:right;" >Billing Name </td>
                    <td class="CellTextBox">  
                        <asp:TextBox ID="txtBillingNameEdit" runat="server" Height="16px" MaxLength="200" Width="70%"></asp:TextBox>
                          
              
                      </td>
                                         
                  </tr>

             
                        
                         <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnEditBillingNameSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime();"/>
                                 <asp:Button ID="btnEditBillingNameCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupEditBillingName" runat="server" CancelControlID="btnEditBillingNameCancel" PopupControlID="pnlEditBillingName" TargetControlID="btndummyEditBillingName" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
              <asp:Button ID="btndummyEditBillingName" runat="server" cssclass="dummybutton" />
            <%-- popup1--%>

            <%-- start--%>

                <asp:Panel ID="pnlEditBillingDetails" runat="server" BackColor="White" Width="40%" Height="70%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2" style="text-align:center;"    >
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Billing Details</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessageBillingDetails" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertBillingDetails" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                          <tr>
                      <td class="CellFormat" style="text-align:right;" >Contact Person</td>
                    <td class="CellTextBox">  
                        <asp:TextBox ID="txtContactPersonEdit" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                          
              
                      </td>
                                         
                  </tr>

             
   
                        
                         <tr>
                             <td class="CellFormat" style="text-align:right;">Address<asp:Label ID="Label63" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                             </td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtBillAddressEdit" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat" style="text-align:right;">&nbsp;</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtBillStreetEdit" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat" style="text-align:right;">&nbsp;</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtBillBuildingEdit" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                             </td>
                         </tr>

             
   
                        
                         <tr>
                             <td class="CellFormat" style="text-align:right;">City</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtBillCityEdit" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat" style="text-align:right;">State</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtBillStateEdit" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                             </td>
                         </tr>

             
   
                        
                         <tr>
                             <td class="CellFormat" style="text-align: right;">Country<asp:Label ID="Label62" runat="server" Font-Size="14px" ForeColor="Red" Text="*"></asp:Label>
                             </td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtBillCountryEdit" runat="server" Height="16px" MaxLength="50" Width="80%"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td class="CellFormat" style="text-align: right;">Postal</td>
                             <td class="CellTextBox">
                                 <asp:TextBox ID="txtBillPostalEdit" runat="server" Height="16px" MaxLength="20" Width="80%"></asp:TextBox>
                             </td>
                         </tr>

             
   
                        
                         <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnEditBillingDetailsSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime();"/>
                                 <asp:Button ID="btnEditBillingDetailsCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupEditBillingDetails" runat="server" CancelControlID="btnEditBillingDetailsCancel" PopupControlID="pnlEditBillingDetails" TargetControlID="btndummyEditBillingDetails" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
              <asp:Button ID="btndummyEditBillingDetails" runat="server" cssclass="dummybutton" />
            <%-- popup1--%>



                      <%-- start--%>

                <asp:Panel ID="pnlEditSalesman" runat="server" BackColor="White" Width="40%" Height="55%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table style="width:100%;padding-left:15px">
                         <tr>
                             <td colspan="2" style="text-align:center;"    >
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Edit Salesman </h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="Label19" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="Label20" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                          <tr>
                      <td class="CellFormat" style="text-align:right;" >Salesman </td>
                    <td class="CellTextBox">  
                        <asp:DropDownList ID="ddlSalesmanEdit" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="StaffId" DataValueField="StaffId" Height="25px" TabIndex="29" Width="80.5%">
                            <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                          
              
                      </td>
                                         
                  </tr>
           
                        
                         <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnEditSalesmanSave" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Save" Width="120px" OnClientClick="currentdatetime();"/>
                                 <asp:Button ID="btnEditSalesmanCancel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" />
                               </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

               <asp:ModalPopupExtender ID="mdlPopupEditSalesman" runat="server" CancelControlID="btnEditSalesmanCancel" PopupControlID="pnlEditSalesman" TargetControlID="btndummyEditSalesman" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
              <asp:Button ID="btndummyEditSalesman" runat="server" cssclass="dummybutton" />
            <%-- popup1--%>




             <%--Search End--%>

               <asp:Panel ID="pnlSearch" runat="server" BackColor="White" Width="85%" Height="95%" BorderColor="#003366" BorderWidth="1" Visible="true" ScrollBars="Auto" HorizontalAlign="Center">
              <br /><br />
                     <table border="0"  style="width:90%; border:thin;   padding-left:3px; margin-left:auto; margin-right:auto;  " >

                            <tr>
                               <td style="font-size:18px; font-weight:bold;font-family:Calibri;color:black;text-align:center;"  colspan="6">Search</td>
                           </tr>
                    
                            <tr>
                                <td colspan="6" style="font-size:18px; font-weight:bold;font-family:Calibri;color:black;text-align:center;">
                                    <asp:Label ID="lblAlertSearch" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="6" style="font-size:18px; font-weight:bold;font-family:Calibri;color:black;text-align:center;">&nbsp;</td>
                            </tr>
                    
                       <tr>
                               <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Sales Order No.
                               </td>
                              <td colspan="3" style="text-align:left;padding-left:5px; width:10%">   
                                   <asp:TextBox ID="txtSearchInvoiceNo" runat="server" MaxLength="50" Height="16px" Width="88%"></asp:TextBox>
                            </td> 
                                 <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Status</td>
                              <td style="width:20%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">  
                                  <asp:CheckBoxList ID="chkStatusSearch0" runat="server" CellPadding="2" CellSpacing="2" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table" TextAlign="Right">
                                      <asp:ListItem Value="O" Selected="True">O - Open/Pending</asp:ListItem>
                                      <asp:ListItem Value="P" Selected="true">P - Posted</asp:ListItem>
                                      <asp:ListItem Value="V">V - Void</asp:ListItem>
                                  </asp:CheckBoxList>
                            </td>                              
                           </tr>

                          <tr>
                      <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Sales Order Date From</td>
                       <td  style="text-align:left;padding-left:5px;width:10%">
                             <asp:TextBox ID="txtInvoiceDateSearchFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchContractDateFrom" TargetControlID="txtInvoiceDateSearchFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:15px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To
                       </td>
                    <td  style="text-align:left;width:15%; padding-right:8px; ">
                       <asp:TextBox ID="txtInvoiceDateSearchTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender9" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchContractDateTo" TargetControlID="txtInvoiceDateSearchTo"/>
                     </td>
                           
                         <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                             Paid Status
                             </td>
                                   <td style="width:12%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                    
                                       <asp:RadioButtonList ID="rdbSearchPaidStatus0" runat="server" RepeatDirection="Horizontal">
                                           <asp:ListItem Selected="True">All</asp:ListItem>
                                           <asp:ListItem>O/S</asp:ListItem>
                                           <asp:ListItem>Fully Paid</asp:ListItem>
                                       </asp:RadioButtonList>
                              </td>
                               </tr>

                         <tr>
                               <td style="width:10%; font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                              <td colspan="3" style="text-align:left;padding-left:5px;width:10%;">  
                                    &nbsp;</td>
                                 <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Batch No</td>
                              <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                  <asp:TextBox ID="txtSearchBatchInvoiceNo" runat="server" Height="16px" MaxLength="50" Width="88%"></asp:TextBox>
                               </td>
                             </tr>

                               <tr>
                                   <td style="width:10%; font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Account Type</td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:10%;">
                                       <asp:DropDownList ID="ddlSearchContactType" runat="server" AppendDataBoundItems="true" Height="25px" Width="89%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                           <asp:ListItem Value="COMPANY">CORPORATE</asp:ListItem>
                                           <asp:ListItem Value="PERSON">RESIDENTIAL</asp:ListItem>
                                       </asp:DropDownList>
                                   </td>
                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Our Reference</td>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       <asp:TextBox ID="txtSearchOurRef" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                                   </td>
                            </tr>

                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Account ID </td>
                                   <td colspan="1" style="text-align:left;padding-left:5px;width:10%">
                                       <asp:TextBox ID="txtSearchAccountID" runat="server" Height="16px" MaxLength="50" Width="70%"></asp:TextBox>
                                       &nbsp;<asp:ImageButton ID="btnClient2" runat="server" CssClass="righttextbox" Height="22px" ImageAlign="top" ImageUrl="~/Images/searchbutton.jpg" Width="24px" />
                                   </td>

                                    <td  style="text-align:left; padding-left:15px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  Name</td>
                    <td  style="text-align:left;width:10%; padding-right:8px; ">
                        <asp:TextBox ID="txtSearchClientName" runat="server" Height="16px" MaxLength="100" Width="80%"></asp:TextBox>
                              
                              </td>
                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Your Reference</td>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       <asp:TextBox ID="txtSearchYourRef" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                                   </td>
                               </tr>


                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Address</td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:10%">
                                               <asp:TextBox ID="txtSearchAddress" runat="server" MaxLength="50" Height="16px" Width="88%" AutoCompleteType="Disabled"></asp:TextBox>
               
                                   </td>
                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">PO No.</td>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       <asp:TextBox ID="txtSearchPONo" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                                   </td>
                               </tr>
                      
                         
                                     <tr>
                       <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Sales Amount</td>
                       <td  style="text-align:left;padding-left:5px;width:10%">
                             <asp:TextBox ID="txtSearchValueBaseFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:15px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:10%; padding-right:8px; ">
                       <asp:TextBox ID="txtSearchValueBaseTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                     </td>

                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       Over Due Days</td>
                                      <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                      <asp:TextBox ID="txtSearchOverDueDays" runat="server" Height="16px" MaxLength="50" Width="90%"></asp:TextBox>
                                   </td>
                               </tr>
                         
                                     <tr>
                       <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Invoice Amount</td>
                       <td  style="text-align:left;padding-left:5px;width:10%">
                             <asp:TextBox ID="txtSearchAppliedBaseFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:15px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:10%; padding-right:8px; ">
                       <asp:TextBox ID="txtSearchAppliedBaseTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                     </td>

                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                      </td>
                                      <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                    <asp:CheckBox ID="chkSearchShowUnPrintedInvoice" runat="server" AutoPostBack="false" Text="Show UnPrinted Invoice Only" />
                                   </td>
                               </tr>
                         
                                     <tr>
                       <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">OS Amount</td>
                       <td  style="text-align:left;padding-left:5px;width:10%">
                             <asp:TextBox ID="txtSearchBalanceBaseFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:15px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:10%; padding-right:8px; ">
                       <asp:TextBox ID="txtSearchBalanceBaseTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                     </td>

                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       Contract Group</td>
                                      <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                   
                                          <asp:DropDownList ID="ddlSearchContractGroup" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataTextField="UPPER(contractgroup)" DataValueField="UPPER(contractgroup)" Height="25px" Value="-1" Width="91%">
                                              <asp:ListItem Text="--SELECT--" Value="-1" />
                                          </asp:DropDownList>
                                   
                                   </td>
                               </tr>        
                         
                          <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Remarks</td>
                                   <td colspan="3" style="text-align:left;padding-left:5px;width:15%">
                                       <asp:TextBox ID="txtSearchComments" runat="server" Height="16px" MaxLength="100" Width="88%"></asp:TextBox>
                                   </td>
                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Salesman</td>
                                    <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                 
                                        <asp:DropDownList ID="ddlSearchSalesman" runat="server" AppendDataBoundItems="true" DataValueField="StaffId" Height="25px" Width="91%">
                                            <asp:ListItem Text="--SELECT--" Value="-1" />
                                        </asp:DropDownList>
                                   </td>
                               </tr>

                  

                               <tr>
                       <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Last Edit Date From</td>
                       <td  style="text-align:left;padding-left:5px;width:10%">
                             <asp:TextBox ID="txtSearchEditEndFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender18" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchActualEndFrom" TargetControlID="txtSearchEditEndFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:15px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:10%; padding-right:8px; ">
                       <asp:TextBox ID="txtSearchEditEndTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender19" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchActualEndTo" TargetControlID="txtSearchEditEndTo"/>
                     </td>

                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       Last Edited By</td>
                                      <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                     <asp:DropDownList ID="ddlSearchEditedBy" runat="server" AppendDataBoundItems="true" DataValueField="StaffId" Height="25px" Width="91%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                                   </td>
                               </tr>


                               <tr>
                                   <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;" >
                                       Entry Date From</td>
                       <td  style="text-align:left;padding-left:5px;width:10%">
                             <asp:TextBox ID="txtSearchEntryDateFrom" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender20" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchEntryDateFrom" TargetControlID="txtSearchEntryDateFrom"/>
                       </td>
                                   
                    <td  style="text-align:left; padding-left:15px; padding-right:25px; font-size:15px;font-weight:bold;font-family:Calibri;color:black; width:2%">  To</td>
                    <td  style="text-align:left;width:10%; padding-right:8px; ">
                       <asp:TextBox ID="txtSearchEntryDateTo" runat="server" MaxLength="50" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender21" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtSearchEntryDateTo" TargetControlID="txtSearchEntryDateTo"/>
                     </td>

                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       Created By</td>
                                    <td style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       <asp:DropDownList ID="ddlSearchCreatedBy" runat="server" AppendDataBoundItems="true" DataValueField="StaffId" Height="25px" Width="91%">
                                           <asp:ListItem Text="--SELECT--" Value="-1" />
                                       </asp:DropDownList>
                                   </td>
                               </tr>

                               <tr>
                                   <td colspan="4" style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                            </tr>
                               
                               <tr>
                                   <td colspan="1" style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Detail Service Location</td>
                                    <td colspan="3" style="text-align:left;padding-left:5px; width:10%">   
                                   <asp:TextBox ID="txtSearchDetailServiceLocation" runat="server" MaxLength="50" Height="16px" Width="88%"></asp:TextBox>
                            </td> 
                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Detail&nbsp; Contract</td>
                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                       <asp:TextBox ID="txtSearchDetailContractNo" runat="server" AutoCompleteType="Disabled" Height="16px" MaxLength="50" Width="91%"></asp:TextBox>
                                   </td>
                            </tr>
                            <tr>
                                <td colspan="1" style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Detail Reference<br /> </td>
                               <td colspan="3" style="text-align:left;padding-left:5px; width:10%">   
                                   <asp:TextBox ID="txtSearchDetailReference" runat="server" MaxLength="50" Height="16px" Width="88%"></asp:TextBox>
                            </td> 
                                  <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">Detail Ledger Code</td>
                                <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                    <asp:DropDownList ID="ddlCOACode" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" Height="25px" TabIndex="25" Width="91.5%">
                                        <asp:ListItem Text="--SELECT--" Value="-1" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                               
                               <tr>
                                   <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">PO No (in Contract)</td>
                                  <td colspan="3" style="text-align:left;padding-left:5px; width:10%">  
                                       <asp:TextBox ID="txtSearchDetailPONONo" runat="server" MaxLength="50" Height="16px" Width="88%"></asp:TextBox>

                                   </td>
                                      <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">
                                          &nbsp;</td>
                              <td>&nbsp;</td>
                               </tr>
                          

                            <tr>
                                <td>&nbsp;</td>
                                <td colspan="3" style="text-align:right">&nbsp;</td>
                                <td style="width:8%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                          

                            <tr>
                                <td></td>
                                <td colspan="3" style="text-align: right">
                                    <asp:Button ID="btnSearch" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Search" Width="100px" />
                                </td>
                                <td style="width: 8%; font-size: 15px; font-weight: bold; font-family: Calibri; color: black; text-align: left;">
                                    <asp:Button ID="btnCloseSearch" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Cancel" Width="100px" />
                                </td>
                                <td></td>
                            </tr>
                          

        </table>
           </asp:Panel>
    <asp:ModalPopupExtender ID="mdlPopupSearch" runat="server" CancelControlID="btnCloseSearch" PopupControlID="pnlSearch" TargetControlID="btnDummySearch" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
                <asp:Button ID="btnDummySearch" runat="server" cssclass="dummybutton" />

            <%-- Search Start--%>


                 <asp:Panel ID="pnlStaff" runat="server" BackColor="White" Width="45%" Height="80%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
         <table><tr>
                               <td colspan="2" style="text-align:center;"><h4 style="color: #000000">Staff</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlStaffClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                  <tr>
                               <td colspan="2" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:center;">
                               <asp:TextBox ID="txtPopUpStaff" runat="server" MaxLength="50" Height="16px" Width="400px" Text = "Search Here" ForeColor = "Gray" onblur = "WaterMarkStaff(this, event);" onfocus = "WaterMarkStaff(this, event);" AutoPostBack="True"></asp:TextBox>
                                   <asp:ImageButton ID="btnPopUpStaffSearch" OnClick="btnPopUpStaffSearch_Click" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" Visible="False" />
                            <asp:ImageButton ID="btnPopUpStaffReset" OnClick="btnPopUpStaffReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" />
                             </td> <td></td>
                                      </tr>
                           </table>
       
        <asp:TextBox ID="txtPopupStaffSearch" runat="server" Visible="False"></asp:TextBox>
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="gvStaff" runat="server" CssClass="Centered" DataSourceID="SqlDSStaffID" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="500px"  OnRowDataBound = "OnRowDataBoundgStaff" OnSelectedIndexChanged = "OnSelectedIndexChangedgStaff">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                     <asp:BoundField DataField="StaffId" HeaderText="StaffId" SortExpression="StaffId" ReadOnly="True">
                       <ControlStyle Width="80PX" />
                  <HeaderStyle Width="80px" Wrap="False" HorizontalAlign="Left" />
                  <ItemStyle Width="80px" Wrap="False" />
                </asp:BoundField>
                
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                        <ControlStyle Width="120px" />
                        <HeaderStyle Width="120px" HorizontalAlign="Left" />
                        <ItemStyle Width="120px" Wrap="True" />
                    </asp:BoundField>
                   
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
           <asp:SqlDataSource ID="SqlDSStaffID" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
   
         
        </div>
        
           </asp:Panel>
     
     <asp:ModalPopupExtender ID="mdlPopupStaff" runat="server" CancelControlID="btnPnlStaffClose" PopupControlID="pnlStaff" TargetControlID="btndummystaff" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
   
       <asp:Button ID="btndummystaff" runat="server" cssclass="dummybutton" />
</div>


       <%--  end--%>


        <%-- Start: CleintSelection-2--%>

         
         
<asp:Panel ID="pnlPopUpClient2" runat="server" BackColor="White" Width="98%" Height="80%" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center" ScrollBars="Horizontal">
    <table style="margin-left:auto; margin-right:auto; "><tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Customer</h4></td><td style="width:1%;text-align:right;">
        <asp:ImageButton ID="btnPnlClientClose2" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" />
    </td></tr>
        
        <tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri; color:black;text-align:center;padding-left:40px;">&#160;Search Name &#160;&#160;
         <asp:TextBox ID="txtPopUpClient2" runat="server" Height="16px" MaxLength="50" Width="400px" ForeColor="Gray" onblur = "WaterMarkClient(this, event);" onfocus = "WaterMarkClient(this, event);" AutoPostBack="True">Search Here for AccountID or Client Name or Contact Person</asp:TextBox>
    <asp:ImageButton ID="btnPopUpClientSearchSearch2" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" OnClick="btnPopUpClientSearch_Click" Width="24px" Visible="False"></asp:ImageButton>
    <asp:ImageButton ID="btnPopUpClientResetSearch2" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/resetbutton.jpg" OnClick="btnPopUpClientReset_Click" Width="24px"></asp:ImageButton>


</td><td>
                <asp:TextBox ID="txtPopupClientSearch2" runat="server" Visible="False"></asp:TextBox>

            </td></tr>


    </table><div style="text-align:center; padding-left: 20px; padding-bottom: 5px;"><div class="AlphabetPager">
       
                
</div><br />
                    <asp:GridView ID="gvClient2" runat="server" DataSourceID="SqlDSClient2" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                       CellPadding="2" GridLines="Vertical" Font-Size="15px" Width="97%" OnRowDataBound = "OnRowDataBoundgClient2" OnSelectedIndexChanged = "OnSelectedIndexChangedgClient2" CellSpacing="6">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
        <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" Visible="false">
        <ControlStyle Width="5%" />

        <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle Width="5%" />
        </asp:CommandField>
            <asp:BoundField DataField="AccountType" HeaderText="Account Type" />
        <asp:BoundField DataField="AccountID" HeaderText="Account ID" SortExpression="AccountID" >
        <ControlStyle Width="8%" />

        <HeaderStyle Wrap="False" HorizontalAlign="Left" />

        </asp:BoundField>
        <asp:BoundField DataField="BillingName" HeaderText="Client Name" SortExpression="Name">
        <ControlStyle Width="35%" />

        <HeaderStyle HorizontalAlign="Left" Wrap="False" />

        <ItemStyle Wrap="False" />
        </asp:BoundField>


        <asp:BoundField DataField="BillContactPerson" HeaderText="Contact Person" SortExpression="BillContactPerson">

        <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle Wrap="False" />
        </asp:BoundField>
        <asp:BoundField DataField="BillAddress1" HeaderText="Bill Address" >
            <ItemStyle Wrap="False" />
        </asp:BoundField>

    <asp:BoundField DataField="BillPostal" HeaderText="Postal" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Wrap="False" HorizontalAlign="Left" />
        </asp:BoundField>


        </Columns>

        <EditRowStyle BackColor="#999999" />

        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" />

        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Left" />

        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" />

        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />

        <SortedAscendingCellStyle BackColor="#E9E7E2" />

        <SortedAscendingHeaderStyle BackColor="#506C8C" />

        <SortedDescendingCellStyle BackColor="#FFFDF8" />

        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>



                <asp:SqlDataSource ID="SqlDSClient2" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                        ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"><FilterParameters>
        <asp:ControlParameter Name="ContType" ControlID="ddlContactType" PropertyName="SelectedValue" Type="String" />
        </FilterParameters>
        </asp:SqlDataSource>
                
      
        </div></asp:Panel>

         

  <asp:ModalPopupExtender ID="mdlPopUpClient2" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlClientClose2" Enabled="True" PopupControlID="pnlPopUpClient2" TargetControlID="btnDummyClient2">
                                    </asp:ModalPopupExtender>

          <asp:Button ID="btnDummyClient2" runat="server" BackColor="White" CssClass="roundbutton" Font-Bold="True" Width="24px" BorderStyle="None" /> 
    

       <%--  End: Client Selection-2--%>


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
                
           
                    <tr><td colspan="2" style="text-align:CENTER"><asp:Label ID="Label27" runat="server" Text="" Font-Names="Calibri" Font-Size="20px" Font-Bold="True" ForeColor="Red"></asp:Label></td></tr>
        </table>
              <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
        
        <asp:GridView ID="grdViewEditHistory" runat="server" DataSourceID="sqlDSViewEditHistory" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
         CellPadding="2" GridLines="None" Width="99%"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>  
              
                <asp:BoundField DataField="LogDate" HeaderText="Date &amp; Time" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" >
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

         <asp:Panel ID="pnlPopUpContractNo" runat="server" BackColor="White" Width="800" Height="600" BorderColor="#003366" BorderWidth="1" Visible="true"
        HorizontalAlign="Left" ScrollBars="None">
        <table>
            <tr>
                <td style="text-align: center;">
                    <h4 style="color: #000000">Contract Number</h4>
                </td>
                <td style="width: 1%; text-align: right;">
                    <asp:ImageButton ID="btnPopUpContractNoClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
            </tr>
        </table>

        <div class="wrp">
            <div class="frm">
                <table style="text-align: center;">
                    <tr>
                        <td style="font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: right; width:30%;">Search</td>
                        <td style="text-align: left; width:30%">
                            <asp:TextBox ID="txtPopUpContractNo" runat="server" AutoCompleteType="Disabled" AutoPostBack="True" Width="98%"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width:30%">
                             <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/>
                       </td>
                     
                    </tr>
                </table>
            </div>
        </div>
        <br />
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="gvPopUpContractNo" runat="server" DataSourceID="SqlDSContractNo" ForeColor="#333333" 
                AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="80%" RowStyle-HorizontalAlign="Left" PageSize="12" Font-Size="14px">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>

                      <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ContractNo" HeaderText="Contract Number" SortExpression="ContractNo">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CustName" HeaderText="Customer Name">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
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
            <asp:SqlDataSource ID="SqlDSContractNo" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                     </asp:SqlDataSource>
        </div>
    </asp:Panel>
      <asp:ModalPopupExtender ID="mdlPopUpContractNo" runat="server" CancelControlID="btnPopUpContractNoClose" PopupControlID="pnlPopUpContractNo"
                                                TargetControlID="btnContractNo" BackgroundCssClass="modalBackground">
                                            </asp:ModalPopupExtender>
          <asp:Button ID="btnContractNo" runat="server" CssClass="dummybutton" />

        <%-- end--%>


        <%-- start--%>

               <asp:Panel ID="pnlPopUpJournalView" runat="server" BackColor="White" Width="1200" Height="600" BorderColor="#003366" BorderWidth="1" Visible="true"
        HorizontalAlign="Left" ScrollBars="None">
        <table>
            <tr>
                <td style="text-align: center;">
                    <h4 style="color: #000000">Journal</h4>
                </td>
                <td style="width: 1%; text-align: right;">
                    <asp:ImageButton ID="btnPopUpJournalClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
            </tr>
        </table>

       
        
        <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
            <br />
            <asp:GridView ID="GrdJournalView" runat="server" DataSourceID="SqlDSJournal" ForeColor="#333333" 
                AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="4" GridLines="None" Width="99%" RowStyle-HorizontalAlign="Left" PageSize="12" Font-Size="14px">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="PostStatus" HeaderText="ST" SortExpression="PostStatus">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="VoucherNumber" HeaderText="Voucher Number">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:BoundField DataField="JournalDate" HeaderText="Journal Date" DataFormatString="{0:dd/MM/yyyy}" >
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DebitBase" HeaderText="Debit" >
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CreditBase" HeaderText="Credit" >
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LedgerCode" HeaderText="Ledger Code" >
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LedgerName" HeaderText="Ledger Name" >
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Description" HeaderText="Description" />
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
            <asp:SqlDataSource ID="SqlDSJournal" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>">
                     </asp:SqlDataSource>
        </div>
    </asp:Panel>
      <asp:ModalPopupExtender ID="mdlPopUpJournalView" runat="server" CancelControlID="btnPopUpJournalClose" PopupControlID="pnlPopUpJournalView"
                                                TargetControlID="btnJournalView" BackgroundCssClass="modalBackground">
                                            </asp:ModalPopupExtender>
          <asp:Button ID="btnJournalView" runat="server" CssClass="dummybutton" />

        <%-- end--%>

        <%-- end--%>

        </ContentTemplate>
         
          <Triggers>
              <asp:AsyncPostBackTrigger ControlID="tb1$TabPanel1$ddlCreditTerms" EventName="SelectedIndexChanged" />
              <asp:PostBackTrigger ControlID="btnMultiPrint" />
          </Triggers>
         
</asp:UpdatePanel>

    

    <script type="text/javascript">
        window.history.forward(1);

        var submit = 0;
        var submit1 = 0;

        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);


        function DoValidation() {

            var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency
            var valid = true;
            //alert(submit);
            //if (++submit > 1) {

            //    alert('Saving the Invoice is in progress.. Please wait.');
            //    submit = 0;
            //    valid = false;
            //    return valid;
            //}


            var linvoicedate = document.getElementById("<%=txtInvoiceDate.ClientID%>").value;

        if (linvoicedate == '') {
            document.getElementById("<%=lblAlert.ClientID%>").innerText = "Please Enter Invoice Date";
            ResetScrollPosition();
            document.getElementById("<%=txtInvoiceDate.ClientID%>").focus();
                valid = false;
                return valid;
            }


            currentdatetime();

            return valid;
        }

        function RefreshSubmit() {
            submit = 0;
            submit1 = 0;
        }


       

        function DoValidationSaveQR(parameter) {

            var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency
            var valid = true;

            if (++submit1 > 1) {
                alert('Saving the Receipt is in progress.. Please wait.');
                submit1 = 0;
                valid = false;
                return valid;
            }

            submit1 = 0;
            var linvoicedate = document.getElementById("<%=txtReceiptDateQR.ClientID%>").value;

        if (linvoicedate == '') {
            document.getElementById("<%=lblAlertQR.ClientID%>").innerText = "Please Enter Receipt Date";
            ResetScrollPosition();
            document.getElementById("<%=txtReceiptDateQR.ClientID%>").focus();
            valid = false;
            return valid;
        }

        var lcompanygroup = document.getElementById("<%=txtCompanyGroup.ClientID%>").value;

        if (lcompanygroup == '--SELECT--') {
            document.getElementById("<%=lblAlertQR.ClientID%>").innerText = "Please Enter Company Group";
             ResetScrollPosition();
             document.getElementById("<%=txtCompanyGroup.ClientID%>").focus();
             valid = false;
             return valid;
         }

         var lpaymentmode = document.getElementById("<%=ddlPaymentModeQR.ClientID%>").value;

        if ((lpaymentmode == '') || (lpaymentmode == '--SELECT--')) {
            document.getElementById("<%=lblAlertQR.ClientID%>").innerText = "Please Select Payment Mode";
             ResetScrollPosition();
             document.getElementById("<%=ddlPaymentModeQR.ClientID%>").focus();
             valid = false;
             return valid;
         }


         var lBankGLCode = document.getElementById("<%=txtBankGLCodeQR.ClientID%>").value;
        if ((lBankGLCode == '') || (lBankGLCode == '--SELECT--')) {
            document.getElementById("<%=lblAlertQR.ClientID%>").innerText = "Please Select Bank";
             ResetScrollPosition();
             document.getElementById("<%=txtBankGLCodeQR.ClientID%>").focus();
            valid = false;
            return valid;
        }

        currentdatetime();


        return valid;
    };


    function BeginRequestHandler(sender, args) {
        xPos = document.getElementById("<%=updpnlBillingDetails.ClientID%>").scrollLeft;
        yPos = document.getElementById("<%=updpnlBillingDetails.ClientID%>").scrollTop;
    }


    function EndRequestHandler(sender, args) {
        document.getElementById("<%=updpnlBillingDetails.ClientID%>").scrollLeft = xPos;
        document.getElementById("<%=updpnlBillingDetails.ClientID%>").scrollTop = yPos;
    }


    var defaultText1 = "Search Here";
    function WaterMarkStaff(txt, evt) {
        if (txt.value.length == 0 && evt.type == "blur") {
            txt.style.color = "gray";
            txt.value = defaultText1;
        }
        if (txt.value == defaultText1 && evt.type == "focus") {
            txt.style.color = "black";
            txt.value = "";
        }
    }


    //// start

    function checkselectallrecs() {
        var table = document.getElementById('<%=grvBillingDetails.ClientID%>');
        var totbillamt = 0;
        var totdiscamt = 0;
        var totamountwithdiscount = 0;
        var totgstamt = 0;
        var totbillamtnet = 0;

        if (table.rows.length > 0) {

            var input = table.rows[0].getElementsByTagName("input");

            if (input[0].id.indexOf("chkSelecAllRec") > -1) {

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


                    document.getElementById("<%=txtInvoiceAmount.ClientID%>").value = "0.00";
                     document.getElementById("<%=txtDiscountAmount.ClientID%>").value = "0.00";
                     document.getElementById("<%=txtAmountWithDiscount.ClientID%>").value = "0.00";
                     document.getElementById("<%=txtTotalWithDiscAmt.ClientID%>").value = "0.00";
                     document.getElementById("<%=txtGSTAmount.ClientID%>").value = "0.00";
                     document.getElementById("<%=txtnetamount.ClientID%>").value = "0.00";

                     document.getElementById("<%=txtTotalGSTAmt.ClientID%>").value = "0.00";
                     document.getElementById("<%=txtTotalWithGST.ClientID%>").value = "0.00";

                 }


                 else {
                     //loop the gridview table
                     for (var i = 1; i < table.rows.length; i++) {

                         //get all the input elements
                         var inputs = table.rows[i].getElementsByTagName("input");

                         for (var j = 0; j < inputs.length; j++) {
                             //get the textbox1
                             if (inputs[j].id.indexOf("txtTotalPriceGV") > -1) {
                                 inputs[0].checked = true;
                                 if ((inputs[j].value).length == 0) {
                                 }
                                 else
                                     totbillamt = totbillamt + parseFloat(inputs[j].value);
                             }

                             if (inputs[j].id.indexOf("txtDiscAmountGV") > -1) {
                                 inputs[0].checked = true;
                                 if ((inputs[j].value).length == 0) {
                                 }
                                 else
                                     totdiscamt = totdiscamt + parseFloat(inputs[j].value);
                             }


                             if (inputs[j].id.indexOf("txtPriceWithDiscGV") > -1) {
                                 inputs[0].checked = true;
                                 if ((inputs[j].value).length == 0) {
                                 }
                                 else
                                     totamountwithdiscount = totamountwithdiscount + parseFloat(inputs[j].value);
                             }


                             if (inputs[j].id.indexOf("txtGSTAmtGV") > -1) {
                                 inputs[0].checked = true;
                                 if ((inputs[j].value).length == 0) {
                                 }
                                 else
                                     totgstamt = totgstamt + parseFloat(inputs[j].value);
                             }


                             if (inputs[j].id.indexOf("txtTotalPriceWithGSTGV") > -1) {
                                 inputs[0].checked = true;
                                 if ((inputs[j].value).length == 0) {
                                 }
                                 else
                                     totbillamtnet = totbillamtnet + parseFloat(inputs[j].value);
                             }


                         }
                     }
                     document.getElementById("<%=txtInvoiceAmount.ClientID%>").value = (totbillamt).toFixed(2);

                document.getElementById("<%=txtDiscountAmount.ClientID%>").value = (totdiscamt).toFixed(2);
                     document.getElementById("<%=txtAmountWithDiscount.ClientID%>").value = (totamountwithdiscount).toFixed(2);
                     document.getElementById("<%=txtTotalWithDiscAmt.ClientID%>").value = (totamountwithdiscount).toFixed(2);
                     document.getElementById("<%=txtGSTAmount.ClientID%>").value = (totamountwithdiscount * .07).toFixed(2);
                     document.getElementById("<%=txtNetAmount.ClientID%>").value = (totamountwithdiscount + (totamountwithdiscount * .07)).toFixed(2);

                     document.getElementById("<%=txtTotalGSTAmt.ClientID%>").value = (totgstamt).toFixed(2);
                     document.getElementById("<%=txtTotalWithGST.ClientID%>").value = (totbillamtnet).toFixed(2);


                 }
             }
         }

     }


     function checkselectrec() {
         //alert("1");
         var table = document.getElementById('<%=grvBillingDetails.ClientID%>');
        var totbillamt = 0;
        var totdiscamt = 0;
        var totamountwithdiscount = 0;
        var totgstamt = 0;
        var totbillamtnet = 0;

        if (table.rows.length > 0) {


            var input = table.rows[0].getElementsByTagName("input");

            //loop the gridview table
            for (var i = 1; i < table.rows.length; i++) {

                //get all the input elements
                var inputs = table.rows[i].getElementsByTagName("input");

                for (var j = 0; j < inputs.length; j++) {

                    //get the textbox1
                    if (inputs[0].checked == true) {
                        if (inputs[j].id.indexOf("txtTotalPriceGV") > -1) {

                            if ((inputs[j].value).length == 0) {
                            }
                            else
                                totbillamt = totbillamt + parseFloat(inputs[j].value);
                        }



                        if (inputs[j].id.indexOf("txtDiscAmountGV") > -1) {
                            if ((inputs[j].value).length == 0) {
                            }
                            else
                                totdiscamt = totdiscamt + parseFloat(inputs[j].value);
                        }


                        if (inputs[j].id.indexOf("txtPriceWithDiscGV") > -1) {

                            if ((inputs[j].value).length == 0) {
                            }
                            else
                                totamountwithdiscount = totamountwithdiscount + parseFloat(inputs[j].value);
                        }


                        if (inputs[j].id.indexOf("txtGSTAmtGV") > -1) {

                            if ((inputs[j].value).length == 0) {
                            }
                            else
                                totgstamt = totgstamt + parseFloat(inputs[j].value);
                        }


                        if (inputs[j].id.indexOf("txtTotalPriceWithGSTGV") > -1) {

                            if ((inputs[j].value).length == 0) {
                            }
                            else
                                totbillamtnet = totbillamtnet + parseFloat(inputs[j].value);
                        }

                    }

                }

            }

            document.getElementById("<%=txtInvoiceAmount.ClientID%>").value = (totbillamt).toFixed(2);

            document.getElementById("<%=txtDiscountAmount.ClientID%>").value = (totdiscamt).toFixed(2);
            document.getElementById("<%=txtAmountWithDiscount.ClientID%>").value = (totamountwithdiscount).toFixed(2);
            document.getElementById("<%=txtTotalWithDiscAmt.ClientID%>").value = (totamountwithdiscount).toFixed(2);
            document.getElementById("<%=txtGSTAmount.ClientID%>").value = (totamountwithdiscount * .07).toFixed(2);
            document.getElementById("<%=txtNetAmount.ClientID%>").value = (totamountwithdiscount + (totamountwithdiscount * .07)).toFixed(2);

            document.getElementById("<%=txtTotalGSTAmt.ClientID%>").value = (totgstamt).toFixed(2);
            document.getElementById("<%=txtTotalWithGST.ClientID%>").value = (totbillamtnet).toFixed(2);
        }
    }

    ///// end


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


        function checkallservicerecs() {
            var table = document.getElementById('<%=grvServiceRecDetails.ClientID%>');
        var totbillamt = 0;


        if (table.rows.length > 0) {

            var input = table.rows[0].getElementsByTagName("input");

            if (input[0].id.indexOf("CheckBox1") > -1) {

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


                    document.getElementById("<%=txtTotalServiceSelected.ClientID%>").value = "0.00";
            }


            else {
                    //loop the gridview table
                for (var i = 1; i < table.rows.length; i++) {

                    //get all the input elements
                    var inputs = table.rows[i].getElementsByTagName("input");

                    for (var j = 0; j < inputs.length; j++) {

                        //get the textbox1
                        if (inputs[j].id.indexOf("txtToBillAmtGV") > -1) {
                            inputs[0].checked = true;
                            totbillamt = totbillamt + parseFloat(inputs[j].value);

                        }

                    }
                }
                document.getElementById("<%=txtTotalServiceSelected.ClientID%>").value = (totbillamt).toFixed(2);
                }
            }
        }

    }


    function checkoneservicerec() {
        //alert("1");
        var table = document.getElementById('<%=grvServiceRecDetails.ClientID%>');
        var totbillamt = 0;


        if (table.rows.length > 0) {
            //alert("2");

            var input = table.rows[0].getElementsByTagName("input");

            //loop the gridview table
            for (var i = 1; i < table.rows.length; i++) {

                //get all the input elements
                var inputs = table.rows[i].getElementsByTagName("input");

                for (var j = 0; j < inputs.length; j++) {

                    //get the textbox1
                    if (inputs[0].checked == true)
                        if (inputs[j].id.indexOf("txtToBillAmtGV") > -1) {

                            totbillamt = totbillamt + parseFloat(inputs[j].value);
                        }
                }

            }

            document.getElementById("<%=txtTotalServiceSelected.ClientID%>").value = (totbillamt).toFixed(2);
        }
    }




    function checkallinvoicerecs() {
        //alert("1");
        var table = document.getElementById('<%=grvInvoiceRecDetails.ClientID%>');
        var totbillamt = 0;


        if (table.rows.length > 0) {
            //alert("2");

            var input = table.rows[0].getElementsByTagName("input");

            if (input[0].id.indexOf("chkSelectInvoiceGV") > -1) {

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


                    document.getElementById("<%=txtTotalInvoiceSelected.ClientID%>").value = "0.00";
                }


                else {
                    //loop the gridview table
                    for (var i = 1; i < table.rows.length; i++) {

                        //get all the input elements
                        var inputs = table.rows[i].getElementsByTagName("input");

                        for (var j = 0; j < inputs.length; j++) {

                            //get the textbox1
                            if (inputs[j].id.indexOf("txtOSAmountGVII") > -1) {
                                inputs[0].checked = true;
                                totbillamt = totbillamt + parseFloat(inputs[j].value);

                            }

                        }
                    }
                    document.getElementById("<%=txtTotalInvoiceSelected.ClientID%>").value = (totbillamt).toFixed(2);
            }
        }
    }

}


function checkoneinvoicerec() {
    //alert("1");
    var table = document.getElementById('<%=grvInvoiceRecDetails.ClientID%>');
        var totbillamt = 0;


        if (table.rows.length > 0) {
            //alert("2");

            var input = table.rows[0].getElementsByTagName("input");

            //loop the gridview table
            for (var i = 1; i < table.rows.length; i++) {

                //get all the input elements
                var inputs = table.rows[i].getElementsByTagName("input");

                for (var j = 0; j < inputs.length; j++) {

                    //get the textbox1
                    if (inputs[0].checked == true)
                        if (inputs[j].id.indexOf("txtOSAmountGVII") > -1) {

                            totbillamt = totbillamt + parseFloat(inputs[j].value);
                        }
                }

            }

            document.getElementById("<%=txtTotalInvoiceSelected.ClientID%>").value = (totbillamt).toFixed(2);
        }
    }





    function checkmultiprint() {

        var table = document.getElementById('<%=grdViewMultiPrint.ClientID%>');
        var totbillamt = 0;


        if (table.rows.length > 0) {
            //alert("2");

            var input = table.rows[0].getElementsByTagName("input");

            if (input[0].id.indexOf("chkSelectAllMultiPrintGV") > -1) {

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




    function ConfirmPost() {
        currentdatetime();
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("Are you sure to POST the Invoice?")) {
            confirm_value.value = "Yes";
        } else {
            confirm_value.value = "No";
        }
        document.forms[0].appendChild(confirm_value);
    }


    function ConfirmUnPost() {
        currentdatetime();
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("Are you sure to REVERSE the Invoice?")) {
            confirm_value.value = "Yes";
        } else {
            confirm_value.value = "No";
        }
        document.forms[0].appendChild(confirm_value);
    }

    function CollapseExpand() {



        var optionselected;
        var durationms = document.getElementById("<%=rbtInvoiceType.ClientID%>");
               var radio = durationms.getElementsByTagName("input");
               for (var i = 0; i < radio.length; i++) {
                   if (radio[i].checked) {
                       optionselected = i;
                       break;
                   }
               }






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


           var defaultTextGL = "Search Here for GL Code or Description";
           function WaterMarkGL(txt, evt) {
               if (txt.value.length == 0 && evt.type == "blur") {
                   txt.style.color = "gray";
                   txt.value = defaultTextGL;
               }
               if (txt.value == defaultTextGL && evt.type == "focus") {
                   txt.style.color = "black";
                   txt.value = "";
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

    function currentdatetimeinvoice() {
        var linvoiceno = document.getElementById("<%=txtInvoiceNo.ClientID%>").value;

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
        //     alert('ok');

        if (linvoiceno == '') {
            document.getElementById("<%=txtInvoiceDate.ClientID%>").value = dd + "/" + mm + "/" + y;
                document.getElementById("<%=txtBillingPeriod.ClientID%>").value = "" + y + mm;
            }

        }


        function currentdatetimereceipt() {
            currentdatetime();
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


            document.getElementById("<%=txtReceiptDateQR.ClientID%>").value = dd + "/" + mm + "/" + y;
        document.getElementById("<%=txtReceiptPeriodQR.ClientID%>").value = y + mm;


    }

    function initialize() {
        document.getElementById("<%=txtInvoiceNo.ClientID%>").value = '';

        currentdatetimeinvoice()
        currentdatetime()
    }

    function DoValidationShowRecords(parameter) {

        var regex = /^[1-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/; //for currency
        var valid = true;

        var lcompanygroup = document.getElementById("<%=ddlCompanyGrp.ClientID%>").value;
        var lcontactType = document.getElementById("<%=ddlContactType.ClientID%>").value;
        var laccountid = document.getElementById("<%=txtAccountId.ClientID%>").value;
        var lclientname = document.getElementById("<%=txtClientName.ClientID%>").value;


        if ((lcompanygroup == "--SELECT--") && (lcontactType == "--SELECT--") && (laccountid == "") && (lclientname == "")) {

            document.getElementById("<%=lblAlert1.ClientID%>").innerText = "Please Enter Company Group/Contact Type/ Account ID / Client Name";
             ResetScrollPosition();
             document.getElementById("<%=txtClientName.ClientID%>").focus();
                valid = false;
                return valid;
            }

            currentdatetime();

            return valid;
        };




        function ResetScrollPosition() {
            setTimeout("window.scrollTo(0,0)", 0);
        }

</script>
</asp:Content>

