<%@ Page Title="Contact Module Master Setup" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Master_ContactSetup.aspx.vb" Inherits="Master_ContactSetup" %>

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
       font-size:15px;
      font-weight:bold;
        font-family:'Calibri';
        color:black;
    
    }

         
          </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
             <h3 style="font-family: Verdana, Geneva, Calibri, sans-serif; font-weight: bold;COLOR:#000099;">Contacts Module Setup</h3>
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
                  
<asp:Button ID="btnEdit" runat="server" Font-Bold="True" Text="EDIT" Width="100px"  CssClass="roundbutton1" BackColor="#CFC6C0" Visible="True" />
                
            
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
                
                  <td colspan="1">

                         <table class="Centered" style="border: 1px solid #808080; text-align:left; width:100%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">Corporate</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;">
                              <table border="0" style="width:100%;text-align:left;padding-left:10px;padding-top:10px;">
                                     
                                  
                                     <tr>
                                     <td class="CellFormatADM" ></td>
                                   <td class="CellTextBox" colspan="3">      <asp:CheckBox ID="chkCompRocNos" runat="server" Text="RocNos not allow to be blank" Visible="true" /></td></tr>
                                   
                                     <tr><td class="CellFormatADM" >AR Term </td>
                                   <td colspan="3" >
                                                 <asp:DropDownList ID="txtCompARTerm" runat="server" AppendDataBoundItems="True" Height="25px" Width="61%">
                                <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                                </td></tr>

                                 

                                   <tr><td class="CellFormatADM" >Currency </td>
                            <td colspan="3">               <asp:DropDownList ID="ddlCompCurrency" runat="server" AppendDataBoundItems="True" Height="25px" Width="61%">
                                <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                            </asp:DropDownList></td></tr>

                                    <tr>
                                   <td class="CellFormatADM" >Industry &nbsp; </td>
                       <td colspan="3">      <asp:DropDownList ID="ddlCompIndustry" runat="server" AppendDataBoundItems="True" Height="25px" Width="61%" AutoPostBack="True">
                                <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                            </asp:DropDownList>

                                   </td>

                               </tr>


                                   <tr>
                                   <td class="CellFormatADM" >Market Segment ID   </td>
                     <td colspan="3">      <asp:TextBox ID="txtCompMarketSegmentID" runat="server" width="60%" Visible="True"></asp:TextBox>

                                   </td>

                               </tr>

                                              <tr>
                                   <td class="CellFormatADM" >Default Auto Email Invoice  </td>
                     <td> <asp:CheckBox ID="chkDefaultAutoEmailInvoiceCompany" runat="server" Visible="true" /></td>
<td class="CellFormatADM" style="text-align:left" >Default Auto Email SOA &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkDefaultAutoEmailSOACompany" runat="server" Visible="true" /></td>

                               </tr>
                                    <tr>
                                   <td class="CellFormatADM" >Default Hard Copy Invoice  </td>
                     <td> <asp:CheckBox ID="chkDefaultHardCopyInvoiceCompany" runat="server" Visible="true" /></td>
<td class="CellFormatADM" style="text-align:left" >Default Hard Copy SOA &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkDefaultHardCopySOACompany" runat="server" Visible="true" /></td>

                               </tr>
                                   <tr>
                                   <td class="CellFormatADM" >Postal Code Validation  </td>
                     <td> <asp:CheckBox ID="chkPostalValidateCompany" runat="server" Visible="true" /></td>
