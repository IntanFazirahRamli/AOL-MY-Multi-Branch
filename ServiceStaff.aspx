<%@ Page Language="VB"   MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ServiceStaff.aspx.vb" Inherits="ServiceStaff" Title="Service Staff"  Culture="en-GB"%>

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
    
    <div>
     <div style="text-align:center">
         <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
               <ControlBundles>
     
            <asp:controlBundle name="ListSearchExtender_Bundle"/>
   </ControlBundles>
         </asp:ToolkitScriptManager>
            <asp:ListSearchExtender ID="ddllsID" runat="server" TargetControlID="ddlID" PromptPosition="Bottom"></asp:ListSearchExtender>
   
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Service Staff</h3>
       
        <table style="width:100%;text-align:center;">
               <tr><td colspan="2">
               <asp:Button ID="btnAdd" runat="server" Font-Bold="True" Text="ADD" Width="100px"  CssClass="button" BackColor="#CFC6C0" Visible="TRUE" />
&nbsp;<asp:Button ID="btnSave" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()" />
&nbsp;<asp:Button ID="btnCancel" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="RESET" Width="100px" Visible="true" />
                      &nbsp;<asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" /></td>
               
            </tr>
           <tr>
              <td colspan="2" style="text-align:right;color:brown;font-size:15px;font-weight:bold;font-family:'Calibri';"> <asp:Label ID="txtMode" runat="server" Text=""></asp:Label>
</td>
            </tr>
            <tr>
             <td style="width:150px;padding-left:250px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">ID
                             </td>
                           <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                          
                                 <asp:DropDownList ID="ddlID" runat="server" AppendDataBoundItems="true" DataSourceID="SqlDSStaff" DataTextField="IDNAME" DataValueField="StaffId" Width="250px">
                                   <asp:ListItem Text="--SELECT--" Value="-1" />
                                      </asp:DropDownList>
                          
                                 <asp:TextBox ID="txtRecordNo" runat="server" MaxLength="10" Height="16px" Width="200px" Visible="false"></asp:TextBox>
                               </td>
                         </tr>
             <tr>
             <td style="width:150px;padding-left:250px;font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">Cost Value
                             </td>
                           <td colspan="1" style="font-size:14px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;">
                          
                            
                                 <asp:TextBox ID="txtCostValue" runat="server" MaxLength="10" Height="16px" Width="245px" Visible="true"></asp:TextBox>
                               </td>
                         </tr>
             <tr><td colspan="2"><br/></td></tr>
            <tr style="padding-top:20px;">
                <td colspan="2">
                    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDSServiceStaff" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="12pt" HorizontalAlign="Center" AllowSorting="True">
                        <Columns>
                               <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                        <ControlStyle Width="50px" />
                        <ItemStyle Width="50px" />
                    </asp:CommandField>
                            <asp:BoundField DataField="StaffID" HeaderText="StaffID" SortExpression="StaffID" >
                               <HeaderStyle Width="150px" />
                               <ItemStyle Width="150px" Wrap="False" />
                               </asp:BoundField>
                            <asp:BoundField DataField="StaffName" HeaderText="StaffName" SortExpression="StaffName" >
                               <HeaderStyle Width="250px" />
                               <ItemStyle Width="250px" Wrap="False" />
                               </asp:BoundField>
                            <asp:BoundField DataField="CostValue" HeaderText="CostValue" SortExpression="CostValue" >
                               <ItemStyle Width="100px" />
                               </asp:BoundField>
                            <asp:BoundField DataField="RecordNo" HeaderText="RecordNo" SortExpression="RecordNo" Visible="false" />
                               <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                   <EditItemTemplate>
                                       <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                                   </EditItemTemplate>
                                   <ItemTemplate>
                                       <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                   </ItemTemplate>
                               </asp:TemplateField>
                            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="false"/>
                            <asp:BoundField DataField="CreatedOn" HeaderText="CreatedOn" SortExpression="CreatedOn" Visible="false"/>
                            <asp:BoundField DataField="LastModifiedBy" HeaderText="LastModifiedBy" SortExpression="LastModifiedBy" Visible="false"/>
                            <asp:BoundField DataField="LastModifiedOn" HeaderText="LastModifiedOn" SortExpression="LastModifiedOn" Visible="false"/>
                            <asp:BoundField DataField="CreateDeviceID" HeaderText="CreateDeviceID" SortExpression="CreateDeviceID" Visible="false"/>
                            <asp:BoundField DataField="CreateSource" HeaderText="CreateSource" SortExpression="CreateSource" Visible="false"/>
                            <asp:BoundField DataField="FlowFrom" HeaderText="FlowFrom" SortExpression="FlowFrom" Visible="false"/>
                            <asp:BoundField DataField="FlowTo" HeaderText="FlowTo" SortExpression="FlowTo" Visible="false"/>
                            <asp:BoundField DataField="EditSource" HeaderText="EditSource" SortExpression="EditSource" Visible="false"/>
                            <asp:BoundField DataField="DeleteStatus" HeaderText="DeleteStatus" SortExpression="DeleteStatus" Visible="false"/>
                            <asp:BoundField DataField="LastEditDevice" HeaderText="LastEditDevice" SortExpression="LastEditDevice" Visible="false"/>
                        </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#E4E4E4" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#e4e4e4" />
            <SortedAscendingHeaderStyle BackColor="#000066" />
            <SortedDescendingCellStyle BackColor="#e4e4e4" />
            <SortedDescendingHeaderStyle BackColor="#000066" />
        </asp:GridView>
                </td>
            </tr>
            </table>
         <asp:SqlDataSource ID="SqlDSServiceStaff" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                SelectCommand="SELECT * from tblservicerecordstaff where (Rcno &lt;&gt; 0) order by staffid"  FilterExpression="RecordNo = '{0}'">
               <FilterParameters>
                    <asp:ControlParameter Name="RecordNo" ControlID="txtRecordNo" PropertyName="Text" Type="String" />
                </FilterParameters>
         </asp:SqlDataSource>
           <asp:SqlDataSource ID="SqlDSStaff" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT StaffId, Name, CONCAT(StaffId, ' [', Name, ']') AS IDNAME FROM tblstaff ORDER BY StaffId"></asp:SqlDataSource>
           
    </div>

         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="txtQuery" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="txtSvcRcno" runat="server" Visible="False"></asp:TextBox>
   </asp:Content>
