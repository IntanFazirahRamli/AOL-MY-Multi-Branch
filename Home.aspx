<%@ Page Title="AOL 2.0" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Home.aspx.vb" Inherits="Home" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

       

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
           <asp:controlBundle name="ModalPopupExtender_Bundle"/>
            <asp:controlBundle name="ListSearchExtender_Bundle"/>
               <asp:controlBundle name="TabContainer_Bundle"/>
                <asp:controlBundle name="CollapsiblePanelExtender_Bundle"/>
   </ControlBundles>
    </asp:ToolkitScriptManager>  
    <h1 style="color: #000000; text-align: center; line-height: 120px;  margin-left:200px;">
        Welcome!!!
  
  

          </h1>
    
<%--    <h1 style="color:red; text-align: center; line-height: 420px;">Modules are Under Construction!!!</h1>--%>
    
    <p style="color: #000000; text-align: center; line-height: 20px; margin-left:100px; ">&nbsp;

       
    </p>


</asp:Content>

