<%@ Page Language="VB"  MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ServiceDetails.aspx.vb" Inherits="ServiceDetails" Title="Service Details"  Culture="en-GB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
            <ControlBundles>
                <asp:ControlBundle Name="CalendarExtender_Bundle" />
                <asp:controlBundle name="ModalPopupExtender_Bundle"/>
            </ControlBundles>
        </asp:ToolkitScriptManager>
     <div style="text-align:center">
         
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Service Detail</h3>
       
        <table style="width:100%;text-align:center;">
            <tr><td>
               <%--<asp:Button ID="btnSaveAdd" runat="server" Font-Bold="True" Text="SAVE & ADD" Width="100px"  CssClass="button" BackColor="#CFC6C0" Visible="TRUE" />--%>
&nbsp;<asp:Button ID="btnSave" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" />
&nbsp;<asp:Button ID="btnCancel" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="RESET" Width="100px" Visible="false" />
                      &nbsp;<asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" /></td>
                
            </tr>
        
                 <tr style="text-align:center;width:100%">
                <td style="text-align:center;padding-left:100px;">
  <asp:Panel ID="Panel5" runat="server" BackColor="#F8F8F8" BorderColor="gray" BorderWidth="2" BorderStyle="Solid" Width="900px" Height="600px" HorizontalAlign="Center">
                       <table style="width:100%;text-align:left;padding-left:10px;padding-top:10px;">
                                
                           <tr>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">RecordNo
                             </td>
                           <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                            <asp:TextBox ID="txtRecordNo" runat="server" MaxLength="10" Height="16px" Width="200px"></asp:TextBox>
                               </td>
                                <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Status
                                
                             </td>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="205px">
                                     <asp:ListItem>--SELECT--</asp:ListItem>
                                    <asp:ListItem Value="O">O - Service not done</asp:ListItem>
                                   <asp:ListItem Value="C">C - Cancelled Job</asp:ListItem>
                                    <asp:ListItem Value="H">H - Non-payable Job</asp:ListItem>                                  
                                    <asp:ListItem Value="P">P - Service done</asp:ListItem>
                                     <asp:ListItem Value="T">T - Stopped Job</asp:ListItem>                                 
                                     <asp:ListItem Value="V">V - Service record deleted</asp:ListItem>
                                     <asp:ListItem Value="B">B - Job cannot complete</asp:ListItem> 
                               </asp:DropDownList>
                            </td>
                                <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                            JobOrder
                               </td>
                               
                                <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                             <asp:TextBox ID="txtJobOrder" runat="server" MaxLength="3" Height="16px" Width="200px"></asp:TextBox> 
                                </td>                          
                            
                        </tr>

                            <tr>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">JobDate
                             </td>
                           <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                            <asp:TextBox ID="txtJobDate" runat="server" MaxLength="10" Height="16px" Width="200px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtJobDate" TargetControlID="txtJobDate" Format="dd/MM/yyyy"></asp:CalendarExtender> </td>
                          
                                <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">JobContact
                                
                             </td>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                               <asp:TextBox ID="txtJobContact" runat="server" MaxLength="10" Height="16px" Width="200px"></asp:TextBox>
                           
                            </td>
                                <td rowspan="5" colspan="2" style="vertical-align:top;">
                                          <asp:Panel ID="Panel3" runat="server" Height="160px" BorderStyle="Solid" BorderColor="Silver" BorderWidth="2px" Width="280px">
                                     <table>
                                         <tr>
                                             <td colspan="2" style="font-size: 12px; font-weight: bold; font-family: 'Calibri'; color: maroon; text-align:center; width: 150px"> Time in Mins</td>
                                         </tr>
                                         <tr>
                                             <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;padding-left:10px">Allocated Time&nbsp;&nbsp;</td>
                                             <td> <asp:TextBox ID="txtAllocatedTime" runat="server" MaxLength="18" Height="16px" Width="100px"></asp:TextBox></td>
                                         </tr>
                                          <tr>
                                             <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;padding-left:10px">Service Time&nbsp;&nbsp;</td>
                                             <td> <asp:TextBox ID="txtSvcTime" runat="server" MaxLength="18" Height="16px" Width="100px"></asp:TextBox></td>
                                         </tr>
                                          <tr>
                                             <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;padding-left:10px">Contract Time&nbsp;&nbsp;</td>
                                             <td><asp:TextBox ID="txtContractTime" runat="server" MaxLength="18" Height="16px" Width="100px"></asp:TextBox></td>
                                         </tr>
                                          <tr>
                                             <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;padding-left:10px">Program Time&nbsp;&nbsp;</td>
                                             <td><asp:TextBox ID="txtProgTime" runat="server" MaxLength="18" Height="16px" Width="100px"></asp:TextBox></td>
                                         </tr>
                                          <tr>
                                             <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;padding-left:10px">Total Duration&nbsp;&nbsp;</td>
                                             <td><asp:TextBox ID="txtTotDuration" runat="server" MaxLength="18" Height="16px" Width="100px"></asp:TextBox></td>
                                         </tr>
                                     </table>       
                                     
                                     </asp:Panel>
                                     </td>
                                        </tr>                      
                            
                           <tr>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">ProspectID
                             </td>
                           <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                            <asp:TextBox ID="txtProspectID" runat="server" MaxLength="10" Height="16px" Width="200px"></asp:TextBox>
                               </td>
                               
                                <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                            ContractNo
                               </td>
                               
                                <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                             <asp:TextBox ID="txtContractNo" runat="server" MaxLength="3" Height="16px" Width="200px"></asp:TextBox> 
                                </td>                          
                            
                        </tr>

                             <tr>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">TargetID
                             </td>
                           <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                            <asp:TextBox ID="txtTargetID" runat="server" MaxLength="10" Height="16px" Width="170px"></asp:TextBox>
                                 <asp:ImageButton ID="btnTarget" runat="server" CssClass="" ImageUrl="~/Images/searchbutton.jpg" Height="22" Width="24" />
                                <asp:ModalPopupExtender ID="mdlPopUpTarget" runat="server" CancelControlID="btnPnlTargetClose" PopupControlID="pnlPopUpTarget"
                                    TargetControlID="btnTarget" BackgroundCssClass="modalBackground">
                                </asp:ModalPopupExtender>

                               </td>
                               
                                <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                            Project
                               </td>
                               
                                <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                             <asp:TextBox ID="txtProject" runat="server" MaxLength="3" Height="16px" Width="200px"></asp:TextBox> 
                                </td>                          
                            
                        </tr>
                           <tr>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Location
                             </td>
                           <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                            <asp:TextBox ID="txtLocation" runat="server" MaxLength="10" Height="16px" Width="200px"></asp:TextBox>
                               </td>
                               
                                <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                            BranchID
                               </td>
                               
                                <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                             <asp:TextBox ID="txtBranchID" runat="server" MaxLength="3" Height="16px" Width="200px"></asp:TextBox> 
                                </td>                          
                            
                        </tr>
                            <tr>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">MainJobNo
                             </td>
                           <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                            <asp:TextBox ID="txtMainJobNo" runat="server" MaxLength="10" Height="16px" Width="200px"></asp:TextBox>
                               </td>
                               
                                <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                           
                               </td>
                               
                                <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                 </td>                          
                            
                        </tr>
                           <tr>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Services
                             </td>
                           <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                            <asp:TextBox ID="txtServices" runat="server" MaxLength="10" Height="16px" Width="170px"></asp:TextBox>
                                <asp:ImageButton ID="btnServices" runat="server" CssClass="" ImageUrl="~/Images/searchbutton.jpg" Height="22" Width="24" />
                                <asp:ModalPopupExtender ID="mdlPopupServices" runat="server" CancelControlID="btnPnlServicesClose" PopupControlID="pnlPopUpServices"
                                    TargetControlID="btnServices" BackgroundCssClass="modalBackground">
                                </asp:ModalPopupExtender>
                               </td>
                                 <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                           Description
                               </td>
                                <td colspan="3" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                             <asp:TextBox ID="txtDescription" runat="server" MaxLength="3" Height="16px" Width="490px"></asp:TextBox> 
                                </td>                          
                            
                        </tr>
                            <tr>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Warranty
                             </td>
                          
                                
                                <td colspan="5" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                             <asp:TextBox ID="txtWarranty" runat="server" MaxLength="3" Height="60px" Width="790px" TextMode="MultiLine"></asp:TextBox> 
                                </td>                          
                            
                        </tr>
                            <tr>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Fault / Reason / Purpose
                             </td>
                          
                                
                                <td colspan="5" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                             <asp:TextBox ID="txtFault" runat="server" MaxLength="3" Height="60px" Width="790px" TextMode="MultiLine"></asp:TextBox> 
                                </td>                          
                            
                        </tr>
                            <tr>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Action / Service Performed
                             </td>
                          
                                
                                <td colspan="5" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                             <asp:TextBox ID="txtAction" runat="server" MaxLength="3" Height="60px" Width="790px" TextMode="MultiLine"></asp:TextBox> 
                                </td>                          
                            
                        </tr>
                            <tr>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Remark
                             </td>
                          
                                
                                <td colspan="5" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                                    <table>
                                        <tr>
                                            <td>To Office<br /> <asp:TextBox ID="txtRemarkOffice" runat="server" MaxLength="3" Height="60px" Width="390px" TextMode="MultiLine"></asp:TextBox> </td>
                                            <td>To Client<br /> <asp:TextBox ID="txtRemarkClient" runat="server" MaxLength="3" Height="60px" Width="390px" TextMode="MultiLine"></asp:TextBox> </td>
                                        </tr>
                                    </table>
                            
                                </td>                          
                            
                        </tr>
                          <tr>
                            <td style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">ServiceCost
                             </td>
                           <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                            <asp:TextBox ID="txtServiceCost" runat="server" MaxLength="10" Height="16px" Width="200px"></asp:TextBox>
                               </td>
                              </tr>
