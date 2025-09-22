<%@ Page Title="Service Frequency Master" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_BillingFrequency.aspx.vb" Inherits="Master_BillingFrequency"  Culture="en-GB" %>

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
       <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Billing Frequency</h3>
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
              
                <td style="width:50%;text-align:left;">
                  
                     <asp:Button ID="btnADD" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" Enabled="False" Visible="False" />
<asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Enabled="False" />
<asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()" Enabled="False" Visible="False"/>
                
                        <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" />
            
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
  <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Rcno" Font-Size="15px" AllowPaging="True" HorizontalAlign="Center" AllowSorting="True" Width="600px">
            <Columns>
                 <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True" >
                <ControlStyle Width="40px" />
                <ItemStyle Width="40px" />
                </asp:CommandField>
                <asp:BoundField DataField="Frequency" HeaderText="Frequency" SortExpression="Frequency">
                </asp:BoundField>
               
               
                <asp:BoundField DataField="NoService" HeaderText="No of Service" SortExpression="NoService" Visible="false" />
                <asp:BoundField DataField="NoDays" HeaderText="No of Days" SortExpression="NoDays" />
                
                 <asp:BoundField DataField="Active" HeaderText="Active" SortExpression="Active" Visible="False" />
                 <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                     <EditItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Eval("Rcno") %>'></asp:Label>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 
                <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" Visible="true" />
                   <asp:BoundField DataField="NoOfWks" HeaderText="No of Weeks" SortExpression="NoOfWks" />
                 <asp:BoundField DataField="NoOfMths" HeaderText="No of Months" SortExpression="NoOfMths" />
                 <asp:BoundField DataField="NoOfYears" HeaderText="No of Years" SortExpression="NoOfYears" />
                 <asp:BoundField DataField="MaxNoDaySvs" HeaderText="Maximum Day Services" SortExpression="MaxNoDaySvs" />

                 <asp:BoundField DataField="MaxNoWeekSvs" HeaderText="Maximum Week Services" SortExpression="MaxNoWeekSvs" />
                 <asp:BoundField DataField="MaxNoSvsInterval" HeaderText="Maximum Service Interval" SortExpression="MaxNoSvsInterval" />
                 <asp:BoundField DataField="ByDOW" HeaderText="By DOW" SortExpression="ByDOW" />
                 <asp:BoundField DataField="DOWByWhichWeek" HeaderText="DOW By Week" SortExpression="DOWByWhichWeek" />
                 <asp:BoundField DataField="MonthByWhichMonth" HeaderText="Month By Month" SortExpression="MonthByWhichMonth" />
                 <asp:BoundField DataField="ByDate" HeaderText="By Date" SortExpression="ByDate" />
                 <asp:BoundField DataField="FreqMtd" HeaderText="Frequency Method" SortExpression="FreqMtd" />
                 
                 <asp:BoundField DataField="AutoCalculateServiceValue" HeaderText="Auto Calculate Service Value" />
                 
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
              <td colspan="2">
                   <table style="width:100%;text-align:center;">
                       
                        <tr><td style="width:20%"></td>
                            <td style="width:20%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">Service Frequency
                            </td>
                            <td colspan="3" style="text-align:left;">
                                <asp:TextBox ID="txtFrequency" runat="server" MaxLength="50" Height="16px" Width="99%"></asp:TextBox></td>
                           <td style="width:20%"></td>
                        </tr>
                          <tr><td style="width:20%"></td>
                            <td style="width:20%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;&nbsp;No of Days</td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtNoofDays" runat="server" MaxLength="50" Height="16px" Width="150px"></asp:TextBox></td>
                              <td style="width:20%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;&nbsp;No of Weeks</td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtNoofWeeks" runat="server" MaxLength="50" Height="16px" Width="150px"></asp:TextBox></td>
                              <td style="width:20%"></td>
                        </tr>
                         <tr><td style="width:20%"></td>
                            <td style="width:20%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;&nbsp;No of Months</td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtNoofMonths" runat="server" MaxLength="50" Height="16px" Width="150px"></asp:TextBox></td>
                              <td style="width:20%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;&nbsp;No of Years</td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtNoofYears" runat="server" MaxLength="50" Height="16px" Width="150px"></asp:TextBox></td>
