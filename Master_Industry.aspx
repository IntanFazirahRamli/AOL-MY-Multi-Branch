<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_Industry.aspx.vb" Inherits="Master_Industry" Title="Industry Master"  Culture="en-GB"%>


 <%@ Register Assembly="Ajaxified" Namespace="Ajaxified" TagPrefix="Ajaxified" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        
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
    
         

    
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
            <ControlBundles>
                <asp:ControlBundle Name="CalendarExtender_Bundle" />
                <asp:controlBundle name="ModalPopupExtender_Bundle"/>  
                <asp:controlBundle name="MaskedEditExtender_Bundle"/>
                <asp:controlBundle name="TabContainer_Bundle"/>     
                  <asp:controlBundle name="ComboBox_Bundle"/>               
            </ControlBundles>
        </asp:ToolkitScriptManager>
     

    <asp:Button ID="Button1" runat="server" Text="" cssclass="dummybutton" />

     <div>
   
     <div style="text-align:center">
         
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">INDUSTRY/MARKET SEGMENT</h3>
     

    <asp:Button ID="dummy" runat="server" Text="" cssclass="dummybutton" />
   
    
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
              
                <td style="width:40%;text-align:left;">
                  <asp:Button ID="btnADD" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" />
<asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="true" />
                    <asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()"/>

                     <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" />

                       </td>
                <td style="text-align: right">
                    <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" /></td>
                
            </tr>
                  <tr>
              <td colspan="2">
              <asp:SqlDataSource ID="SqlDSMarketSegmentID" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT CONCAT(MarketSegmentID, ' - ', Description) AS MarketSegmentID FROM tblindustrysegment ORDER BY MarketSegmentID">
                         </asp:SqlDataSource>
               <br /></td>
            </tr>
       <tr>
           <td colspan="2" style ="text-align:RIGHT">
                  <asp:ImageButton ID="btnExportToExcel" runat="server" ImageUrl="~/Images/excel.png" Width="40px" Height="35px" OnClientClick="currentdatetime()" />
                  
           </td>
       </tr>
            <tr class="Centered">
                <td colspan="2">
  <asp:GridView ID="GridView1" runat="server" CssClass="centered" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Industry" Font-Size="15px" AllowSorting="True" AllowPaging="True" HorizontalAlign="Center">
            <Columns>
                 <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" >
                <ControlStyle Width="50px" />
                <ItemStyle Width="50px" />
                </asp:CommandField>
                <asp:BoundField DataField="Industry" HeaderText="Industry" ReadOnly="True" SortExpression="Industry">
                <ControlStyle Width="280px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="280px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description">
                <ControlStyle Width="240px" />
                <HeaderStyle Font-Size="12pt" />
                <ItemStyle Width="240px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField DataField="MarketSegmentID" HeaderText="Market Segment ID" SortExpression="MarketSegmentID" />
                 <asp:BoundField DataField="MarketSegmentDescription" HeaderText="Description">
                 <ControlStyle Width="350px" />
                 <ItemStyle Width="350px" />
                 </asp:BoundField>

                 <asp:TemplateField ><ItemTemplate> <asp:Button ID="btnRevenueDistribution" runat="server" OnClick="btnRevenueDistribution_Click" Text="REV. DISTR." CssClass="righttextbox" Height="25px" Visible="true" OnClientClick="currentdatetime()" ImageAlign="Top"   Width="90px"   />
                                               </ItemTemplate></asp:TemplateField>
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
                     

                    <td class="CellFormatADM">Industry<asp:Label ID="Label23" runat="server" visible="true" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                     

                    <td class="CellTextBoxADM"> <asp:TextBox ID="txtIndustry" runat="server" MaxLength="50" Height="16px" Width="45%"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Description</td>
                     

                    <td class="CellTextBoxADM"><asp:TextBox ID="txtDescription" runat="server" MaxLength="100" Height="16px" Width="45%"></asp:TextBox></td>
                     

                 </tr>
              <tr>
                     

                    <td class="CellFormatADM">Market Segment ID</td>
                     

                    <td class="CellTextBoxADM"> 
                               <asp:DropDownList ID="ddlMarketSegmentID" runat="server" AppendDataBoundItems="True" CssClass="chzn-select" DataSourceID="SqlDSMarketSegmentID" DataTextField="MarketSegmentID" DataValueField="MarketSegmentID" Height="25px" Width="45%" TabIndex="25">
                                   <asp:ListItem Text="--SELECT--" Value="-1" /></asp:DropDownList>
                              
                            </td>
                     

                 </tr>
           <tr>
               <td colspan="2" style="text-align:right">   
                   <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" />
                   <asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" />
               </td>
           </tr>
          </table>
    
       </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT Industry, Description, MarketSegmentID, MarketSegmentDescription, Rcno, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn FROM tblindustry WHERE (Rcno &lt;&gt; 0)"></asp:SqlDataSource>

       


               <%-- start: Contract Group Distribution --%>

      
 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
          <ContentTemplate>

            <asp:Panel ID="pnlContractGroupDistribution" runat="server" BackColor="White" Width="30%" Height="70%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
              
                     <table border="0" style="width:100%;padding-left:10px">
                         <tr>
                             <td colspan="2" >
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099; text-align:center">Edit Revenue Distribution</h4>
                             </td>
                         </tr>
                          <tr>
               <td style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';" colspan="2"> 
                      <asp:Label ID="lblMsgContractGroupDistribution" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';" colspan="2"> 
                      <asp:Label ID="lblAlertContractGroupDistribution" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
                 
                         
               <tr>
               <td style="width:25%;text-align:right;font-size:14px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="Label2" runat="server"  Text="Industry :" ForeColor="Black" ></asp:Label>
                      </td> 
                   <td style="width:85%;text-align:left;font-size:14px;font-weight:bold;font-family:'Calibri'; ">
                       <asp:Label ID="lblIndustry" runat="server" ForeColor="Black" ></asp:Label>
                   </td>
            </tr>
                          
                              <tr>
                                  <td style="width:25%;text-align:right;font-size:14px;font-weight:bold;font-family:'Calibri';">
                                      &nbsp;</td>
                                  <td style="width:85%;text-align:left;font-size:14px;font-weight:bold;font-family:'Calibri'; ">
                                      <asp:Label ID="lblIndustryDescription" runat="server" ForeColor="Black"></asp:Label>
                                  </td>
                         </tr>
                          
                              <tr>
                                  <td style="width:30%;text-align:right;font-size:14px;font-weight:bold;font-family:'Calibri';">
                                      <asp:Label ID="Label24" runat="server" ForeColor="Black" Text="Contract Group "></asp:Label>
                                      </td>
                                  <td style="width:85%;text-align:left;font-size:14px;font-weight:bold;font-family:'Calibri'; ">
                                      <asp:DropDownList ID="ddlContractGroup" runat="server" AppendDataBoundItems="True" Height="20px" Width="70%" AutoPostBack="True"><asp:ListItem>--SELECT--</asp:ListItem></asp:DropDownList>
                                  </td>
                         </tr>
                          
                              <tr style="width:95%">
                                  <td style="font-size:15px;font-weight:bold;font-family:Calibri; text-align:left; padding-left:1%; width:60% " colspan="2">
                                      <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                          <ContentTemplate>
                                              <asp:GridView ID="grvContractGroupDistributionDetails" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="1" CssClass="table_head_bdr " GridLines="None" Height="17px" onpageindexchanging="grvContractGroupDistributionDetails_PageIndexChanging" onrowdatabound="grvContractGroupDistributionDetails_RowDataBound" OnRowDeleting="grvContractGroupDistributionDetails_RowDeleting" ShowFooter="True" Style="text-align: left" Width="50%">
                                                  <Columns>
                                                      <asp:TemplateField HeaderText="Contract Group">
                                                          <ItemTemplate>
                                                              <asp:DropDownList ID="ddlContractGroupContractGroupDistributionGV" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataTextField="Descrip1" DataValueField="Descrip1" Height="19px" onselectedindexchanged="ddlContractGroupDistributionGV_SelectedIndexChanged" width="250px">
                                                                  <asp:ListItem Text="--SELECT--" Value="-1" />
                                                              </asp:DropDownList>
                                                          </ItemTemplate>
                                                      </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Percentage">
                                                          <ItemTemplate>
                                                              <asp:TextBox ID="txtPercContractGroupDistributionGV" runat="server" Enabled="true" Height="17px" ReadOnly="false" AutoPostBack="True" OnTextChanged="txtPercContractGroupDistributionGV_TextChanged" Style="text-align:center" Width="75px"></asp:TextBox>
                                                          </ItemTemplate>
                                                      </asp:TemplateField>
                                                      <asp:TemplateField>
                                                          <ItemTemplate>
                                                              <asp:TextBox ID="txtIndustryContractGroupDistributionGV" runat="server" Height="15px" Visible="false" Width="5px"></asp:TextBox>
                                                          </ItemTemplate>
                                                      </asp:TemplateField>
                                                      <asp:TemplateField>
                                                          <ItemTemplate>
                                                              <asp:TextBox ID="txtRcnoContractGroupDistributionGV" runat="server" Height="15px" Visible="false" Width="5px"></asp:TextBox>
                                                          </ItemTemplate>
                                                      </asp:TemplateField>
                                                      <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Images/delete_icon_color.gif" ShowDeleteButton="True">
                                                      <FooterStyle VerticalAlign="Top" />
                                                      <ItemStyle Height="15px" />
                                                      </asp:CommandField>
                                                      <asp:TemplateField>
                                                          <FooterStyle HorizontalAlign="Left" />
                                                          <FooterTemplate>
                                                              <asp:Button ID="btnAddDetailContractGroupDistribution" runat="server" OnClick="btnAddDetailContractGroupDistribution_Click" Text="Add New Row" ValidationGroup="VGroup" Visible="false" />
                                                          </FooterTemplate>
                                                          <ItemStyle ForeColor="#507CD1" />
                                                      </asp:TemplateField>
                                                  </Columns>
                                                  <FooterStyle BackColor="#F7DFB5" Font-Bold="True" ForeColor="White" Height="5px" />
                                                  <RowStyle BackColor="#EFF3FB" Height="17px" />
                                                  <EditRowStyle BackColor="#2461BF" />
                                                  <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                  <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                  <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" />
                                                  <AlternatingRowStyle BackColor="White" />
                                              </asp:GridView>
                                          </ContentTemplate>
                                         <Triggers>
                                              <asp:AsyncPostBackTrigger ControlID="grvContractGroupDistributionDetails" EventName="SelectedIndexChanged" />
                                          </Triggers>
                                      </asp:UpdatePanel>
                                      </td>
                              </tr>
                        
                 
                       

                            <tr style="padding-top:40px;">
                             <td class="CellFormat" style="text-align:right; width:25%" colspan="1">Total
                                </td>

                              
                                <td style="text-align:center;width:37%"><asp:TextBox ID="txtTotalPercent" runat="server" Height="16px" MaxLength="150" Enabled="false" style="text-align:right" Width="40%"></asp:TextBox></td>
                             
              
                
                         </tr>
                          
                

                         <tr style="padding-top:40px;">
                             <td style="text-align:center" colspan="2">
                                 <asp:Button ID="btnSaveContractGroupDistribution" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClientClick="currentdatetime()" Text="Save" Width="120px" />
                                 <asp:Button ID="btnCancelContractGroupDistribution" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" Text="Cancel" Width="100px" />
                             </td>
                         </tr>
                          
                 

        </table>
           </asp:Panel>

          
              
               <asp:ModalPopupExtender ID="mdlContractGroupDistribution" runat="server" CancelControlID="btnCancelContractGroupDistribution" PopupControlID="pnlContractGroupDistribution" TargetControlID="btnDummyContractGroupDistribution" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
        <asp:Button ID="btnDummyContractGroupDistribution" runat="server" cssclass="dummybutton" />
  
             </ContentTemplate>
         </asp:UpdatePanel>
        <%-- end:Contract Group Distribution --%>

               
    
        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
        
     <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtNoofContractDistribution" runat="server" width="1px" Enabled="false" BorderStyle="None" CssClass="dummybutton"></asp:TextBox>
               <asp:TextBox ID="txtDuplicateTarget" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox>            
             <asp:TextBox ID="txtRecordAdded" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox>            
     <asp:TextBox ID="txtRecordDeleted" runat="server" AutoCompleteType="Disabled" Height="16px" Visible="False" Width="35%"></asp:TextBox>            
 
      </asp:Content>