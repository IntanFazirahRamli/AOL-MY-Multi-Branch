<%@ Page Title="Reports" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Reports_New.aspx.vb" Inherits="Reports_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <link type="text/css" rel="stylesheet" href="CSS/ReportmenuTemplate.css" />
      <link href="tabmenu/tabmenu.css" rel="stylesheet" type="text/css" />
    <script src="tabmenu/tabmenu.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">REPORTS</h3>
       
        <table style="width:100%;text-align:center;">
            <tr>
                <td colspan="2"><br /></td>
            </tr>
            <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>  

                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
                      </td> 
            </tr>
          
            <tr>
                <td colspan="2" style="width:100%;text-align:RIGHT;"> 
                 <asp:Button ID="btnQuit" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="90px" Height="30px" />
            </td>
            </tr>
            <tr>
     <td colspan="2" style="text-align:left">
    <ul class="menuTemplate1 decor3_1">
    <li><a href="#CSS-Menu">SERVICE RECORD</a>
         <div class="drop decor3_2" style="width: 600px;">
            <div class='left'>
                <b>Service Record</b>
                <div>
                    <a href="#">1. Service Record Listing</a><br />
                    <a href="#">2. Service Record Detail Listing</a><br />
                    <a href="#">3. Service Record Detail Listing by Client</a><br />
                    <a href="#">4. Service Record Listing by Staff</a><br />
                    <a href="#">5. Service Record Detail Listing by Staff</a><br />
                     <a href="#">6. Scheduled Service Listing</a><br />
                    <a href="#">7. Completed Service Listing</a><br />
                    <a href="#">8. Service Record Printing</a><br />
                    <a href="#">9. Service Record with Duplicate Team Members</a><br />
                    <a href="#">10. Service Record with Different Schedule Date and Actual Service Date</a>
                             
                </div>
            </div>
            <div class='left'>
                <b>Service Team List</b>
                <div>
                    <a href="#">1. Service Team Member Listing</a><br />
                    <a href="#">2. Service Team Vehicle Listing</a><br />
                    <a href="#">3. Service Team Equipment & Tools Listing</a><br />
                          
                </div>
            </div>
       
        </div>
    </li>
    <li class="separator"></li>
    <li><a href="#Horizontal-Menus" class="arrow">SERVICE CONTRACT</a>
        <div class="drop decor1_2 dropToLeft2" style="width: 900px;">
            <div class='left'>
                <b>Service Contract</b>
                <div>
                    <a href="#">1. Service Contract Listing</a><br />
                    <a href="#">2. Service Contract Listing by Incharge</a><br />
                    <a href="#">3. Service Contract Listing by Support Staff</a><br />
                    <a href="#">4. Service Contract Expiry/Renewal Listing</a><br />
                    <a href="#">5. Offset Contract Expiry/Renewal Listing</a><br />
                     <a href="#">6. Service Contract Quote Price Listing</a><br />
                   
                </div>
            </div>
            <div>
                <b>Due for Renewal Service Contract</b>
                <div>
                    <a href="#">1. Due for Renewal Service Contract Listing by Client ID</a><br />
                    <a href="#">2. Due for Renewal Service Contract Listing by Contract Group</a><br />
                    <a href="#">3. Due for Renewal Service Contract Listing by Salesman</a><br />
                  <%-- <br /><br /><br />--%>
                </div>
            </div>
                <div>
                <b>Renewed Service Contract</b>
                <div>
                    <a href="#">1. Renewed Service Contract Listing by Client ID</a><br />
                    <a href="#">2. Renewed Service Contract Listing by Contract Group</a><br />
                    <a href="#">3. Renewed Service Contract Listing by Salesman</a><br />
             
                </div>
            </div>
           
             <div class='left'>
                <b>New Service Contract</b>
                <div>
                    <a href="#">1. New Service Contract Listing by Client ID</a><br />
                    <a href="#">2. New Service Contract Listing by Contract Group</a><br />
                    <a href="#">3. New Service Contract Listing by Salesman</a><br />
                     </div>
            </div>
             <div>
                <b>Renewal Service Contract</b>
                <div>
                    <a href="#">1. Renewal Service Contract Listing by Client ID</a><br />
                    <a href="#">2. Renewal Service Contract Listing by Contract Group</a><br />
                    <a href="#">3. Renewal Service Contract Listing by Salesman</a><br />
                   
                </div>
            </div>
             <div class='left'>
                <b>Active Service Contract</b>
                <div>
                    <a href="#">1. Active Service Contract Listing by Client ID</a><br />
                    <a href="#">2. Active Service Contract Listing by Contract Group</a><br />
                           <%--<br />--%>
                </div>
            </div>
       <div class='left'>
                <b>Terminated Service Contract</b>
                <div>
                      <a href="#">1. Terminated Service Contract Listing by Client ID</a><br />
                    <a href="#">2. Terminated Service Contract Listing by Contract Group</a><br />
                    <a href="#">3. Terminated Service Contract Listing by Salesman</a><br />
                
                </div>
            </div>
            <div style='clear: both;'></div>
        </div>
    </li>
    <li class="separator"></li>
         <li><a href="#CSS-Menu">MANAGEMENT</a>
         <div class="drop decor3_2" style="width: 640px;">
            <div class='left'>
                <b>Productivity</b>
                <div>
                    <a href="#">1. Team Productivity Report - Details</a><br />
                    <a href="#">2. Team Productivity Report - Summary</a><br />
                    <a href="#">3. Manpower Productivity by Team Leader - Details</a><br />
                    <a href="#">4. Manpower Productivity by Team Leader - Summary</a><br />
                    <a href="#">5. Manpower Productivity by Team Members - Details</a><br />
                     <a href="#">6. Manpower Productivity by Team Members - Summary</a><br />
                    <a href="#">7. Manpower Productivity by Team Members - Over Time Details</a><br />
                    <a href="#">8. Manpower Productivity by Team Members - Over Time Summary</a><br />
                    <a href="#">9. Vehicle Productivity Report - Details</a><br />
                    <a href="#">10. Vehicle Productivity Report - Summary</a>
                                 
                </div>
            </div>
             
        </div>
    </li>
         <li class="separator"></li>
         <li><a>REVENUE</a>
         <div class="drop decor3_2" style="width: 600px;">
            <div class='left'>
                <b>Actual Revenue</b>
                <div>
                    <a href="#">1. Actual Revenue Report by Date</a><br />
                    <a href="#">2. Actual Revenue Report by Team</a><br />
                    <a href="#">3. Actual Revenue Report by Client</a><br />
                    <a href="#">4. Actual Revenue Report by Account Code</a><br />
                    <a href="#">5. Actual Revenue Report by Postal Code</a>
                      
                </div>
            </div>
                <div class='left'>
                <b>Forecasted Revenue</b>
                <div>
                    <a href="#">1. Forecasted Revenue Report by Date</a><br />
                    <a href="#">2. Forecasted Revenue Report by Team</a><br />
                    <a href="#">3. Forecasted Revenue Report by Client</a><br />
                    <a href="#">4. Forecasted Revenue Report by Account Code</a><br />
                    <a href="#">5. Forecasted Revenue Report by Postal Code</a>
                      
                </div>
            </div>
               <div class='left'>
                <b>Others</b>
                <div>
                   <a href="#">1. Zero Value Listing Report</a>
                      
                </div>
            </div>
             
                 <div class='left'>
                <b>Accounts</b>
                <div>
                    <a href="#">1. Revenue Report for Accounts by Contract Group</a><br />
                    <a href="#">2. Revenue Report for Accounts by Contract Group and Billing Frequency</a><br />
                    <a href="#">3. Revenue Report for Accounts by Industry</a><br />
                              
                </div>
            </div>
             
        </div>
    </li>
         
         <li class="separator"></li>
         <li><a href="#CSS-Menu">PORTFOLIO</a>
         <div class="drop decor3_2" style="width: 480px;">
            <div class='left'>
                <b>Portfolio</b>
                <div>
                    <a href="#">1. Contract Group Value Report</a><br />
                    <a href="#">2. Portfolio Movement Detail</a><br />
                    <a href="#">3. Portfolio Movement Summary</a><br />
                    <a href="#">4. Portfolio Value Summary by Contract Group Category</a><br />
                      
                </div>
            </div>
                <div class='left'>
                <b>Schedule Summary</b>
                <div>
                    <a href="#">1. Scheduled Service VS Completed Service</a><br />
                          </div>
            </div>
           
    </li>
 
    
  
</ul>
         </td>
                </tr>
        
            </table>

   <div style="height:500px;"></div>
</asp:Content>