<td class="CellFormatADM" style="text-align:left" >Postal Code Format &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPostalFormatCompany" runat="server" MaxLength="6" Width="100px"></asp:TextBox></td>

                               </tr>
                                    <tr><td class="CellFormatADM" colspan="4" style="padding-left:50px;width:0%;text-align:left;">&nbsp;<asp:TextBox ID="txtCompAPTerm" runat="server" width="150px" Visible="False"></asp:TextBox>
                                </td></tr>
                                    
                                    <tr><td class="CellFormatADM" colspan="4" style="padding-left:50px;width:0%;text-align:left;">&nbsp;<asp:TextBox ID="TextBox1" runat="server" width="150px" Visible="False"></asp:TextBox>
                                </td></tr>
                                      <tr>
                                    <td class="CellTextBox" >
                                         <asp:CheckBox ID="chkCompAddedCompany" runat="server" Text="New Added Company with Company Default Ledger" Visible="False" /></td>
                                 
                                      
                                     
                                <td colspan="3">       <asp:CheckBox ID="chkCompNameEAlphabets" runat="server" Text="Name(E) only allow alphabets" Visible="False" /></td></tr>
                       
                                    <tr>
                                    <td class="CellFormatADM" >
                                         <asp:CheckBox ID="chkCompDefaultAutoEmail" runat="server" Text="Default Auto Email Service Record" Visible="False" /></td>
                                 
                                       <td class="CellFormatADM" colspan="3">&nbsp;<asp:TextBox ID="txtCompSvcRecReportFormat" runat="server" width="50px" Visible="False"></asp:TextBox>
                                            </td></tr>
                                  
                                         </table></td></tr>
                      </table>
                          </td>


                   <td colspan="1">

                         <table border="0" class="Centered" style="border: 1px solid #808080; text-align:right; width:100%; border-radius: 25px;padding: 2px; height:60px; background-color: #F3F3F3;">
                             <tr>
                         <td colspan="1" style="font-size:15px;font-weight:bold;font-family:'Calibri';color:#800000; text-align:left; text-decoration: underline;padding-left:5%;">Residential</td>
                    </tr>
                           <tr><td style="text-align:left;width:100%;"> 
                              <table boder="0" style="width:100%;text-align:left;padding-top:10px;">
                                    
                                     <tr>
                                     <td class="CellFormatADM" > </td>
                                   <td class="CellTextBox" colspan="3">       <asp:CheckBox ID="chkPersonNRIC" runat="server" Text="NRIC not allow to be blank" Visible="true" /></td></tr>
                                   
                                     <tr><td class="CellFormatADM" >AR Term </td>
                                 <td colspan="3">          <asp:DropDownList ID="txtPersonARTerm" runat="server" AppendDataBoundItems="True" Height="25px" Width="61%">
                                <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                            </asp:DropDownList></td></tr>

                               <tr>
                                   <td class="CellFormatADM" >Currency </td>
                          <td colspan="3">   <asp:DropDownList ID="ddlPersCurrency" runat="server" AppendDataBoundItems="True" Height="25px" Width="61%">
                                <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                            </asp:DropDownList>

                                   </td>

                               </tr>

                                   <tr>
                                   <td class="CellFormatADM" >Industry </td>
                         <td colspan="3">    <asp:DropDownList ID="ddlPersIndustry" runat="server" AppendDataBoundItems="True" Height="25px" Width="61%" AutoPostBack="True">
                                <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                            </asp:DropDownList>

                                   </td>

                               </tr>

                              <tr>
                                   <td class="CellFormatADM" >Market Segment ID </td>
                    <td colspan="3">       <asp:TextBox ID="txtPersMarketSegmentID" runat="server" width="60%" Visible="True"></asp:TextBox>

                                   </td>

                               </tr>

                                <tr>
                                   <td class="CellFormatADM" style="width:30%" >Default Auto Email Invoice  </td>
                     <td style="width:10%">      

                                   <asp:CheckBox ID="chkDefaultAutoEmailInvoicePerson" runat="server" Visible="true" />      

                                   </td>
<td class="CellFormatADM" style="text-align:LEFT;width:40%" colspan="2">Default Auto Email SOA &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkDefaultAutoEmailSOAPerson" runat="server" Visible="true" /></td>

                               </tr>
                                    <tr>
                                   <td class="CellFormatADM" >Default Hard Copy Invoice  </td>
                     <td> <asp:CheckBox ID="chkDefaultHardCopyInvoicePerson" runat="server" Visible="true" /></td>
<td class="CellFormatADM" style="text-align:LEFT;" colspan="2" >Default Hard Copy SOA &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkDefaultHardCopySOAPerson" runat="server" Visible="true" /></td>

                               </tr>    
                                   <tr>
                                   <td class="CellFormatADM" >Postal Code Validation</td>
                     <td> <asp:CheckBox ID="chkPostalValidatePerson" runat="server" Visible="true" /></td>
<td class="CellFormatADM" style="text-align:LEFT;" colspan="2" >Postal Code Format &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPostalFormatPerson" runat="server" MaxLength="6" Width="100px"></asp:TextBox></td>

                               </tr>     
                                   <tr><td class="CellFormatADM" colspan="4">&nbsp;<asp:TextBox ID="txtPersonAPTerm" runat="server" width="150px" Visible="False"></asp:TextBox>
</td></tr>
                                     <tr>
                                    <td class="CellFormatADM" colspan="4" >
                                         <asp:CheckBox ID="chkPersonDefaultAutoEmail" runat="server" Text="Default Auto Email Service Record" Visible="False" /></td></tr>
                                 
                                       <tr><td class="CellFormatADM" colspan="4" >&nbsp;
                                           <asp:TextBox ID="txtPersonSvcRecReportFormat" runat="server" width="50px" Visible="False"></asp:TextBox>
</td></tr>
                                    
                                      <tr>
                                     <td class="CellFormatADM" colspan="4">
                                         <asp:CheckBox ID="chkPersonNameEAlphabets" runat="server" Text="Name(E) only allow alphabets" Visible="False" /></td></tr>
                                        </table></td></tr>
                      </table><br />
                          </td>
                 </tr>
             
           <tr>
               <td colspan="2" style="text-align:right">    <asp:Button ID="btnSave" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="SAVE" Width="100px" OnClientClick="currentdatetime()"/>

<asp:Button ID="btnCancel" runat="server"  CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="CANCEL" Width="100px" /></td>
           </tr>
          </table>
       
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>" ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>" SelectCommand="SELECT * FROM tblcity where rcno<>0"></asp:SqlDataSource>

      <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
</asp:Content>

