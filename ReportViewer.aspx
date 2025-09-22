<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportViewer.aspx.vb" Inherits="ReportViewer" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report</title>
     <script src='<%=ResolveUrl("~/crystalreportviewers13/js/crviewer/crv.js")%>' type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
           
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="CrystalReport.rpt">
            </Report>
        </CR:CrystalReportSource>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ReportSourceID="CrystalReportSource1" ToolPanelView="None" DisplayToolbar="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False" />
           
    </div>
    </form>
</body>
</html>
