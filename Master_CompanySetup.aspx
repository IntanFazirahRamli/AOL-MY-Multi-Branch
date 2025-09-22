<%@ Page Title="Company Setup" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_CompanySetup.aspx.vb" Inherits="Master_CompanySetup" %>
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
       
   </ControlBundles>
    </asp:ToolkitScriptManager>
    <div style="text-align:center">
       
        <h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">Company Setup</h3>
    
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
                  
                     <asp:Button ID="btnADD" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="ADD" Width="100px" Visible="False" />
<asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="True" />
<asp:Button ID="btnDelete" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="DELETE" Width="100px" OnClientClick = "Confirm(); currentdatetime()" Visible="False" />
                
                        <asp:Button ID="btnPrint" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="PRINT" Width="100px" Visible="False" />
            
                       </td>
                <td style="text-align: right">
            
                    <asp:Button ID="btnQuit" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="100px" />

                </td>
                
            </tr>
                  <tr>
              <td colspan="2">
               <br /></td>
            </tr>
        
            <tr>
                
                  <td colspan="2">

                         <table class="Centered" style="border: 1px solid #808080; text-align:right; width:80%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">COMPANY INFORMATION</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table style="width:100%;text-align:center;padding-left:10px;padding-top:10px;">
                                     <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Company Name</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtCompanyName" runat="server" width="350px" MaxLength="500"></asp:TextBox>

                                      </td></tr>

                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Office Address</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtOfficeAddress" runat="server" width="350px" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>

                                      </td></tr>

                                   <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Business Registration Number</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtBusinessRegNumber" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                      </td></tr>

                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">GST Number</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtGSTNumber" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                      </td></tr>

                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Telephone Number</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtTelNumber" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                      </td></tr>

                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Fax Number</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtFaxNumber" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                      </td></tr>
                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Website</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtWebsite" runat="server" width="350px" MaxLength="200"></asp:TextBox>

                                      </td></tr>

                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Email</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtEmail" runat="server" width="350px" MaxLength="200"></asp:TextBox>

                                      </td></tr>

                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">BillingEmail</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtInvoiceEmail" runat="server" width="350px" MaxLength="200"></asp:TextBox>

                                      </td></tr>

                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Mobile</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtMobile" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                      </td></tr>
                                  

                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Bank Name</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtBankName" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                      </td></tr>
                                  

                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Bank Code</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtBankCode" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                      </td></tr>
                                  

                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Branch Code</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtBranchCode" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                      </td></tr>
                                  

                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Account Name</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtAccountName" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                      </td></tr>
                                  

                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Account Code</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtAccountCode" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                      </td></tr>
                                  

                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">SWIFT Code</td>
                                <td class="CellTextBoxADM" colspan="1">
                                       <asp:TextBox ID="txtSWIFTCode" runat="server" width="350px" MaxLength="100"></asp:TextBox>

                                      </td></tr>
                                    <tr><td class="CellFormatADM" style="padding-left:50px;width:40%;text-align:left">Logo</td>
                                <td class="CellTextBoxADM" colspan="1">
                                      <asp:FileUpload ID="FileUpload1" runat="server" />
                                      </td></tr>
                                  <tr>
                                      <td></td>
                                         <td style="padding-left:50px;text-align:left;">
                                  
                  
                                    <asp:Panel ID="Panel1" runat="server" Height="95%" BorderStyle="Solid" BorderColor="Silver" BorderWidth="1px" Width="60%" HorizontalAlign="Center">
                                <asp:Image ID="Image2" runat="server" Width="60%" Height="100%" />

                                        </asp:Panel></td>
                                  </tr>

                              </table></td></tr>
                      </table><br />

                       
  
                  </td>
                 </tr>
             
           <tr>
               <td colspan="2" style="text-align:right">    <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/>

<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td>
           </tr>
          </table>
       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblcompanyinfo where rcno=1"></asp:SqlDataSource>

       
        
         <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
        
    </div>
 

</asp:Content>



