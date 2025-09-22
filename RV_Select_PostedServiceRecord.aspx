<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage_Report.master" AutoEventWireup="false" CodeFile="RV_Select_PostedServiceRecord.aspx.vb" Inherits="RV_Select_PostedServiceRecord" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
           <ControlBundles>   
                    <asp:controlBundle name="ModalPopupExtender_Bundle"/>
        <asp:ControlBundle Name="CalendarExtender_Bundle" />
                     
   </ControlBundles>
    </asp:ToolkitScriptManager>  
       <h5 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">POSTED SERVICE RECORD LISTING - DETAIL</h5>
      
</asp:Content>

