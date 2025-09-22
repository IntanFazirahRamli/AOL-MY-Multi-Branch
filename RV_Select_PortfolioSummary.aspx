<%@ Page Title="Portfolio Summary" Language="VB" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" CodeFile="RV_Select_PortfolioSummary.aspx.vb" Inherits="RV_Select_PortfolioSummary" %>
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
    <asp:UpdateProgress ID="updateProgress1" runat="server">
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

     
        <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedOn" runat="server" Visible="true" CssClass="dummybutton"></asp:TextBox>
               
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
             </asp:ToolkitScriptManager>  
    <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold; color: #000099;">PORTFOLIO SUMMARY</h4>

    <table style="width: 100%; text-align: center;">
        <tr>
            <td colspan="2" style="width: 100%; text-align: center; font-size: 18px; font-weight: bold; font-family: 'Calibri';">
                <asp:Label ID="lblAlert" runat="server" Text="" BackColor="Red" ForeColor="White"></asp:Label>
            </td>
        </tr>
    </table>

    <table style="width: 100%; font-family: Calibri; font-size: 15px; font-weight: bold; color: black; text-align: left; padding-left: 20px; line-height: 25px;">
        <tr>
            <td class="CellFormat" style="width: 25%">Date</td>
            <td class="CellTextBox" colspan="4">
                <asp:TextBox ID="txtSvcDateFrom" runat="server" MaxLength="10" Height="16px" Width="15%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtSvcDateFrom" TargetControlID="txtSvcDateFrom" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
                &nbsp;&nbsp; &nbsp;TO&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtSvcDateTo" runat="server" MaxLength="10" Height="16px" Width="15%"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtSvcDateTo" TargetControlID="txtSvcDateTo" Format="dd/MM/yyyy" Enabled="True"></asp:CalendarExtender>
            </td>
        </tr>
        
           <tr>
               <td></td>
            <td colspan="4" style="padding-left: 0%; padding-top: 1%">
                <asp:Panel ID="Panel2" runat="server" Height="40%" BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="2px" Width="40%" HorizontalAlign="Center">
                    <div style="padding-left: 20px; text-align: left; padding-bottom: 20px;">
                        <asp:RadioButtonList ID="rbtnSelect" runat="server" CellSpacing="5" CellPadding="5" RepeatDirection="Horizontal">
                           <%-- <asp:ListItem Text="Contract Group Value Report" Value="1" Selected="true"></asp:ListItem>--%>
                            <asp:ListItem Text="Month" Value="SummaryMonthWise" Selected="true"></asp:ListItem>
                            <asp:ListItem Text="Date" Value="SummaryDateWise"></asp:ListItem>
                           
                        </asp:RadioButtonList>
                    </div>
                </asp:Panel>
            </td>
        </tr>
          <tr>
            <td colspan="5"><br /></td>
        </tr>
        <tr>
            <td colspan="5" style="text-align:center">
                     <asp:Button ID="btnExportToExcel" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Export To Excel" Width="120px" Visible="true"/>
                      <asp:Button ID="btnCloseServiceRecordList" runat="server" CssClass="button" BackColor="#CFC6C0" Font-Bold="True" Text="Cancel" Width="120px" OnClientClick="currentdatetime()"/>
     
            </td>
        
          
        </tr>
    </table>
    </ContentTemplate>
         <Triggers>
             <asp:PostBackTrigger ControlID="btnExportToExcel" />
            
            </Triggers>
         </asp:UpdatePanel>
            <asp:TextBox ID="txtQuery" runat="server" CssClass="dummybutton" ></asp:TextBox>
        <asp:TextBox ID="txtCriteria" runat="server" CssClass="dummybutton" ></asp:TextBox>

</asp:Content>



