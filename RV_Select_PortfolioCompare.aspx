<%@ Page Title="Portfolio Report Comparison" Language="VB" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" CodeFile="RV_Select_PortfolioCompare.aspx.vb" Inherits="RV_Select_PortfolioCompare" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
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
            font-size: 15px;
            font-weight: bold;
            font-family: Calibri;
            color: black;
            text-align: right;
            width: 30%;
            border-collapse: collapse;
            border-spacing: 0;
            line-height: 10px;
            height: 50px;
        }
        .auto-style2 {
            color: black;
            text-align: left;
            font-family: Calibri;
            height: 50px;
            padding-left: 20px;
        }
        .auto-style3 {
            width: 60%;
            height: 35px;
        }
        .auto-style4 {
            width: 40%;
            height: 35px;
        }
        </style>
       <script type="text/javascript">

           function UploadFile1(fileUpload) {
               if (fileUpload.value != '') {
                   document.getElementById("<%=btnUpload1.ClientID%>").click();
        }
           }

           function UploadFile2(fileUpload) {
               if (fileUpload.value != '') {
                   document.getElementById("<%=btnUpload2.ClientID%>").click();
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
</script>
    <div style="text-align:center">
    <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
               <div class="loading-panel">
                    <div class="loading-container">
                        <img src="<%= Me.ResolveUrl("~/images/logo_loader_new.gif")%>" />
                    </div>
                </div>
               
            </ProgressTemplate>
        </asp:UpdateProgress>
 
     <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
         <ContentTemplate>

             
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
             </asp:ToolkitScriptManager>  
    <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold; color: #000099;">PORTFOLIO REPORT COMPARISON</h4>

    <table style="width: 100%; text-align: center;">
        <tr>
            <td colspan="2" style="width: 100%; text-align: center; font-size: 18px; font-weight: bold; font-family: 'Calibri';">
                <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
            </td>
        </tr>
    </table>
          
        <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
     
    <table style="width: 100%; font-family: Calibri; font-size: 15px; font-weight: bold; color: black; text-align: left; padding-left: 20px; line-height: 25px;">
        <tr>
            <td class="CellFormat" style="width: 25%">File1</td>
            <td class="CellTextBox" colspan="3">
                <asp:FileUpload ID="FileUpload1" runat="server" /><asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                

                </td><td style="text-align:left">
                <asp:Button ID="btnUpload1" runat="server" Text="Upload" CssClass="roundbutton1" Font-Bold="true" /> </td>
        </tr>
         <tr>
            <td class="CellFormat" style="width: 25%">File2</td>
            <td class="CellTextBox" colspan="3">
                <asp:FileUpload ID="FileUpload2" runat="server" /><asp:Label ID="Label2" runat="server" Text=""></asp:Label>
            

                </td><td style="text-align:left">         <asp:Button ID="btnUpload2" runat="server" Text="Upload" CssClass="roundbutton1" Font-Bold="true" /> </td>
        </tr>
        
         
          <tr>
            <td colspan="5"><br /></td>
        </tr>
        <tr>
            <td colspan="5" style="text-align:center">
                     <asp:Button ID="btnExportToExcel" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0"  Font-Bold="True" Text="Excel Compare" Width="120px" Visible="true"/>
                      <asp:Button ID="btnClose" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="Cancel" Width="120px" OnClientClick="currentdatetime()"/>
     
            </td>
        
          
        </tr>
    </table>
    </ContentTemplate>
         <Triggers>
             <asp:PostBackTrigger ControlID="btnExportToExcel" />
            <asp:PostBackTrigger ControlID="btnUpload1" />
             <asp:PostBackTrigger ControlID="btnUpload2" />
           
            </Triggers>
         </asp:UpdatePanel>
      <br />
           <asp:ImageButton ID="btnExcel" runat="server" ImageUrl="~/Images/excel.png" Width="40px" Height="25px" OnClientClick="currentdatetime()" />
        
        <asp:GridView ID="GridView1" runat="server" Width="744px" CssClass="Centered">
                          <Columns>
                             <%--  <asp:BoundField DataField="AccountID" HeaderText="AccountID" SortExpression="AccountID" >
                       <ControlStyle Width="150px" />
                       <HeaderStyle Width="150px" />
                       </asp:BoundField>
                       <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" >
                       <ControlStyle Width="180px" />
                       <HeaderStyle Width="180px" />
                       </asp:BoundField>
                              <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" >
                       <ControlStyle Width="180px" />
                       <HeaderStyle Width="180px" />
                       </asp:BoundField>--%>
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
                 
            <asp:TextBox ID="txtQuery" runat="server" CssClass="dummybutton" ></asp:TextBox>
        <asp:TextBox ID="txtCriteria" runat="server" CssClass="dummybutton" ></asp:TextBox>
     <asp:TextBox ID="txtWorkBookName" runat="server" CssClass="dummybutton"></asp:TextBox>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div>
</asp:Content>