<td style="width:20%"></td>
                        </tr>
                         <tr><td style="width:20%"></td>
                            <td style="width:20%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;&nbsp;Max no of Day Services</td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtMaxDaySvc" runat="server" MaxLength="50" Height="16px" Width="150px"></asp:TextBox></td>
                              <td style="width:20%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;&nbsp;Max no of Week Services</td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtMaxWeekSvc" runat="server" MaxLength="50" Height="16px" Width="150px"></asp:TextBox></td>
                             <td style="width:20%"></td>
                        </tr>
                         <tr><td></td>
                            <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;&nbsp;Max no of Service Interval</td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtMaxSvcInterval" runat="server" MaxLength="50" Height="16px" Width="150px"></asp:TextBox></td>
                              <td style="font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;&nbsp;Frequency Method</td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtFrequencyMethod" runat="server" MaxLength="50" Height="16px" Width="150px"></asp:TextBox></td>
                             <td></td>
                        </tr>
                         <tr><td style="width:20%"></td>
                            <td style="width:20%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;&nbsp;By DOW</td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtDOW" runat="server" MaxLength="50" Height="16px" Width="150px"></asp:TextBox></td>
                              <td style="width:20%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;&nbsp;DOW by which week</td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtDOWWeek" runat="server" MaxLength="50" Height="16px" Width="150px"></asp:TextBox></td>
                             <td style="width:20%"></td>
                        </tr>
                         <tr><td style="width:20%"></td>
                            <td style="width:20%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;&nbsp;Month by which month</td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtMonthByMonth" runat="server" MaxLength="50" Height="16px" Width="150px"></asp:TextBox></td>
                              <td style="width:20%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;&nbsp;By Date</td>
                            <td style="text-align:left;">
                                <asp:TextBox ID="txtByDate" runat="server" MaxLength="50" Height="16px" Width="150px"></asp:TextBox></td>
                             <td style="width:20%"></td>
                        </tr>
                       <%--  <tr style="padding-top:3px;"><td style="width:20%"></td>
                            <td style="width:20%;text-align:left;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;">Description</td>
                             <td style="text-align:left;">
                                <asp:TextBox ID="txtDescription" runat="server" MaxLength="100" Height="16px" Width="200px"></asp:TextBox></td>
                        </tr>--%>
                         <tr><td style="width:20%">&nbsp;</td>
                            <td style="width:20%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:right;">&nbsp;</td>
                            <td style="width:20%;font-size:15px;font-weight:bold;font-family:'Calibri';color:black;text-align:left;" colspan="3">
                                <asp:CheckBox ID="chkAutoCalculateServiceValue" runat="server" Text="Auto-calculate Service Value based on Schedule of Rate" />
                             </td>
                             <td style="width:20%">&nbsp;</td>
                        </tr>
                       </table>
              </td>
          </tr>
           <tr>
               <td colspan="2" style="text-align:right">    <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/>

<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" Visible="False" /></td>
           </tr>
          </table>
    
        <table style="width:100%;text-align:center;">
           
            <tr style="text-align:center;">
                <td style="text-align:center;">
                    
                </td>

            </tr>
             <tr>
                <td><br /></td>
            </tr>
            <tr style="text-align:center;">
                <td style="width:100%;text-align:center">
                   
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#000066" Font-Bold="True" ForeColor="White" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle" />
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
       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblservicefrequency where rcno<>0"></asp:SqlDataSource>

       
        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
        
                    <asp:TextBox ID="txtActive" runat="server" Visible="False"></asp:TextBox>
          <asp:TextBox ID="txtExists" runat="server" Visible="False"></asp:TextBox>
    </div>
 
</asp:Content>

