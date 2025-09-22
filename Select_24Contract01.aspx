<%@ Page Language="vb" AutoEventWireup="false" Codefile="Select_24Contract01.aspx.vb" 
    Inherits="Select_24Contract01" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .chkList input {
            margin-left: -20px;
        }

        .chkList td {
            padding-left: 30px;
            padding-right: 10px;
        }

        .CellTextBox {
            font-family: 'Calibri';
            color: black;
            text-align: left;
            padding-left: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <br />
            <asp:Panel ID="pnlServiceContractListing" runat="server" BackColor="White" Width="50%" Height="58%" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
                <table style="font-family: Calibri; font-size: 15px; font-weight: bold; color: black; text-align: left; padding-left: 20px; line-height: 25px;">
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <h5 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold; color: #800000; text-decoration: underline;">Service Contract Listing</h5>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblStatus" Text="Status" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBoxList ID="chkListStatus" runat="server" Width="100%" RepeatDirection="Horizontal" RepeatLayout="Table"
                                RepeatColumns="4" CellPadding="5" CellSpacing="2" TextAlign="Right" Enabled="True">
                                <asp:ListItem Value="O">O - Open</asp:ListItem>
                                <asp:ListItem Value="C">C - Completed</asp:ListItem>
                                <asp:ListItem Value="V">V - Void</asp:ListItem>
                                <asp:ListItem Value="P">P - Post</asp:ListItem>
                                <asp:ListItem Value="R">R - Revised</asp:ListItem>
                                <asp:ListItem Value="H">H - On Hold</asp:ListItem>
                                <asp:ListItem Value="T">T - Terminated</asp:ListItem>
                                <asp:ListItem Value="X">X - Cancelled</asp:ListItem>
                                <asp:ListItem Value="E">E - Early Complete</asp:ListItem>
                                <asp:ListItem Value="ALL">All Status</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblRenewalSt" Text="Renewal Status" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBoxList ID="chkListRenewalSt" runat="server" RepeatDirection="Horizontal"
                                CssClass="chkList">
                                <asp:ListItem Text="Open" Value="O" />
                                <asp:ListItem Text="Renewal" Value="R" />
                                <asp:ListItem Text="No Follow" Value="NF" />
                                <asp:ListItem Text="Declined" Value="D" />
                                <asp:ListItem Text="All Renewal Status" Value="All" />
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblNotedSt" Text="Noted Status" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBoxList ID="chkListNotedSt" runat="server" RepeatDirection="Horizontal"
                                CssClass="chkList">
                                <asp:ListItem Text="Open" Value="O" />
                                <asp:ListItem Text="Noted" Value="N" />
                                <asp:ListItem Text="All Noted Status" Value="All" />
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblSettlementType" Text="Settlement Type" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBoxList ID="chkListSettlementType" runat="server" RepeatDirection="Horizontal"
                                CssClass="chkList">
                                <asp:ListItem Text="Under Contract" Value="UC" />
                                <asp:ListItem Text="Offset Contract" Value="OC" />
                                <asp:ListItem Text="Contract Billing" Value="CB" />
                                <asp:ListItem Text="All Settle Status" Value="All" />
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblCompanyGrp" runat="server" Text="Company Group"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCompanyGrp" runat="server" Height="16px" Width="150px" AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:ModalPopupExtender ID="mdlCompanyGrp" runat="server" CancelControlID="btnPnlCompanyGrpClose" PopupControlID="pnlPopUpCompanyGrp"
                                TargetControlID="imgBtnCompanyGrp" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:ImageButton ID="imgBtnCompanyGrp" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                Height="15" Width="20" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblContractGp" runat="server" Text="Contract Group"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtContractGrp" runat="server" Height="16px" Width="150px" AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:ModalPopupExtender ID="mdlContractGrp" runat="server" CancelControlID="btnPnlContractGrpClose" PopupControlID="pnlPopUpContractGrp"
                                TargetControlID="imgBtnContractGrp" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:ImageButton ID="imgBtnContractGrp" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                Height="15" Width="20" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblClient" runat="server" Text="Client Type/Id"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtClientType" runat="server" Height="16px" Width="150px" AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:ModalPopupExtender ID="mdlPopUpClient" runat="server" CancelControlID="btnPnlClientClose" PopupControlID="pnlPopUpClient"
                                TargetControlID="imgbtnClient" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:ImageButton ID="imgbtnClient" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg" Height="15" Width="20" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtClientId" runat="server" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblClientName" runat="server" Text="Client Name"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtClientName" runat="server" Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblSalesman" runat="server" Text="Salesman"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSalesman" runat="server" Height="16px" Width="150px" AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:ModalPopupExtender ID="mdlPopUpSalesman" runat="server" CancelControlID="btnPnlSalesmanClose" PopupControlID="pnlPopUpSalesman"
                                TargetControlID="imgBtnSalesman" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:ImageButton ID="imgBtnSalesman" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                Height="15" Width="20" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblScheduler" runat="server" Text="Scheduler"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtScheduler" runat="server" Height="16px" Width="150px" AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:ModalPopupExtender ID="mdlPopUpScheduler" runat="server" CancelControlID="btnPnlSchedulerClose" PopupControlID="pnlPopUpScheduler"
                                TargetControlID="imgBtnScheduler" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:ImageButton ID="imgBtnScheduler" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                Height="15" Width="20" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblIncharge" runat="server" Text="Incharge"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIncharge" runat="server" Height="16px" Width="150px" AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:ModalPopupExtender ID="mdlPopUpIncharge" runat="server" CancelControlID="btnPnlInchargeClose" PopupControlID="pnlPopUpIncharge"
                                TargetControlID="imgBtnIncharge" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:ImageButton ID="imgBtnIncharge" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                Height="15" Width="20" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblTechSupport" runat="server" Text="Technical Support"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTechSupport" runat="server" Height="16px" Width="150px" AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:ModalPopupExtender ID="mdlPopUpTechSupport" runat="server" CancelControlID="btnPnlTechSupportClose" PopupControlID="pnlPopUpTechSupport"
                                TargetControlID="imgBtnTechSupport" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:ImageButton ID="imgBtnTechSupport" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                Height="15" Width="20" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblServiceId" runat="server" Text="Service Id"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtServiceId" runat="server" Height="16px" Width="150px" AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:ModalPopupExtender ID="mdlPopUpServiceId" runat="server" CancelControlID="btnPnlServiceIdClose" PopupControlID="pnlPopUpServiceId"
                                TargetControlID="imgBtnServiceId" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                            <asp:ImageButton ID="imgBtnServiceId" runat="server" CssClass="righttextbox" ImageUrl="~/Images/searchbutton.jpg"
                                Height="15" Width="20" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblContractDate" runat="server" Text="Contract Date"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtContractDt" runat="server" MaxLength="50" Height="16px" Width="150px"
                                AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:CalendarExtender ID="cal" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtContractDt" TargetControlID="txtContractDt" />
                            &nbsp;&nbsp; To
                        </td>
                        <%--<td>
                        <asp:Label ID="lblContractDateTo" runat="server" Text="To"></asp:Label>
                    </td>--%>
                        <td>
                            <asp:TextBox ID="txtContractDtTo" runat="server" MaxLength="50" Height="16px" Width="150px"
                                AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:CalendarExtender ID="cal1" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtContractDtTo" TargetControlID="txtContractDtTo" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblStartDate" runat="server" Text="Start Date"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStartDt" runat="server" MaxLength="50" Height="16px" Width="150px"
                                AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:CalendarExtender ID="cal3" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtStartDt" TargetControlID="txtStartDt" />
                            &nbsp;&nbsp; To
                        </td>
                        <%--<td>
                        <asp:Label ID="lblStartDateTo" runat="server" Text="To"></asp:Label>
                    </td>--%>
                        <td>
                            <asp:TextBox ID="txtStartDtTo" runat="server" MaxLength="50" Height="16px" Width="150px"
                                AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:CalendarExtender ID="cal4" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtStartDtTo" TargetControlID="txtStartDtTo" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblEndDt" runat="server" Text="End Date"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEndDt" runat="server" MaxLength="50" Height="16px" Width="150px"
                                AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:CalendarExtender ID="cal5" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtEndDt" TargetControlID="txtEndDt" />
                            &nbsp;&nbsp; To
                        </td>
                        <%--<td colspan="2">
                        <asp:Label ID="lblEndDtTo" runat="server" Text="To"></asp:Label>
                    </td>--%>
                        <td>
                            <asp:TextBox ID="txtEndDtTo" runat="server" MaxLength="50" Height="16px" Width="150px"
                                AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:CalendarExtender ID="cal6" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtEndDtTo" TargetControlID="txtEndDtTo" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblActualEnd" runat="server" Text="Actual End"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtActualEnd" runat="server" MaxLength="50" Height="16px" Width="150px"
                                AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:CalendarExtender ID="cal7" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtActualEnd" TargetControlID="txtActualEnd" />
                            &nbsp;&nbsp; To
                        </td>
                        <%--<td colspan="2">
                        <asp:Label ID="lblActualEndTo" runat="server" Text="To"></asp:Label>
                    </td>--%>
                        <td>
                            <asp:TextBox ID="txtActualEndTo" runat="server" MaxLength="50" Height="16px" Width="150px"
                                AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:CalendarExtender ID="cal8" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtActualEndTo" TargetControlID="txtActualEndTo" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td colspan="2" style="width: 130px;">
                            <asp:Label ID="lblSortBy" runat="server" Text="Sort By"></asp:Label>
                        </td>
                        <td>
                            <asp:ListBox ID="listSort1" runat="server" Height="120px" Width="150px" SelectionMode="Multiple">
                                <asp:ListItem Text="Client Id" Value="CustCode"></asp:ListItem>
                                <asp:ListItem Text="Client Name" Value="CustName"></asp:ListItem>
                                <asp:ListItem Text="Contract Date" Value="ContractDate"></asp:ListItem>
                                <asp:ListItem Text="Salesman" Value="Salesman"></asp:ListItem>
                                <asp:ListItem Text="Scheduler" Value="Scheduler"></asp:ListItem>
                                <asp:ListItem Text="End Date" Value="EndDate"></asp:ListItem>
                            </asp:ListBox>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSortMove" runat="server" OnClick="btnSortMove_Click" Text=">>" />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSortRemove" runat="server" OnClick="btnSortRemove_Click" Text="<<" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:ListBox ID="listSort2" runat="server" SelectionMode="Multiple"
                                AppendDataBoundItems="true" Height="120px" Width="150px"></asp:ListBox>
                        </td>
                    </tr>
                </table>

                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" />
                            </td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                            </td>
                            <td></td>
                            <td>
                                <asp:Button ID="btnQuit" runat="server" Text="Quit" OnClick="btnQuit_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>

        </div>

        <asp:Panel ID="pnlPopUpCompanyGrp" runat="server" BackColor="White" Width="400" Height="400" BorderColor="#003366" BorderWidth="1"
            HorizontalAlign="Left" ScrollBars="None">
            <table>
                <tr>
                    <td style="text-align: center;">
                        <h4 style="color: #000000">Company Group</h4>
                    </td>
                    <td style="width: 1%; text-align: right;">
                        <asp:ImageButton ID="btnPnlCompanyGrpClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
                </tr>
            </table>

            <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
                <br />
                <asp:GridView ID="gvCompanyGrp" runat="server" DataSourceID="SqlDSCompanyGrp" ForeColor="#333333" AllowPaging="true" AutoGenerateColumns="False"
                    CellPadding="4" GridLines="None" Width="300px" RowStyle-HorizontalAlign="Left" PageSize="12" OnSelectedIndexChanged="gvCompanyGrp_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                            <ControlStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="CompanyGroup" HeaderText="Company Group" SortExpression="CompanyGroup">
                            <ControlStyle Width="120px" />
                            <HeaderStyle Width="120px" />
                            <ItemStyle Width="120px" />
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
                <asp:SqlDataSource ID="SqlDSCompanyGrp" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                    ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                    SelectCommand="SELECT distinct * From tblCompanyGroup where (Rcno &lt;&gt; 0) order by CompanyGroup"></asp:SqlDataSource>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlPopUpContractGrp" runat="server" BackColor="White" Width="400" Height="500" BorderColor="#003366" BorderWidth="1"
            HorizontalAlign="Left" ScrollBars="None">
            <table>
                <tr>
                    <td style="text-align: center;">
                        <h4 style="color: #000000">Contract Group</h4>
                    </td>
                    <td style="width: 1%; text-align: right;">
                        <asp:ImageButton ID="btnPnlContractGrpClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
                </tr>
            </table>

            <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
                <br />
                <asp:GridView ID="gvContractGrp" runat="server" DataSourceID="SqlDSContractGrp" ForeColor="#333333" AllowPaging="true" AutoGenerateColumns="False"
                    CellPadding="4" GridLines="None" Width="300px" RowStyle-HorizontalAlign="Left" PageSize="12" OnSelectedIndexChanged="gvContractGrp_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                            <ControlStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="ContractGroup" HeaderText="Contract Group" SortExpression="ContractGroup">
                            <ControlStyle Width="120px" />
                            <HeaderStyle Width="120px" />
                            <ItemStyle Width="120px" />
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
                <asp:SqlDataSource ID="SqlDSContractGrp" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                    ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                    SelectCommand="SELECT distinct * From tblContractGroup where (Rcno &lt;&gt; 0) order by ContractGroup"></asp:SqlDataSource>
            </div>
        </asp:Panel>

        <%--  <asp:Panel ID="pnlPopUpClient" runat="server" BackColor="White" Width="900" Height="600" BorderColor="#003366" BorderWidth="1"
            HorizontalAlign="Left" ScrollBars="None">
            <table>
                <tr>
                    <td style="text-align: center;">
                        <h4 style="color: #000000">Client</h4>
                    </td>
                    <td style="width: 1%; text-align: right;">
                        <asp:ImageButton ID="btnPnlClientClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
                </tr>
            </table>

            <div class="wrp">
                <div class="frm">
                    <table style="text-align: center;">
                        <tr>
                            <td style="font-size: 15px; font-weight: bold; font-family: 'Comic Sans MS'; color: black; text-align: left; padding-left: 40px;">&nbsp;Search Client Name</td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtPopUpClient" runat="server" AutoCompleteType="Disabled" Width="200px"></asp:TextBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:Button ID="btnPopUpClientSearch" runat="server" CssClass="button" BackColor="#CFC6C0" Font-Bold="True" Text="SEARCH" Width="80px" Visible="true"
                                    OnClick="btnPopUpClientSearch_Click" />
                            </td>
                            <td style="text-align: left;">
                                <asp:Button ID="btnPopUpClientReset" runat="server" CssClass="auto-style8" BackColor="#CFC6C0" Font-Bold="True" Text="RESET" Width="80px" Visible="true"
                                    OnClick="btnPopUpClientReset_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <br />
            <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
                <br />
                <asp:GridView ID="gvClient" runat="server" DataSourceID="SqlDSClient" ForeColor="#333333" AllowPaging="true" AutoGenerateColumns="False"
                    CellPadding="4" GridLines="None" Width="700px" RowStyle-HorizontalAlign="Left" PageSize="12" OnSelectedIndexChanged="gvClient_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                            <ControlStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="ContID" HeaderText="Client Id" SortExpression="ContID">
                            <ControlStyle Width="120px" />
                            <HeaderStyle Width="120px" />
                            <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ContName" HeaderText="Name" SortExpression="ContName">
                            <ControlStyle Width="300px" />
                            <HeaderStyle Width="300px" />
                            <ItemStyle Width="300px" Wrap="true" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ContType" HeaderText="Client Type" SortExpression="ContType">
                            <ControlStyle Width="120px" />
                            <HeaderStyle Width="120px" />
                            <ItemStyle Width="120px" />
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
                <asp:SqlDataSource ID="SqlDSClient" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                    ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                    SelectCommand="SELECT distinct * From tblContactMaster where (Rcno &lt;&gt; 0) order by ContName"></asp:SqlDataSource>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlPopUpSalesman" runat="server" BackColor="White" Width="900" Height="600" BorderColor="#003366" BorderWidth="1"
            HorizontalAlign="Left" ScrollBars="None">
            <table>
                <tr>
                    <td style="text-align: center;">
                        <h4 style="color: #000000">Salesman</h4>
                    </td>
                    <td style="width: 1%; text-align: right;">
                        <asp:ImageButton ID="btnPnlSalesmanClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
                </tr>
            </table>

            <div class="wrp">
                <div class="frm">
                    <table style="text-align: center;">
                        <tr>
                            <td style="font-size: 15px; font-weight: bold; font-family: 'Comic Sans MS'; color: black; text-align: left; padding-left: 40px;">&nbsp;Search Client Name</td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtPopUpSalesman" runat="server" AutoCompleteType="Disabled" Width="200px"></asp:TextBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:Button ID="btnPopUpSalesmanSearch" runat="server" CssClass="button" BackColor="#CFC6C0" Font-Bold="True" Text="SEARCH" Width="80px" Visible="true"
                                    OnClick="btnPopUpSalesmanSearch_Click" />
                            </td>
                            <td style="text-align: left;">
                                <asp:Button ID="btnPopUpSalesmanReset" runat="server" CssClass="auto-style8" BackColor="#CFC6C0" Font-Bold="True" Text="RESET" Width="80px" Visible="true"
                                    OnClick="btnPopUpSalesmanReset_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <br />
            <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
                <br />
                <asp:GridView ID="gvSalesman" runat="server" DataSourceID="SqlDSSalesman" ForeColor="#333333" AllowPaging="true" AutoGenerateColumns="False"
                    CellPadding="4" GridLines="None" Width="700px" RowStyle-HorizontalAlign="Left" PageSize="12" OnSelectedIndexChanged="gvSalesman_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                            <ControlStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="StaffId" HeaderText="Staff Id" SortExpression="StaffId">
                            <ControlStyle Width="200px" />
                            <HeaderStyle Width="120px" />
                            <ItemStyle Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                            <ControlStyle Width="200px" />
                            <HeaderStyle Width="200px" />
                            <ItemStyle Width="200px" Wrap="true" />
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
                <asp:SqlDataSource ID="SqlDSSalesman" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                    ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                    SelectCommand="SELECT distinct * From tblStaff where (Rcno &lt;&gt; 0) order by StaffId"></asp:SqlDataSource>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlPopUpScheduler" runat="server" BackColor="White" Width="900" Height="600" BorderColor="#003366" BorderWidth="1"
            HorizontalAlign="Left" ScrollBars="None">
            <table>
                <tr>
                    <td style="text-align: center;">
                        <h4 style="color: #000000">Scheduler</h4>
                    </td>
                    <td style="width: 1%; text-align: right;">
                        <asp:ImageButton ID="btnPnlSchedulerClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
                </tr>
            </table>

            <div class="wrp">
                <div class="frm">
                    <table style="text-align: center;">
                        <tr>
                            <td style="font-size: 15px; font-weight: bold; font-family: 'Comic Sans MS'; color: black; text-align: left; padding-left: 40px;">&nbsp;Search Client Name</td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtPopUpScheduler" runat="server" AutoCompleteType="Disabled" Width="200px"></asp:TextBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:Button ID="btnPopUpSchedulerSearch" runat="server" CssClass="button" BackColor="#CFC6C0" Font-Bold="True" Text="SEARCH" Width="80px" Visible="true"
                                    OnClick="btnPopUpSchedulerSearch_Click" />
                            </td>
                            <td style="text-align: left;">
                                <asp:Button ID="btnPopUpSchedulerReset" runat="server" CssClass="auto-style8" BackColor="#CFC6C0" Font-Bold="True" Text="RESET" Width="80px" Visible="true"
                                    OnClick="btnPopUpSchedulerReset_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <br />
            <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
                <br />
                <asp:GridView ID="gvScheduler" runat="server" DataSourceID="SqlDSScheduler" ForeColor="#333333" AllowPaging="true" AutoGenerateColumns="False"
                    CellPadding="4" GridLines="None" Width="700px" RowStyle-HorizontalAlign="Left" PageSize="12" OnSelectedIndexChanged="gvScheduler_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                            <ControlStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="StaffId" HeaderText="Staff Id" SortExpression="StaffId">
                            <ControlStyle Width="200px" />
                            <HeaderStyle Width="200px" />
                            <ItemStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                            <ControlStyle Width="200px" />
                            <HeaderStyle Width="200px" />
                            <ItemStyle Width="200px" Wrap="true" />
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
                <asp:SqlDataSource ID="SqlDSScheduler" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                    ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                    SelectCommand="SELECT distinct * From tblStaff where (Rcno &lt;&gt; 0) order by StaffId"></asp:SqlDataSource>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlPopUpTechSupport" runat="server" BackColor="White" Width="700" Height="600" BorderColor="#003366" BorderWidth="1"
            HorizontalAlign="Left" ScrollBars="None">
            <table>
                <tr>
                    <td style="text-align: center;">
                        <h4 style="color: #000000">Technical Support</h4>
                    </td>
                    <td style="width: 1%; text-align: right;">
                        <asp:ImageButton ID="btnPnlTechSupportClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
                </tr>
            </table>

            <div class="wrp">
                <div class="frm">
                    <table style="text-align: center;">
                        <tr>
                            <td style="font-size: 15px; font-weight: bold; font-family: 'Comic Sans MS'; color: black; text-align: left; padding-left: 40px;">&nbsp;Search Client Name</td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtPopUpTechSupport" runat="server" AutoCompleteType="Disabled" Width="200px"></asp:TextBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:Button ID="btnPopUpTechSupportSearch" runat="server" CssClass="button" BackColor="#CFC6C0" Font-Bold="True" Text="SEARCH" Width="80px" Visible="true"
                                    OnClick="btnPopUpTechSupportSearch_Click" />
                            </td>
                            <td style="text-align: left;">
                                <asp:Button ID="btnPopUpTechSupportReset" runat="server" CssClass="auto-style8" BackColor="#CFC6C0" Font-Bold="True" Text="RESET" Width="80px" Visible="true"
                                    OnClick="btnPopUpTechSupportReset_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <br />
            <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
                <br />
                <asp:GridView ID="gvTechSupport" runat="server" DataSourceID="SqlDSTechSupport" ForeColor="#333333" AllowPaging="true" AutoGenerateColumns="False"
                    CellPadding="4" GridLines="None" Width="700px" RowStyle-HorizontalAlign="Left" PageSize="12" OnSelectedIndexChanged="gvTechSupport_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                            <ControlStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="StaffId" HeaderText="Staff Id" SortExpression="StaffId">
                            <ControlStyle Width="200px" />
                            <HeaderStyle Width="200px" />
                            <ItemStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                            <ControlStyle Width="200px" />
                            <HeaderStyle Width="200px" />
                            <ItemStyle Width="200px" Wrap="true" />
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
                <asp:SqlDataSource ID="SqlDSTechSupport" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                    ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                    SelectCommand="SELECT distinct * From tblStaff where (Rcno &lt;&gt; 0) order by StaffId"></asp:SqlDataSource>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlPopUpIncharge" runat="server" BackColor="White" Width="500" Height="600" BorderColor="#003366" BorderWidth="1"
            HorizontalAlign="Left" ScrollBars="None">
            <table>
                <tr>
                    <td style="text-align: center;">
                        <h4 style="color: #000000">Incharge</h4>
                    </td>
                    <td style="width: 1%; text-align: right;">
                        <asp:ImageButton ID="btnPnlInchargeClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
                </tr>
            </table>

            <div class="wrp">
                <div class="frm">
                    <table style="text-align: center;">
                        <tr>
                            <td style="font-size: 15px; font-weight: bold; font-family: 'Comic Sans MS'; color: black; text-align: left; padding-left: 40px;">&nbsp;Search Client Name</td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtPopUpIncharge" runat="server" AutoCompleteType="Disabled" Width="200px"></asp:TextBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:Button ID="btnPopUpInchargeSearch" runat="server" CssClass="button" BackColor="#CFC6C0" Font-Bold="True" Text="SEARCH" Width="80px" Visible="true"
                                    OnClick="btnPopUpInchargeSearch_Click" />
                            </td>
                            <td style="text-align: left;">
                                <asp:Button ID="btnPopUpInchargeReset" runat="server" CssClass="auto-style8" BackColor="#CFC6C0" Font-Bold="True" Text="RESET" Width="80px" Visible="true"
                                    OnClick="btnPopUpInchargeReset_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <br />
            <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
                <br />
                <asp:GridView ID="gvIncharge" runat="server" DataSourceID="SqlDSIncharge" ForeColor="#333333" AllowPaging="true" AutoGenerateColumns="False"
                    CellPadding="4" GridLines="None" Width="700px" RowStyle-HorizontalAlign="Left" PageSize="12" OnSelectedIndexChanged="gvIncharge_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                            <ControlStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="InchargeId" HeaderText="Incharge" SortExpression="InchargeId">
                            <ControlStyle Width="200px" />
                            <HeaderStyle Width="200px" />
                            <ItemStyle Width="200px" />
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
                <asp:SqlDataSource ID="SqlDSIncharge" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                    ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                    SelectCommand="SELECT distinct * From tblTeam where (Rcno &lt;&gt; 0) order by InchargeId"></asp:SqlDataSource>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlPopUpServiceId" runat="server" BackColor="White" Width="500" Height="600" BorderColor="#003366" BorderWidth="1"
            HorizontalAlign="Left" ScrollBars="None">
            <table>
                <tr>
                    <td style="text-align: center;">
                        <h4 style="color: #000000">Service Id</h4>
                    </td>
                    <td style="width: 1%; text-align: right;">
                        <asp:ImageButton ID="btnPnlServiceIdClose" runat="server" ImageUrl="~/Images/closebutton.png" Height="27px" Width="30px" /></td>
                </tr>
            </table>

            <div class="wrp">
                <div class="frm">
                    <table style="text-align: center;">
                        <tr>
                            <td style="font-size: 15px; font-weight: bold; font-family: 'Comic Sans MS'; color: black; text-align: left; padding-left: 40px;">&nbsp;Search Client Name</td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtPopUpServiceId" runat="server" AutoCompleteType="Disabled" Width="200px"></asp:TextBox>
                            </td>
                            <td style="text-align: left;">
                                <asp:Button ID="btnPopUpServiceIdSearch" runat="server" CssClass="button" BackColor="#CFC6C0" Font-Bold="True" Text="SEARCH" Width="80px" Visible="true"
                                    OnClick="btnPopUpServiceIdSearch_Click" />
                            </td>
                            <td style="text-align: left;">
                                <asp:Button ID="btnPopUpServiceIdReset" runat="server" CssClass="auto-style8" BackColor="#CFC6C0" Font-Bold="True" Text="RESET" Width="80px" Visible="true"
                                    OnClick="btnPopUpServiceIdReset_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <br />
            <div style="text-align: center; padding-left: 50px; padding-bottom: 5px;">
                <br />
                <asp:GridView ID="gvServiceId" runat="server" DataSourceID="SqlDSServiceId" ForeColor="#333333" AllowPaging="true" AutoGenerateColumns="False"
                    CellPadding="4" GridLines="None" Width="700px" RowStyle-HorizontalAlign="Left" PageSize="12" OnSelectedIndexChanged="gvServiceId_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField HeaderText="Select" ShowHeader="True" ShowSelectButton="True">
                            <ControlStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:CommandField>
                        <asp:BoundField DataField="ProductId" HeaderText="Service Id" SortExpression="ProductId">
                            <ControlStyle Width="200px" />
                            <HeaderStyle Width="200px" />
                            <ItemStyle Width="200px" />
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
                <asp:SqlDataSource ID="SqlDSServiceId" runat="server" ConnectionString="<%$ ConnectionStrings:sitadataConnectionString %>"
                    ProviderName="<%$ ConnectionStrings:sitadataConnectionString.ProviderName %>"
                    SelectCommand="SELECT distinct * From tblProduct where (Rcno &lt;&gt; 0) order by ProductId"></asp:SqlDataSource>
            </div>
        </asp:Panel>--%>
    </form>
</body>
</html>