</table>
        </asp:Panel></td>
                     </tr>
            </table>   
           <asp:Panel ID="pnlPopUpTarget" runat="server" BackColor="White" Width="700px" Height="500px" BorderColor="#003366" BorderWidth="1" Visible="true"
        HorizontalAlign="Left" ScrollBars="None">

         <table><tr>
                               <td style="text-align:center;"><h4 style="color: #000000">Select Target</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlTeamClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                                
                           </table>
        
             <asp:panel runat="server" ScrollBars="Vertical" Width="620px" Height="350px" HorizontalAlign="Center">
            <asp:GridView ID="gvTarget" runat="server" DataSourceID="SqlDSTarget" ForeColor="#333333" AutoGenerateColumns="False"
                CellPadding="2" GridLines="None" Width="600px" RowStyle-HorizontalAlign="Left" DataKeyNames="Rcno">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <EditItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle Width="40px" />
                        <ItemStyle Width="40px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="TargetID" HeaderText="TargetID" SortExpression="TargetID" >
                    <ControlStyle Width="100px" />
                    <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Descrip1" HeaderText="Descrip1" SortExpression="Descrip1" >
                    <HeaderStyle Width="200px" />
                    <ItemStyle Width="200px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="False" />
                    <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="False"/>
                    <asp:BoundField DataField="LastModifiedBy" HeaderText="LastModifiedBy" SortExpression="LastModifiedBy" Visible="False"/>
                    <asp:BoundField DataField="LastModifiedOn" HeaderText="LastModifiedOn" SortExpression="LastModifiedOn" Visible="False"/>
                    <asp:BoundField DataField="WS" HeaderText="WS" SortExpression="WS" Visible="False"/>
                    <asp:BoundField DataField="WTS" HeaderText="WTS" SortExpression="WTS" Visible="False"/>
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" Visible="False"/>
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
            </asp:GridView></asp:panel>
             <br />
             <div style="text-align:center;">
                 <asp:Button ID="btnOk" runat="server" Text="Ok" CssClass="button" Font-Bold="True" />
             <asp:Button ID="btnBack" runat="server" Text="Cancel" CssClass="button" Font-Bold="True" />
             </div>
             
            <asp:SqlDataSource ID="SqlDSTarget" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT distinct * from tblTarget where (Rcno &lt;&gt; 0) order by TargetID"></asp:SqlDataSource>
       
    </asp:Panel>
          <asp:Panel ID="pnlPopupServices" runat="server" BackColor="White" Width="700px" Height="500px" BorderColor="#003366" BorderWidth="1" Visible="true"
        HorizontalAlign="Left" ScrollBars="None">

         <table><tr>
                               <td style="text-align:center;"><h4 style="color: #000000">Select Product</h4> </td>
                               <td style="width:1%;text-align:right;"><asp:ImageButton ID="pnlServicesClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr>
                             <tr>
                                 <td style="padding-left:25px;">
             <asp:panel ID="pnl" runat="server" ScrollBars="Vertical" Width="620px" Height="350px" HorizontalAlign="Center">
            <asp:GridView ID="GridView2" runat="server" DataSourceID="SqlDSService" ForeColor="#333333" AutoGenerateColumns="False"
                CellPadding="2" GridLines="None" Width="600px" RowStyle-HorizontalAlign="Left" DataKeyNames="Rcno">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" >
                <ControlStyle Width="40px" />
                <ItemStyle Width="40px" />
                </asp:CommandField>
                    <asp:BoundField DataField="ProductID" HeaderText="ProductID" SortExpression="ProductID" >
                    <ControlStyle Width="150px" />
                    <ItemStyle Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ProductDesc" HeaderText="ProductDesc" SortExpression="ProductDesc" >
                    <HeaderStyle Width="250px" />
                    <ItemStyle Width="250px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:BoundField DataField="EstimatedValue" HeaderText="EstimatedValue" SortExpression="EstimatedValue" Visible="False"/>
                    <asp:BoundField DataField="Target" HeaderText="Target" SortExpression="Target" Visible="False"/>
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
            </asp:GridView></asp:panel>
                                     </td>
                             </tr>
             </table>
             <br />
             <div style="text-align:center;">
                 <asp:Button ID="btnServicesOk" runat="server" Text="Ok" CssClass="button" Font-Bold="True" />
             <asp:Button ID="btnServicesCancel" runat="server" Text="Cancel" CssClass="button" Font-Bold="True" />
             </div>
             
            <asp:SqlDataSource ID="SqlDSService" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT distinct * from tblProduct where (Rcno &lt;&gt; 0) order by ProductID"></asp:SqlDataSource>
      
    </asp:Panel>
          <asp:TextBox ID="txtMode" runat="server" Visible="False"></asp:TextBox>

         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtQuery" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtSvcRcno" runat="server" Visible="False"></asp:TextBox>
    </div>
   </asp:Content>
