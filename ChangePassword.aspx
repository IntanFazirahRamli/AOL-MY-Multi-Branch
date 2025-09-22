<%@ Page Title="Change Password" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ChangePassword.aspx.vb" Inherits="ChangePassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <asp:UpdateProgress ID="updateProgress" runat="server">
            <ProgressTemplate>
               <div class="loading-panel">
                    <div class="loading-container">
                        <img src="<%= Me.ResolveUrl("~/images/logo_loader_new.gif")%>" />
                    </div>
                </div>
               
            </ProgressTemplate>
        </asp:UpdateProgress>
      <asp:UpdatePanel ID="updPanelCompany" runat="server" UpdateMode="Conditional">
          <ContentTemplate>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release">
            <ControlBundles>
                <asp:controlBundle name="ModalPopupExtender_Bundle"/> 
            </ControlBundles>
        </asp:ToolkitScriptManager>
         <asp:Label ID="lblUserID" runat="server" Text="Label" Visible="False"></asp:Label>
           <table style="width:100%;">
               <tr>
                    <td colspan="2" style="text-align:right;"> <asp:Button ID="Button2" runat="server"  CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="QUIT" Width="90px" Height="30px" />
               </td>
               </tr>
                          <tr>
                             <td colspan="2" style="text-align:center">
                                 <h4 style="font-family: Verdana, Geneva, Tahoma, sans-serif; font-weight: bold;COLOR:#000099;">Change Password</h4>
                             </td>
                         </tr>
                          <tr>
               <td colspan="2" style="width:100%;text-align:center;color:brown;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblMessagePswd" runat="server"></asp:Label>
                      </td> 
            </tr>
             <tr>
               <td colspan="2" style="width:100%;text-align:center;font-size:18px;font-weight:bold;font-family:'Calibri';"> 
                      <asp:Label ID="lblAlertPswd" runat="server" BackColor="Red" ForeColor="White"></asp:Label><br />
                      </td> 
            </tr>
                         
                         <tr>
                             <td class="CellFormat" style="width:50%">Old Password<asp:Label ID="Label5" runat="server" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label></td>
                             <td class="CellTextBox">
                                <asp:TextBox ID="txtOldPswd" runat="server" MaxLength="50" Height="16px" Width="30%" TextMode="Password"></asp:TextBox>
                                                   </td>
                         </tr>
                          <tr>
                               <td class="CellFormat">New Password<asp:Label ID="Label1" runat="server" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label>
                               </td>
                              <td class="CellTextBox" colspan="1">      <asp:TextBox ID="txtNewPswd" runat="server" MaxLength="50" Height="16px" Width="30%" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
                  </td>
                           </tr>
                            <tr>
                               <td class="CellFormat">Confirm New Password<asp:Label ID="Label2" runat="server" Font-Size="14px" ForeColor="Red" Text="*" Height="20px"></asp:Label>
                               </td>
                              <td class="CellTextBox" colspan="1">      <asp:TextBox ID="txtNewPswd2" runat="server" MaxLength="50" Height="16px" Width="30%" AutoCompleteType="Disabled" TextMode="Password"></asp:TextBox>
                  </td>
                           </tr>
                        
                         <tr>
                             <td colspan="2"><br /><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnSavePswd" runat="server" CssClass="button"  BackColor="#CFC6C0"  Font-Bold="True" Text="Update Password" Width="200px"/>
                                 <asp:Button ID="btnCancelPswd" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="Cancel" Width="100px" Visible="False" />
                               </td>
                         </tr>
                          
                 

        </table>
              <br /><br /><br /><br /><br />
        <asp:Panel ID="pnlConfirmPost" runat="server" BackColor="White" Width="400px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
           
                
               <tr>
                             <td><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          <div style="text-align:center; font-family:Calibri;font-weight:bold;">Password Updated Successfully!<br /><br />Please Login using your New Password!</div>
                        
                      </td>
                           </tr>
                            <tr>
                             <td><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td style="text-align:center"><asp:Button ID="btnConfirmYes" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px"/>
                              <asp:Button ID="Button1" runat="server" CssClass="dummybutton" />

            <%--<asp:Button ID="btnConfirmNo" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px" />--%>
                               </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupConfirmPost" runat="server" CancelControlID="Button1" PopupControlID="pnlConfirmPost" TargetControlID="btndummyPost" BackgroundCssClass="modalBackground" DynamicServicePath="" Enabled="True"></asp:ModalPopupExtender>
         <asp:Button ID="btndummyPost" runat="server" CssClass="dummybutton" />
                                     <asp:TextBox ID="txtPassword" runat="server" MaxLength="10" Height="16px" Width="50%" CssClass="dummybutton"></asp:TextBox>
                       </ContentTemplate>
          </asp:UpdatePanel>
</asp:Content>

