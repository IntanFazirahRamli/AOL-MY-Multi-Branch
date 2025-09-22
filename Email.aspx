<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" ValidateRequest="false" CodeFile="Email.aspx.vb" Inherits="Email" Title="SEND EMAIL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script type="text/javascript">
          function openInNewTab() {
              window.document.forms[0].target = '_blank';
              setTimeout(function () { window.document.forms[0].target = ''; }, 0);
          }
</script>
 
     <%-- <script type="text/javascript">
  www
tinymce.init({
    selector: 'div.editable',
    //inline: true,
    height: 500,
    width:1000,
  plugins: [
    'advlist autolink lists link image charmap print preview anchor',
    'searchreplace visualblocks code fullscreen',
    'insertdatetime media table contextmenu paste'
  ],
  toolbar: 'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
  content_css: [
    '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i',
    '//www.tinymce.com/css/codepen.min.css']
});
           </script>--%>
    <%-- <script type="text/javascript" src="//tinymce.cachefly.net/4.0/tinymce.min.js"></script>--%>
    <%--<script src="https://cdn.tiny.cloud/1/wbpm6kns0ldo5gwppgy23st8rypy6f0o5pq3c1l0d55ya0k3/tinymce/5/tinymce.min.js"></script>--%>
    <%--<script src="https://cdn.tiny.cloud/1/euzc1xqhv8isljf7dvnnzg8itui2oepgjfmx2u1dv6gl41dy/tinymce/5/tinymce.min.js"></script>--%>
    <%--<script src="https://cdn.tiny.cloud/1/euzc1xqhv8isljf7dvnnzg8itui2oepgjfmx2u1dv6gl41dy/tinymce/7/tinymce.min.js"></script>--%> 
      <%--<script src="Scripts/tinymce.min.js"></script>--%>
       <script src="https://cdn.tiny.cloud/1/9yu7due2brbbw6ypxj6scn4q7jv47veombchzb2gzhm926nk/tinymce/5/tinymce.min.js"></script>

     <script type="text/javascript">
        tinymce.init({
               mode: "textareas", editor_selector: 'editable', width: 800, height: 350
            //selector: 'textarea', width: 800, height: 350
        });
        //tinymce.init({ selector: 'textarea#txtContent', width: 800, height: 500 });
      
    </script>
     <style type="text/css">                                                                                                                                                                                                                                                                                     vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv                      

    .CellFormat{
        font-size:15px;
        font-weight:bold;
        font-family:'Calibri';
        color:black;
        text-align:left;
        width:10%;
     
          border-collapse: collapse;
              border-spacing: 0;
              line-height:10px;
    }
      .CellTextBox{
        
        color:black;
        text-align:left;
        /*width:20%;*/
    
    }
 
        .button {
            margin-right:10px;
            border-radius:1px; 
            box-shadow: 2px 2px 1px #808080; 
            height:30px;
            width:100px;
        }
        .roundbutton {
            border: 2px solid #a1a1a1;   
    background: #dddddd;   
    border-radius: 25px;
        }
        
          </style>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div style="height:1000px">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release" >
             <ControlBundles>
             <asp:controlBundle name="ModalPopupExtender_Bundle"/>
                  </ControlBundles>
        </asp:ToolkitScriptManager>
        <br /><br />
    <table style="width:100%">
        <tr>
            <td class="CellFormat">To</td>
            <td class="CellTextBox">
                <asp:TextBox ID="txtTo" runat="server" Width="100%" TextMode="MultiLine" Height="40px"></asp:TextBox>
                 <asp:TextBox ID="txtBranchLocation" runat="server" Width="100%" Visible="false"/>
            </td>
        </tr>
         <tr>
            <td class="CellFormat">CC</td>
            <td class="CellTextBox">
                <asp:TextBox ID="txtCC" runat="server" Width="100%" TextMode="MultiLine" Height="40px"></asp:TextBox></td>
        </tr>
     <%--    <tr>
            <td class="CellFormat">BCC</td>
            <td class="CellTextBox">
                <asp:TextBox ID="txtBCC" runat="server" Width="100%" TextMode="MultiLine" Height="40px"></asp:TextBox></td>
        </tr>--%>
         <tr>
            <td class="CellFormat">From</td>
            <td class="CellTextBox">
                <asp:TextBox ID="txtFrom" runat="server" Width="100%" TextMode="SingleLine" Height="16px" ReadOnly="true" Enabled="false"></asp:TextBox></td>
        </tr>
         <tr>
            <td class="CellFormat">Subject</td>
            <td class="CellTextBox">
                <asp:TextBox ID="txtSubject" runat="server" Width="100%" TextMode="singleline" Height="16px"></asp:TextBox></td>
        </tr>
         <tr>
            <td class="CellFormat">Content</td>
            <td class="CellTextBox">
            <%--    <asp:HtmlEditorExtender runat="server" TargetControlID="txtContent" EnableSanitization="false" DisplaySourceTab="True">
                    <Toolbar> 
              
                <asp:Bold />
                <asp:Italic />
                <asp:Underline />
             
                <asp:JustifyLeft />
                <asp:JustifyCenter />
                <asp:JustifyRight />
                <asp:JustifyFull />
              
                <asp:SelectAll />
                <asp:UnSelect />
                <asp:Delete />
              
                <asp:BackgroundColorSelector />
                <asp:ForeColorSelector />
                <asp:FontNameSelector />
                <asp:FontSizeSelector />
                <asp:Indent />
                <asp:Outdent />
              
            </Toolbar>
     
                </asp:HtmlEditorExtender>--%>
               <%--<textarea class="editable">--%>
                  <asp:TextBox ID="txtContent" runat="server" CssClass="editable" Width="100%" TextMode="MultiLine" Height="200px" Font-Bold="true"></asp:TextBox>
             <%-- </textarea>--%>
            </td>
        </tr>
         <tr>
            <td class="CellFormat">Attachment</td>
            <td id="tdAttach" runat="server" class="CellTextBox">
               <asp:LinkButton ID="lnkPreview" runat="server" Text="" /><asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Images/deletebutton1.jpg" Width="15px" Height="15px" />&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:LinkButton ID="lnkPreview1" runat="server" Text="" />&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:LinkButton ID="lnkPreview2" runat="server" Text="" /><asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Images/deletebutton1.jpg" Width="15px" Height="15px" />&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:LinkButton ID="lnkAttach1" runat="server" Text="" /><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/deletebutton1.jpg" Width="15px" Height="15px" />&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:LinkButton ID="lnkAttach2" runat="server" Text="" /><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/deletebutton1.jpg" Width="15px" Height="15px" />&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:LinkButton ID="lnkAttach3" runat="server" Text="" /><asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Images/deletebutton1.jpg" Width="15px" Height="15px" />&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:LinkButton ID="lnkAttach4" runat="server" Text="" /><asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Images/deletebutton1.jpg" Width="15px" Height="15px" />&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:LinkButton ID="lnkAttach5" runat="server" Text="" /><asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Images/deletebutton1.jpg" Width="15px" Height="15px" />&nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:LinkButton ID="lnkAttach6" runat="server" Text="" /><asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Images/deletebutton1.jpg" Width="15px" Height="15px" />&nbsp;&nbsp;&nbsp;&nbsp;

                      <asp:LinkButton ID="lnkAttach7" runat="server" Text="" /><asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Images/deletebutton1.jpg" Width="15px" Height="15px" />&nbsp;&nbsp;&nbsp;&nbsp;

                <%-- <asp:LinkButton ID="LinkButton1" runat="server"  OnClientClick="openInNewTab();" OnClick="PreviewFile" Text="" />--%>
                <asp:TextBox ID="txtAttachment" runat="server" Width="100%" TextMode="MultiLine" Height="80px" Visible="false"></asp:TextBox>
                 <%--<asp:FileUpload ID="fuAttachment" runat="server" Width="100%" />--%>
            </td>
        </tr>
          <tr>
            <td class="CellFormat">Additional Attachment</td>
            <td id="td1" runat="server" class="CellTextBox">
              <asp:FileUpload ID="FileUpload1" runat="server" Width="100%" CssClass="Centered" /><asp:Button ID="btnUpload" runat="server" Text="Add Attachment" OnClientClick="currentdatetime()" />
                 <%--<asp:FileUpload ID="fuAttachment" runat="server" Width="100%" />--%>
            </td>
        </tr>
        <tr>
            <td colspan="2"><br /></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center">
                <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True"/>
                &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True"/>
             
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height:1000px">
                <iframe runat="server" id ="iframeid" style="width:100%;height:100%"></iframe>
            </td>
        </tr>
    </table>
         <asp:Label ID="lblRecordNo" runat="server" Text="" Visible="false"></asp:Label>
           <asp:Label ID="lblServicedBy" runat="server" Text=""></asp:Label> 
          <asp:TextBox ID="txtQuery" runat="server" Visible="False"></asp:TextBox>
           <asp:TextBox ID="txtQuery1" runat="server" Visible="False"></asp:TextBox>
         <asp:TextBox ID="txtSvcRcno" runat="server" Visible="False"></asp:TextBox>
               <asp:Panel ID="pnlConfirmMsg" runat="server" BackColor="White" Width="300px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
            
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="lblQuery" runat="server" Text="Mail Sent Successfully"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnConfirmOk" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px"/>
                                 <asp:Button ID="btnCancelMsg" runat="server" CssClass="dummybutton" BackColor="#CFC6C0"  Font-Bold="True" Text="Ok" Width="100px"/>
                                </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupMsg" runat="server" CancelControlID="btnCancelMsg" PopupControlID="pnlConfirmMsg" TargetControlID="btndummyMsg" BackgroundCssClass="modalBackground" DynamicServicePath="" ></asp:ModalPopupExtender>
         <asp:Button ID="btndummyMsg" runat="server" CssClass="dummybutton" />

             <asp:Panel ID="pnlMsg" runat="server" BackColor="White" Width="300px" Height="130px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
            
                
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="lblMsg" runat="server" Text="Mail Sent Successfully"></asp:Label>
                        
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnOkMsg" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px"/>
                                 <asp:Button ID="btnCancelMsg1" runat="server" CssClass="dummybutton" BackColor="#CFC6C0"  Font-Bold="True" Text="Ok" Width="100px"/>
                                </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlMsg" runat="server" CancelControlID="btnCancelMsg1" PopupControlID="pnlMsg" TargetControlID="btndummMsg" BackgroundCssClass="modalBackground" DynamicServicePath="" ></asp:ModalPopupExtender>
         <asp:Button ID="btndummMsg" runat="server" CssClass="dummybutton" />


            <asp:Panel ID="pnlWarningMsg" runat="server" BackColor="White" Width="700px" Height="220px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
            
                  <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label45" runat="server" Text="Warning"></asp:Label>
                          
    
                      </td>
                           </tr>
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label1" runat="server" Text="The following email address/es are potentially invalid."></asp:Label>
                       <br /> <br />
                          <%--<asp:Label ID="Label2" runat="server" Text="Please update the customer contact details to ensure that there will be no errors."></asp:Label><br /><br />--%>
                      </td>
                           </tr>
              <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="lblWarningEmail" runat="server" Text=""></asp:Label><br /><br />
                          <asp:Label ID="lblToEmail" CssClass="dummybutton" Visible="false" runat="server" Text=""></asp:Label>
                        
                      </td>
                           </tr>
             <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label2" runat="server" Text="Please choose one of the following options."></asp:Label>
                       <br /> <br />
                          </td>
                 </tr>

                 <tr>
                      <td class="CellFormat" style="text-align:left;padding-left:100px; margin-left:auto; margin-right:auto;"">
           
                          <asp:RadioButtonList ID="rdbWarningOptions" runat="server">
                              <asp:ListItem>Ignore the warning and proceed to send the email.</asp:ListItem>
                              <asp:ListItem>Remove the invalid email addresses and send the email.</asp:ListItem>
                          </asp:RadioButtonList>
                          <%--<asp:Label ID="Label2" runat="server" Text="Please update the customer contact details to ensure that there will be no errors."></asp:Label><br /><br />--%>
                      </td>
                           </tr>
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnOkWarningMsg" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px"/>
                                 <asp:Button ID="btnCancelWarningMsg" runat="server" CssClass="dummybutton" BackColor="#CFC6C0"  Font-Bold="True" Text="Ok" Width="100px"/>
                                </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupWarningMsg" runat="server" CancelControlID="btnCancelWarningMsg" PopupControlID="pnlWarningMsg" TargetControlID="btndummWarningMsg" BackgroundCssClass="modalBackground" DynamicServicePath="" ></asp:ModalPopupExtender>
         <asp:Button ID="btndummWarningMsg" runat="server" CssClass="dummybutton" />


           <asp:Panel ID="pnlattchwarningMsg" runat="server" BackColor="White" Width="700px" Height="220px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
            
                  <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label3" runat="server" Text="Warning"></asp:Label>
                          
    
                      </td>
                           </tr>
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label4" runat="server" Text="This invoice was already sent by [Sent By] on [Sent Time Stamp], do you wish to proceed?" ></asp:Label>
                       <br /> <br />
                          <%--<asp:Label ID="Label2" runat="server" Text="Please update the customer contact details to ensure that there will be no errors."></asp:Label><br /><br />--%>
                      </td>
                           </tr>
           
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnYesAttchWarning" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px"/>
                                 <asp:Button ID="btnCancelattchwarningMsg" runat="server" CssClass="dummybutton" BackColor="#CFC6C0"  Font-Bold="True" Text="Ok" Width="100px"/>
                                </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupAttchWarning" runat="server" CancelControlID="btnCancelattchwarningMsg" PopupControlID="pnlattchwarningMsg" TargetControlID="btndummattchwarningMsg" BackgroundCssClass="modalBackground" DynamicServicePath="" ></asp:ModalPopupExtender>
         <asp:Button ID="btndummattchWarningmsg" runat="server" CssClass="dummybutton" />

            <asp:Panel ID="pnlAttchManualRpt" runat="server" BackColor="White" Width="700px" Height="200px" BorderColor="#003366" BorderWidth="1px" ScrollBars="Auto">
          
         <table border="0" style="font-family: Calibri; font-size: 15px; font-weight: bold; color: white;text-align:left;padding-left:2px;">
            
                  <tr style="background-color:navy; color:white;font-size:medium">
                      <td style="font-family: Calibri; font-size: 17px; font-weight: bold; color: white;text-align:center">
                         
                          <asp:Label ID="Label5" runat="server" Text="Warning"></asp:Label>
                          
    
                      </td>
                           </tr>
               <tr>
                             <td colspan="2"><br /></td>
                         </tr> 
               <tr>
                      <td class="CellFormat" style="text-align:center; margin-left:auto; margin-right:auto;"">
                         
                          &nbsp;<asp:Label ID="Label6" runat="server" Text="Would you like to attach the manual service report along with this invoice ?" ></asp:Label>
                       <br /> <br />
                          <%--<asp:Label ID="Label2" runat="server" Text="Please update the customer contact details to ensure that there will be no errors."></asp:Label><br /><br />--%>
                      </td>
                           </tr>
           
                            <tr>
                             <td colspan="2"><br /></td>
                         </tr>
                            <tr style="padding-top:40px;">
                             <td colspan="2" style="text-align:center"><asp:Button ID="btnYesAttchManualRpt" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="OK" Width="100px"/>
                                 <asp:Button ID="btncancelAttchManualRpt" runat="server" CssClass="button" BackColor="#CFC6C0"  Font-Bold="True" Text="No" Width="100px"/>
                                </td>
                         </tr>                       
        </table>
        
           </asp:Panel>

               
      <asp:ModalPopupExtender ID="mdlPopupAttchManualRpt" runat="server" CancelControlID="btnCancelAttchManualRpt" PopupControlID="pnlAttchManualRpt" TargetControlID="btndummyAttchManualRpt" BackgroundCssClass="modalBackground" DynamicServicePath="" ></asp:ModalPopupExtender>
         <asp:Button ID="btndummyAttchManualRpt" runat="server" CssClass="dummybutton" />

            <asp:TextBox ID="txtSvc" runat="server" Height="16px" MaxLength="10" Width="20%" CssClass="dummybutton"></asp:TextBox>
                   <asp:TextBox ID="txtSvcAddr" runat="server" Height="16px" MaxLength="10" Width="20%" CssClass="dummybutton"></asp:TextBox>
           <asp:TextBox ID="txtFileType" runat="server" Height="16px" MaxLength="10" Width="20%" CssClass="dummybutton"></asp:TextBox>
              <asp:TextBox ID="txtManualRpt" runat="server" Height="16px" MaxLength="10" Width="20%" CssClass="dummybutton"></asp:TextBox>
     
    </div>

 </asp:Content>
