<%@ Page Title="Mass Change of Service Records" Language="vb" AutoEventWireup="false" CodeFile="ServiceMassChange.aspx.vb" Inherits="ServiceMassChange" MasterPageFile="~/MasterPage.Master" Culture="en-GB" %>


<%@ Register Assembly="Ajaxified" Namespace="Ajaxified" TagPrefix="Ajaxified" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .Hide {
            display: none;
        }

        .cell {
            text-align: left;
        }

        .righttextbox {
            float: right;
        }

        .AlphabetPager a, .AlphabetPager span {
            font-size: 8pt;
            display: inline-block;
            min-width: 15px;
            text-align: center;
            text-decoration: none;
            font-weight: bold;
        }

        .AlphabetPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
        }

        .AlphabetPager span {
            background-color: #A1DCF2;
            color: #000;
            border: 1px solid #3AC0F2;
        }

        .modalBackground {
            background-color: gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .wrp {
            width: 100%;
            text-align: center;
        }

        .frm {
            text-align: left;
            width: 500px;
            margin: auto;
            height: 40px;
        }

        .fldLbl {
            white-space: nowrap;
        }

        .lbl {
            font-size: 15px;
            font-weight: bold;
            font-family: 'Calibri';
            color: black;
            text-align: right;
        }
       
                     .roundbutton1 {
            border: 2px solid #a1a1a1;   
    background: #dddddd;   
    border-radius: 25px;
    height:30px;
    width:90%;
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
        .auto-style4 {
            width: 40%;
            height: 35px;
        }
        </style>
       <script type="text/javascript">

        

      function stopRKey(evt) {
          var evt = (evt) ? evt : ((event) ? event : null);
          var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
          if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
      }

      document.onkeypress = stopRKey;

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


      var defaultTextTeam = "Search Here for Team or In-ChargeId";
      function WaterMarkTeam(txt, evt) {
          if (txt.value.length == 0 && evt.type == "blur") {
              txt.style.color = "gray";
              txt.value = defaultTextTeam;
          }
          if (txt.value == defaultTextTeam && evt.type == "focus") {
              txt.style.color = "black";
              txt.value = "";
          }
      }
   
      var defaultTextStaff = "Search Here for Staff";
      function WaterMarkStaff(txt, evt) {
          if (txt.value.length == 0 && evt.type == "blur") {
              txt.style.color = "gray";
              txt.value = defaultTextStaff;
          }
          if (txt.value == defaultTextStaff && evt.type == "focus") {
              txt.style.color = "black";
              txt.value = "";
          }
      }

      function ResetScrollPosition() {
          setTimeout("window.scrollTo(0,0)", 0);
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
               <div class="loading-panel">
                    <div class="loading-container">
                        <img src="<%= Me.ResolveUrl("~/images/logo_loader_new.gif")%>" />
                    </div>
                </div>
               
            </ProgressTemplate>
        </asp:UpdateProgress>
 
  
     <asp:UpdatePanel ID="updPanelMassChange1" runat="server" UpdateMode="Conditional">
          <ContentTemplate>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
           <asp:controlBundle name="ModalPopupExtender_Bundle"/>
              <asp:controlBundle name="MaskedEditExtender_Bundle"/>
                <asp:controlBundle name="TabContainer_Bundle"/>
   </ControlBundles>
    </asp:ToolkitScriptManager>     




    <div>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
     <ContentTemplate>


      <div style="text-align:center">
         
       
            <h3 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099; text-align:center">Mass Change of Service Schedule</h3>
        
        <table border="0" style ="width:100%;text-align:center;">
         <tr>
              <td colspan="8" style="width:100%; text-align:center;color:brown;font-size:15px;font-weight:bold;font-family:'Calibri';"> 
                                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        </td>
            </tr>      
          
            <tr>
              <td colspan="8">
                  <asp:Label ID="lblAlert" runat="server" BackColor="Red" ForeColor="White"></asp:Label>
                </td>
            </tr>
               <tr style="text-align:center;width:100%">
                <td style="text-align:center;padding-left:5px;" colspan="8">
                      <asp:Panel ID="Panel5" runat="server" BackColor="#F8F8F8"   BorderColor="gray" BorderWidth="2" BorderStyle="Solid" Width="100%" Height="90%"  HorizontalAlign="Center">
                              
                     <table border="0" style="width:100%;text-align:center;padding-left:10px;padding-top:0px; ">
                                
                           <tr>
                                        <td style="width: 8%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left;" >Sch. Date From</td>
                                       <td style="text-align: left; width:12%" >
                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="50" Height="16px" Width="150px"
                                                AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:CalendarExtender ID="calFrom" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtFromDate" TargetControlID="txtFromDate" />
                                        </td>

                                        <td style="width: 10%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-left:4%">To

                                              <asp:Button ID="btnDummyClient" runat="server" BackColor="#F8F8F8" BorderStyle="None" CssClass="roundbutton" Font-Bold="True" ForeColor="White" Text="L" Width="2px" />
                                           <asp:Button ID="btnDummyContract" runat="server" BackColor="#F8F8F8" BorderStyle="None" CssClass="roundbutton" Font-Bold="True" ForeColor="White" Text="L" Width="2px" />
                                         
                                        </td>
                                       <td style="text-align: left; width:18%" >
                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="50" Height="16px" Width="200px"
                                                AutoCompleteType="Disabled"></asp:TextBox>

                                            <asp:CalendarExtender ID="calTo" runat="server" Format="dd/MM/yyyy" PopupButtonID="txtToDate" TargetControlID="txtToDate" />
                                        </td>

                                        <td style="width: 10%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-left:2%">DOW</td>
                                        <td style="text-align: left; width:18%" >
                                            <asp:DropDownList ID="ddlDOW" runat="server" AppendDataBoundItems="true" Width="200px" Height="25px">
                                                <asp:ListItem>--SELECT--</asp:ListItem>
                                                <asp:ListItem>MONDAY</asp:ListItem>
                                                <asp:ListItem>TUESDAY</asp:ListItem>
                                                <asp:ListItem>WEDNESDAY</asp:ListItem>
                                                <asp:ListItem>THURSDAY</asp:ListItem>
                                                <asp:ListItem>FRIDAY</asp:ListItem>
                                                <asp:ListItem>SATURDAY</asp:ListItem>
                                                <asp:ListItem>SUNDAY</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; width:10%">Week Number
                                            </td>
                                        <td style="text-align: left; width:15%">
                                            <asp:DropDownList ID="ddlWeekNoSearch" runat="server" AppendDataBoundItems="true" Height="25px" Width="75%">
                                                <asp:ListItem>--SELECT--</asp:ListItem>
                                                <asp:ListItem>1st Week</asp:ListItem>
                                                <asp:ListItem>2nd Week</asp:ListItem>
                                                <asp:ListItem>3rd Week</asp:ListItem>
                                                <asp:ListItem>4th Week</asp:ListItem>
                                                <asp:ListItem>5th Week</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="padding-top: 2px;">
                                        <td style="width: 8%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;" >Location ID</td>
                                        <td  style="text-align: left; width:12%" >
                                            <asp:TextBox ID="txtClient" runat="server" Height="16px" Width="73%" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:ImageButton ID="imgbtnClient" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg" Height="22" Width="24" ImageAlign="Top" />
                                        </td>

                                        <td style="width: 10%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; padding-left:4%">Name</td>
                                        <td style="text-align: left; width:18%" >
                                            <asp:TextBox ID="txtName" runat="server" Height="16px" Width="200px" AutoCompleteType="Disabled"></asp:TextBox>
                                           <asp:ImageButton ID="ImageButton2" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg" Height="22" Width="24"  ImageAlign="Top" />
                                               <asp:ModalPopupExtender ID="mdlPopUpTeam" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlTeamClose" Enabled="True" PopupControlID="pnlPopUpTeam" TargetControlID="btnDummyTeam">
                                            </asp:ModalPopupExtender>
                                            <asp:ModalPopupExtender ID="mdlPopUpClient" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlClientClose" PopupControlID="pnlPopUpClient" TargetControlID="btnDummyClient">
                                            </asp:ModalPopupExtender>
                                              <asp:ModalPopupExtender ID="mdlPopUpStaff" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlStaffClose" Enabled="True" PopupControlID="pnlPopUpStaff" TargetControlID="btnDummyStaff">
                                            </asp:ModalPopupExtender>
                                           
                                           
                                        </td>

                                            <td style="width: 10%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; padding-left:2%" >Contract No</td>
                                         <td style="text-align: left; width:18%" >
                                            <asp:TextBox ID="txtContractNo" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:ModalPopupExtender ID="mdlPopUpContractNo" runat="server" CancelControlID="btnPopUpContractNoClose" PopupControlID="pnlPopUpContractNo"
                                                TargetControlID="btnDummyContract" BackgroundCssClass="modalBackground">
                                            </asp:ModalPopupExtender>
                                            <asp:ImageButton ID="imgBtnContractNo" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg" Height="22" Width="24" />
                                        </td>

                                        <td style="font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; width:10%" rowspan="1">Service Description</td>
                                        <td rowspan="3" style="text-align: left; width:15%;"> <asp:TextBox ID="txtSvcDescription" runat="server" Height="40px" Width="90%" AutoCompleteType="Disabled" TextMode="MultiLine"></asp:TextBox></td>

                                    </tr>

                                    <tr >

                                        <td style="width: 8%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;" >Team ID</td>
                                        <td style="text-align: left; width:12%" >
                                            <asp:TextBox ID="txtTeam" runat="server" Height="16px" Width="73%" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:ImageButton ID="imgBtnTeam" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                                Height="22" Width="24" />
                                        </td>

                                        <td style="width: 10%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; padding-left:4%">Incharge</td>
                                        <td style="text-align: left; width:18%" >
                                            <asp:TextBox ID="txtIncharge" runat="server" Height="16px" Width="200px" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBtnInCharge" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                                Height="22" Width="24"  ImageAlign="Top" />
                                           
                                           
                                           


                                            </td>

                                        <td style="width: 10%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;padding-left:2%">Service By</td>
                                         <td style="text-align: left; width:18%" >
                                            <asp:TextBox ID="txtServiceBy" runat="server" Height="16px" Width="80%" AutoCompleteType="Disabled"></asp:TextBox>
                                      <asp:ImageButton ID="ImgBtnServiceBy" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                                Height="22" Width="24"  ImageAlign="Top"/>
                                              </td>
                                        <td style="text-align: left; width:10%">&nbsp;</td>
                                       <%-- <td style="text-align: left; width:15%">&nbsp;</td>--%>
                                    </tr>
                                    <tr>
                                        <td style="width: 8%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">Service Address</td>
                                      <td style="text-align: left; width:37%" colspan="3">
                                                 <asp:TextBox ID="txtServiceAddressSearch" runat="server" Height="16px" Width="93%" AutoCompleteType="Disabled"></asp:TextBox>
                                
                                        </td>
                                      <%--  <td style="width: 10%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; padding-left:4%">&nbsp;</td>--%>
                                       <%-- <td style="text-align: left; width:18%">&nbsp;</td>--%>
                                        <td style="width: 10%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;padding-left:2%">Scheduler</td>
                                        <td style="text-align: left; width:18%">
                                            <asp:DropDownList ID="ddlSchedulerSearch" runat="server" AppendDataBoundItems="true" Height="25px" Width="90%">
                                                <asp:ListItem>--SELECT--</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left; width:10%">&nbsp;</td>
                           </tr>
                                    </asp:Panel>
                          
                                    </table>
                               
                                
                        
                    </td>
                   </tr>
            </table>

             <table style="width: 100%; text-align: center;">
                    <tr>
                        <td style="width: 50%; text-align: center;">
                            <asp:Button ID="btnGo" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="GO" Width="100px" OnClick="btnGo_Click" />
                            &nbsp;<asp:Button ID="btnReset" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="RESET" Width="100px" OnClick="btnReset_Click" />
                            &nbsp;<asp:Button ID="btnQuit" runat="server" CssClass="roundbutton1" BackColor="#CFC6C0" Font-Bold="True" Text="QUIT" Width="100px" />

                        </td>

                         <td style="text-align: left; width:8%; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;" class="auto-style4">
                        Total Records :  
                                

                        </td>
                         <td style="width: 8%; text-align: center;">
                          <asp:TextBox ID="txtTotalRecords" runat="server" AutoCompleteType="Disabled" BackColor="#CCCCCC" Height="16px" style="text-align:center;" Width="90%"></asp:TextBox>
                                

                        </td>
                    </tr>
                </table>



  <%--<asp:Panel ID="pnlServiceRecord" runat="server" Width="1300px" ScrollBars="Auto" BackColor="#F8F8F8" BorderColor="gray" BorderWidth="2" BorderStyle="Solid" Height="200px">
  --%>         <asp:Panel ID="pnlServiceRecord" runat="server" BackColor="White" BorderColor="#003366" BorderWidth="1" Height="200px" ScrollBars="Auto" style="text-align:center; width:1350px; margin-left:auto; margin-right:auto;"    Visible="true" Width="1330px">
                     
                    <table>
                        <tr style="text-align: center;">
                            <td style="width: 100%; text-align: center">

                                <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" 
                                      Font-Size="14px"  CellPadding="3" CellSpacing="2" DataKeyNames="RecordNo" 
                                    HorizontalAlign="Center" AllowSorting="True" ForeColor="#333333" GridLines="Vertical"> 
                                              <AlternatingRowStyle BackColor="White"/>
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_CheckedChanged" AutoPostBack="true" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkGrid" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RecordNo" HeaderText="Service Record" ReadOnly="True" SortExpression="RecordNo">
                                            <ControlStyle Width="125px" />
                                            <HeaderStyle Font-Size="10pt" />
                                            <ItemStyle Width="125px" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="ContractNo" HeaderText="Contract No.">
                                        <ControlStyle Width="130px" />
                                        <ItemStyle Width="130px" Wrap="False" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="LocationID" HeaderText="LocationID" >

                                        <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CustCode" HeaderText="Client ID" SortExpression="CustCode">
                                            <ControlStyle Width="80px" />
                                            <HeaderStyle Font-Size="10pt" />
                                            <ItemStyle Width="80px" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Accountid" HeaderText="Account Id" SortExpression="Accountid" >
                                        <ControlStyle CssClass="dummybutton" />
                                        <HeaderStyle Wrap="False" CssClass="dummybutton" />
                                        <ItemStyle Wrap="False" CssClass="dummybutton" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CustName" HeaderText="Name" SortExpression="CustName">
                                            <ControlStyle Width="230px" />
                                            <HeaderStyle Font-Size="10pt" HorizontalAlign="Left" />
                                            <ItemStyle Width="230px" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Address1" HeaderText="Service Address" SortExpression="Address1">
                                            <ControlStyle Width="250px" />
                                            <HeaderStyle Font-Size="10pt" HorizontalAlign="Left" />
                                            <ItemStyle Width="250px" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="schServiceDate" HeaderText="Sch. Date" SortExpression="schServiceDate" DataFormatString="{0:d}">
                                            <ControlStyle Width="80px" />
                                            <HeaderStyle Font-Size="10pt" />
                                            <ItemStyle Width="80px" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SchServiceTime" HeaderText="Time In" SortExpression="TimeIn">
                                            <ControlStyle Width="100px" />
                                            <HeaderStyle Font-Size="10pt" Wrap="False" />
                                            <ItemStyle Width="100px" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SchServiceTimeOut" HeaderText="Time Out" SortExpression="TimeOut">
                                            <ControlStyle Width="100px" />
                                            <HeaderStyle Font-Size="10pt" Wrap="False" />
                                            <ItemStyle Width="100px" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DOW" HeaderText="DOW" SortExpression="DOW">
                                            <ControlStyle Width="100px" />
                                            <HeaderStyle Font-Size="10pt" />
                                            <ItemStyle Width="100px" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TeamId" HeaderText="Team Id" SortExpression="TeamId">
                                            <ControlStyle Width="150px" />
                                            <HeaderStyle Font-Size="10pt" />
                                            <ItemStyle Width="150px" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="InchargeId" HeaderText="InCharge" SortExpression="InchargeId">
                                            <ControlStyle Width="100px" />
                                            <HeaderStyle Font-Size="10pt" />
                                            <ItemStyle Width="100px" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundField>
                                         <asp:TemplateField HeaderText="ServiceDescription" SortExpression="Notes">
                <ItemTemplate>
                    <div style="width: 300px;text-align:left;height:37px;overflow-y:auto;">
                        <%# Eval("Notes")%>  
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
                                        <asp:BoundField DataField="VehNo" HeaderText="Vehicle No." SortExpression="VehNo">
                                            <ControlStyle Width="100px" />
                                            <HeaderStyle Font-Size="10pt" />
                                            <ItemStyle Width="100px" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ServiceBy" HeaderText="Service By" SortExpression="ServiceBy">
                                            <ControlStyle Width="120px" />
                                            <HeaderStyle Font-Size="10pt" />
                                            <ItemStyle Width="120px" HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Rcno" InsertVisible="False" SortExpression="Rcno" Visible="False">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Rcno") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Rcno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
                                        <asp:BoundField DataField="ScheduleType" HeaderText="Schedule Type">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Comments" HeaderText="Service Instruction">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Scheduler" HeaderText="Scheduler" />

                                        <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy" >
                                        <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CreatedOn" HeaderText="Created On" SortExpression="CreatedOn" >
                                        <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastModifiedBy" HeaderText="Last Modified By" SortExpression="LastModifiedBy" >
                                        <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LastModifiedOn" HeaderText="Last Modified On" SortExpression="LastModifiedOn" >

                                        <ItemStyle Wrap="False" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Holiday">
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
                                                    <SelectedRowStyle BackColor="#AEE4FF" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

<%--            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnGo" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>--%>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
            ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>

        <asp:TextBox ID="TextBox2" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtRcno" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtCreatedBy" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtCreatedOn" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtLastModifiedBy" runat="server" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtLastModifiedOn" runat="server" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtIsPopup" runat="server" Height="16px" Width="200px" AutoCompleteType="Disabled" Visible="False"></asp:TextBox>
          <asp:Button ID="btnDummyStaff" runat="server" BackColor="#F8F8F8" BorderStyle="None" CssClass="dummybutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
          <asp:Button ID="btnDummyTeam" runat="server" BackColor="#F8F8F8" BorderStyle="None" CssClass="dummybutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
                                   
    </div>


                <asp:Panel ID="pnlTeamDetails" runat="server" BackColor="#F8F8F8" BorderColor="gray" BorderWidth="2" BorderStyle="Solid" Width="100%">
                <table border="0">
                    <tr>
                        <td  style="font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:8%;">
                            <asp:RadioButton ID="rbtnTeamDetails" runat="server" Text="Team Details" AutoPostBack="true" OnCheckedChanged="rbtnTeamDetails_CheckedChanged" />
                        </td>
                   
                        <td style="width:6%; text-align: right; font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black;" >Team Id</td>
                           <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:13%;" >
                     
                            <asp:TextBox ID="txtTeamDetailsId" runat="server" Height="16px" Width="75%" AutoCompleteType="Disabled"></asp:TextBox>
                           
                            <asp:ImageButton ID="imgBtnTeamDetails" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                Height="22" Width="24" ImageAlign="Top" />
                        </td>
                          <td style="width: 6%; text-align: right; font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black;">Incharge</td>
                          <td style=" font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 5px; width:10%;" >
                          <asp:TextBox ID="txtTeamDetailsIncharge" runat="server" Width="80%"></asp:TextBox>
                              <asp:ImageButton ID="ImgBtnInChargeDetails" runat="server" CssClass="righttextbox" Height="22" ImageUrl="~/Images/searchbutton.jpg" Width="24" />
                        </td>
                        <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">Service By</td>
                         <td style=" font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 5px; width:12%;" >
                            <asp:TextBox ID="txtTeamDetailsServiceBy" runat="server" Width="75%"></asp:TextBox>
                             &nbsp;<asp:ImageButton ID="ImgBtnServiceByDetails" runat="server" CssClass="righttextbox" Height="22" ImageUrl="~/Images/searchbutton.jpg" Width="24" />
                        </td>
                        <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;" >
                            Supervisor</td>
                        <td style="width: 10%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                            <asp:DropDownList ID="ddlSupervisor" runat="server" AppendDataBoundItems="true" Height="25px" Width="98%">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 8%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                            <asp:CheckBox ID="chkUpdateServiceContract" runat="server" Text="Update Service Contract" />
                        </td>
                    </tr>




                         <tr>
                        <td  style="font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:8%;">
                             <asp:RadioButton ID="rbtnDOWDetails" runat="server" Text="DOW Details" AutoPostBack="true" OnCheckedChanged="rbtnDOWDetails_CheckedChanged" />
                       </td>
                   
                    
                        <td style="width:6%;  font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black; text-align:right" >
                             <asp:RadioButton id="rbtnDOW" runat="server" OnCheckedChanged="rbtnDOW_CheckedChanged" AutoPostBack="true" />DOW</td>
                       </td>
                        <td style="width: 10%; text-align: left; font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black; ">
                       <asp:DropDownList ID="ddlDOWDetailsDOW" runat="server" AppendDataBoundItems="true" Width="92%" Height="25px">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                                <asp:ListItem>MONDAY</asp:ListItem>
                                <asp:ListItem>TUESDAY</asp:ListItem>
                                <asp:ListItem>WEDNESDAY</asp:ListItem>
                                <asp:ListItem>THURSDAY</asp:ListItem>
                                <asp:ListItem>FRIDAY</asp:ListItem>
                                <asp:ListItem>SATURDAY</asp:ListItem>
                                <asp:ListItem>SUNDAY</asp:ListItem>
                            </asp:DropDownList>
                             </td>
                             <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                                 <asp:RadioButton ID="rbtnWeekNo" runat="server" AutoPostBack="true" OnCheckedChanged="rbtnWeekNo_CheckedChanged" />
                                 Week</td>
                        </td>
                       
                        
                              <td style="width:10%;  font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black; text-align:left;" >
                            <asp:DropDownList ID="ddlWeekNo" runat="server" AppendDataBoundItems="true" Width="82%" Height="25px">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                                <asp:ListItem>1st Week</asp:ListItem>
                                <asp:ListItem>2nd Week</asp:ListItem>
                                <asp:ListItem>3rd Week</asp:ListItem>
                                <asp:ListItem>4th Week</asp:ListItem>
                                <asp:ListItem>5th Week</asp:ListItem>
                            </asp:DropDownList>
                                  </td>
                             <td style="font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black; text-align:Left" colspan="2" >
                                 <asp:CheckBox ID="chkUpdateServiceContractDOWDetails" runat="server" Text="Update Service Contract" />
                        </td>
                                 <td style="width:6%; text-align: right; font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black;" >&nbsp;</td>
                                
                       
                     
                              <td style="width:10%; text-align: right; font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black;">&nbsp;</td>
                             <td style="width:15%; text-align: right; font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black;"></td>
                                
                       
                     
                              </tr>


                                       



                    <tr>
                        <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:9%;" >
                            <asp:RadioButton ID="rbtnScheduler" runat="server" AutoPostBack="true" Text="Scheduler" />
                        </td>
                   
                   
                        <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; width:6%" >Scheduler</td>
                           <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:13%;" >
                     
                               <asp:DropDownList ID="ddlScheduler" runat="server" AppendDataBoundItems="true" Height="25px" Width="92%">
                                   <asp:ListItem>--SELECT--</asp:ListItem>
                               </asp:DropDownList>
                     
                                 </td>
                          <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">&nbsp;</td>
                          <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:13%;" >
                     <asp:CheckBox ID="chkUpdateServiceContractScheduler" runat="server" Text="Update Service Contract" />
                              &nbsp;</td>

                         <td>
                             &nbsp;</td>

                          <td>
                              <asp:TextBox ID="txtGridQuery" runat="server" Enabled="False" Visible="False" Width="40%"></asp:TextBox>
                        </td>
                         <td style="width: 6%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;" >
                             &nbsp;</td>
                        <td style="width: 10%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">&nbsp;</td>
                        <td style="width: 12%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:9%;">
                            <asp:RadioButton ID="rbtnScheduleTime" runat="server" AutoPostBack="true" OnCheckedChanged="rbtnScheduleTime_CheckedChanged" Text="Schedule Time" />
                        </td>
                        <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; width:6%">Time In</td>
                        <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:13%;">
                            <asp:TextBox ID="txtScheduleTimeIn" runat="server" AutoCompleteType="Disabled" Width="90%"></asp:TextBox>
                        </td>
                        <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">Time Out</td>
                        <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:13%;">
                            <asp:TextBox ID="txtScheduleTimeOut" runat="server" AutoCompleteType="Disabled" Width="80%"></asp:TextBox>
                            <asp:MaskedEditExtender ID="txtServTimeIn_MaskedEditExtender" runat="server" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtScheduleTimeIn">
                            </asp:MaskedEditExtender>
                            <asp:MaskedEditExtender ID="txtServTimeOut_MaskedEditExtender" runat="server" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" Mask="99:99" MaskType="Time" TargetControlID="txtScheduleTimeOut">
                            </asp:MaskedEditExtender>
                        </td>
                        <td style="font-size: 14px; font-weight: bold; font-family: 'Calibri'; color: black; text-align:left" colspan="2" >
                            <asp:CheckBox ID="chkUpdateServiceContractScheduledTime" runat="server" Text="Update Service Contract" />
                            <asp:TextBox ID="txtStaffSelect" runat="server" AutoCompleteType="Disabled" BackColor="#F8F8F8" BorderStyle="None" ForeColor="#F8F8F8" Width="30%"></asp:TextBox>
                            <asp:Button ID="btnDummyTeam0" runat="server" BackColor="#F8F8F8" BorderStyle="None" CssClass="roundbutton" Font-Bold="True" ForeColor="White" Text="L" Width="24px" />
                            <asp:ModalPopupExtender ID="btnDummyTeam0_ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground" CancelControlID="btnPnlTeamClose" Enabled="True" PopupControlID="pnlPopUpTeam" TargetControlID="btnDummyTeam0">
                            </asp:ModalPopupExtender>
                        </td>
                        <td style="width: 6%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                            <asp:TextBox ID="txtProcessed" runat="server" AutoCompleteType="Disabled" BackColor="#F8F8F8" BorderStyle="None" ForeColor="#F8F8F8" Width="6%"></asp:TextBox>
                        </td>
                        <td style="width: 10%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                            <asp:TextBox ID="txtTeamSelect" runat="server" AutoCompleteType="Disabled" BackColor="#F8F8F8" BorderStyle="None" ForeColor="#F8F8F8" Width="30%"></asp:TextBox>
                        </td>
                        <td style="width: 12%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:8%;">
                            <asp:RadioButton ID="rbtnScheduleType" runat="server" AutoPostBack="true" Text="Schedule Type" />
                        </td>
                        <td colspan="1" style="text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; width:6%">
                            </td>
                       
                        <td style="width: 8%; text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; width:14%;" >
                            <asp:DropDownList ID="ddlScheduleType" runat="server" AppendDataBoundItems="true" Height="25px" Width="92%">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="1" style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; ">
                            &nbsp;</td>
                       <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:15%;">
                             <asp:CheckBox ID="chkUpdateServiceContractST" runat="server" Text="Update Service Contract" />
                        </td>
                        <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                            &nbsp;</td>

                        <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                            &nbsp;</td>
                         <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                            &nbsp;</td>
                        <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">&nbsp;</td>
                        <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:11%;">
                            <asp:RadioButton ID="rbtnServiceInstruction" runat="server" AutoPostBack="true" Text="Service Instruction" />
                        </td>
                        <td colspan="3" style="text-align: left; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; ">
                            <asp:TextBox ID="txtServiceInstruction" runat="server" Enabled="False" Width="99%" Height="60px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td style="text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black; text-align: left; padding-top: 2px; width:15%;">
                            <asp:CheckBox ID="chkUpdateServiceContractSI" runat="server" Text="Update Service Contract" />
                        </td>
                        <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                            &nbsp;</td>
                        <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                            &nbsp;</td>
                         <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                             &nbsp;</td>
                         <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">&nbsp;</td>
                        <td style="width: 6%; text-align: right; font-size: 15px; font-weight: bold; font-family: 'Calibri'; color: black;">
                            <asp:Button ID="btnProcess" runat="server" BackColor="#CFC6C0" CssClass="roundbutton1" Font-Bold="True" OnClick="btnProcess_Click" Text="PROCESS" Width="100px" />
                        </td>
                        
                    </tr>
                </table>
            </asp:Panel>
          
              </ContentTemplate>
                     </asp:UpdatePanel>


        <asp:Panel ID="pnlPopUpClient" runat="server" BackColor="White" Width="90%" Height="80%" BorderColor="#003366" BorderWidth="1px" HorizontalAlign="Center"><table style="margin-left:auto; margin-right:auto; "><tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Customer</h4></td><td style="width:1%;text-align:right;">
            <asp:ImageButton ID="btnPnlClientClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" />
</td></tr><tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri; color:black;text-align:center;padding-left:20px;">&#160;Search Name &#160;&#160; <asp:TextBox ID="txtPopUpClient" runat="server" Height="16px" MaxLength="50" Width="400px" ForeColor="Gray" onblur = "WaterMarkClient(this, event);" onfocus = "WaterMarkClient(this, event);" AutoPostBack="True">Search Here for AccountID or Client Name or Contact Person</asp:TextBox>
<asp:ImageButton ID="btnPopUpClientSearch" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/searchbutton.jpg" OnClick="btnPopUpClientSearch_Click" Width="24px" Visible="False"></asp:ImageButton>
<asp:ImageButton ID="btnPopUpClientReset" runat="server" Height="22px" ImageAlign="Middle" ImageUrl="~/Images/resetbutton.jpg" OnClick="btnPopUpClientReset_Click" Width="24px"></asp:ImageButton>
</td><td>
                    <asp:TextBox ID="txtPopupClientSearch" runat="server" Visible="False"></asp:TextBox>
                </td></tr></table><div style="text-align: center; padding-left: 10px; padding-bottom: 5px;"><div class="AlphabetPager">
        <asp:Repeater ID="rptClientAlphabets" runat="server" Visible="False"><ItemTemplate>
<asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("Value")%>' OnClick="ClientAlphabet_Click" ForeColor="Black" />
</ItemTemplate>
</asp:Repeater>
</div><br />
                    <asp:GridView ID="gvClient" runat="server" DataSourceID="SqlDSClient" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False"
                       CellPadding="4" GridLines="None" Font-Size="15px" Width="95%" OnSelectedIndexChanged="gvClient_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
            <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
            <ControlStyle Width="5%" />

            <HeaderStyle HorizontalAlign="Left" />

            <ItemStyle Width="5%" />
            </asp:CommandField>
            <asp:BoundField DataField="AccountID" HeaderText="Account Id" SortExpression="AccountID" >
            <ControlStyle Width="8%" />

            <HeaderStyle Wrap="False" HorizontalAlign="Left" />

            <ItemStyle Width="8%" />
            </asp:BoundField>
                <asp:BoundField DataField="ID" HeaderText="ID">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            <asp:BoundField DataField="LocationID" HeaderText="Location ID">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Wrap="False" HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="ServiceName" HeaderText="Service Name" SortExpression="Name">
            <ControlStyle Width="30%" />

            <HeaderStyle HorizontalAlign="Left" />

            <ItemStyle Width="30%" Wrap="False" HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="ServiceAddress1" HeaderText="Service Address" >
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
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
            <asp:SqlDataSource ID="SqlDSClient" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
        </div>
    </asp:Panel>

       <asp:Panel ID="pnlPopUpTeam" runat="server" BackColor="White" Width="700px" Height="80%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
                       <table><tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Team</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlTeamClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr><tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;padding-left:40px;">&nbsp;Search &nbsp; <asp:TextBox ID="txtPopUpTeam" runat="server" MaxLength="50" Height="16px" Width="400px" ForeColor="Gray" onblur = "WaterMarkTeam(this, event);" onfocus = "WaterMarkTeam(this, event);" AutoPostBack="True">Search Here for Team or In-ChargeId</asp:TextBox>
    <asp:ImageButton ID="btnPopUpTeamSearch" OnClick="btnPopUpTeamSearch_Click" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle" Visible="False" /><asp:ImageButton ID="btnPopUpTeamReset" OnClick="btnPopUpTeamReset_Click" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/></td><td>
                               <asp:TextBox ID="txtPopupTeamSearch" runat="server" Width="20%" Visible="False"></asp:TextBox>
                           </td></tr></table><div style="text-align: center; padding-left: 50px; padding-bottom: 5px;"><div class="AlphabetPager">
    <asp:Repeater ID="rptrTeam" runat="server" Visible="False"><ItemTemplate><asp:LinkButton ID="lbtnTeam" runat="server" Text='<%#Eval("Value")%>' OnClick="TeamAlphabet_Click" ForeColor="Black" /></ItemTemplate></asp:Repeater></div><br />
        
        <asp:GridView ID="gvTeam" runat="server" DataSourceID="SqlDSTeam" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
                                    CellPadding="2" GridLines="None" Width="90%"><AlternatingRowStyle BackColor="White" ForeColor="#284775" /><Columns><asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True"><ControlStyle Width="50px" />
        <HeaderStyle HorizontalAlign="Left" />
        <ItemStyle Width="8%" /></asp:CommandField><asp:BoundField DataField="TeamID" HeaderText="Id" SortExpression="TeamID"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField><asp:BoundField DataField="TeamName" HeaderText="Name" SortExpression="TeamName"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField><asp:BoundField DataField="InChargeId" HeaderText="InCharge Id" SortExpression="InChargeId"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField>
                <asp:BoundField DataField="Supervisor" HeaderText="Supervisor" />
            </Columns><EditRowStyle BackColor="#999999" /><FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" /><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#E9E7E2" /><SortedAscendingHeaderStyle BackColor="#506C8C" /><SortedDescendingCellStyle BackColor="#FFFDF8" /><SortedDescendingHeaderStyle BackColor="#6F8DAE" /></asp:GridView><asp:SqlDataSource ID="SqlDSTeam" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource></div>

          </asp:Panel>

   

         <asp:Panel ID="pnlPopUpStaff" runat="server" BackColor="White" Width="700px" Height="80%" BorderColor="#003366" BorderWidth="1px"
        HorizontalAlign="Left">
        
                       <table><tr><td colspan="2" style="text-align:center;"><h4 style="color: #000000">Staff</h4></td><td style="width:1%;text-align:right;"><asp:ImageButton ID="btnPnlStaffClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td></tr><tr><td colspan="2" style="width:10%;font-size:15px;font-weight:bold;font-family:Calibri;color:black;text-align:left;padding-left:40px;">&nbsp;Search &nbsp; <asp:TextBox ID="txtPopupStaff" runat="server" MaxLength="50" Height="16px" Width="400px" ForeColor="Gray" onblur = "WaterMarkStaff(this, event);" onfocus = "WaterMarkStaff(this, event);" AutoPostBack="True">Search Here for Staff </asp:TextBox>
    <asp:ImageButton ID="btnPopUpStaffSearch" OnClick="btnPopUpStaffSearch_Click" runat="server" ImageUrl="~/Images/searchbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle" Visible="False" /><asp:ImageButton ID="btnPopUpStaffReset" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/></td><td>
                               <asp:TextBox ID="txtPopupStaffSearch" runat="server" Visible="False" Width="20%"></asp:TextBox>
                           </td></tr></table><div style="text-align: center; padding-left: 50px; padding-bottom: 5px;"><div class="AlphabetPager">
    <asp:Repeater ID="rptrStaff" runat="server" Visible="False"><ItemTemplate><asp:LinkButton ID="lbtnStaff" runat="server" Text='<%#Eval("Value")%>' OnClick="StaffAlphabet_Click" ForeColor="Black" /></ItemTemplate></asp:Repeater></div><br />
        
        <asp:GridView ID="gvStaff" runat="server" DataSourceID="SqlDSStaff" ForeColor="#333333" AllowPaging="True" AutoGenerateColumns="False" Font-Size="15px"
                                    CellPadding="2" GridLines="None" Width="80%"><AlternatingRowStyle BackColor="White" ForeColor="#284775" /><Columns><asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True"><ControlStyle Width="50px" />
        <HeaderStyle HorizontalAlign="Left" />
        <ItemStyle Width="8%" /></asp:CommandField><asp:BoundField DataField="StaffId" HeaderText="Id" SortExpression="StaffId"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField><asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField><asp:BoundField DataField="NRIC" HeaderText="NRIC" SortExpression="NRIC"><ControlStyle Width="150px" /><HeaderStyle Width="25%" HorizontalAlign="Left" /><ItemStyle Width="25%" Wrap="False" /></asp:BoundField></Columns><EditRowStyle BackColor="#999999" /><FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Names="Calibri" /><PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" Font-Names="Calibri" /><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#E9E7E2" /><SortedAscendingHeaderStyle BackColor="#506C8C" /><SortedDescendingCellStyle BackColor="#FFFDF8" /><SortedDescendingHeaderStyle BackColor="#6F8DAE" /></asp:GridView><asp:SqlDataSource ID="SqlDSStaff" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource></div>

          </asp:Panel>

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
                             <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/resetbutton.jpg" Height="22px" Width="24px" ImageAlign="Middle"/>
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
            <asp:SqlDataSource ID="SqlDSContractNo" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"></asp:SqlDataSource>
        </div>
    </asp:Panel>
    
           <%--     </ContentTemplate>
                     </asp:UpdatePanel>--%>
            </div>
      </ContentTemplate>
        
</asp:UpdatePanel>
    </asp:Content>
