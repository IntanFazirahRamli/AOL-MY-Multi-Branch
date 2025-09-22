<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DragDrop.aspx.vb" Inherits="DragDrop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 
<style type="text/css">
.grd1-header
{   
background-color:Maroon;
border: 3px solid #ffba00;
color:White;
}
.grd2-header
{   
background-color:Green;
border: 3px solid #ffba00;
color:White;
}
.sel-row
{   
background-color:Yellow;
border: 3px solid #ffba00;
color:Black;
}
.sel-row td
{
cursor:move;
font-weight:bold;
}
</style>
    <title>Drag & Drop GridView</title>
   <%-- <script src="js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.8.24.custom.min.js" type="text/javascript"></script>--%>

     <script src="js/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            var droppedItemIndex = -1;
            $('.block').draggable({
                helper: "clone",
                cursor: "move"
            });
            $('#<%=DetailsView1.ClientID %>').droppable({
                drop: function (ev, ui) {
                    accept: '.block',
                     droppedItemIndex = $(ui.draggable).index();
                    var droppedItem = $(".grd-source tr").eq(droppedItemIndex);
                    index = -1;
                    $("[id*=DetailsView1] .name").html(droppedItem.find(".name").html());
                    $("[id*=DetailsView1] .designation").html(droppedItem.find(".designation").html());

                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <h1>Drag and Drop GridView Row to another GridView using jQuery</h1>
    <div>
    <table class="ui-corner-top" border="1">
    <tr>
    <td>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"  
            onrowcreated="GridView1_RowCreated" CssClass="grd-source" >
            <SelectedRowStyle CssClass="block sel-row" />
            <RowStyle CssClass="block" />
            <HeaderStyle CssClass="grd1-header" />
            <Columns>
            <asp:BoundField DataField="id" HeaderText="ID" ItemStyle-CssClass="id" />
            <asp:BoundField DataField="name" HeaderText="NAME" ItemStyle-CssClass="name" />
             <asp:BoundField DataField="designation" HeaderText="DESIGNATION" ItemStyle-CssClass="designation" />
              <asp:BoundField DataField="salary" HeaderText="SALARY" ItemStyle-CssClass="salary" />
           </Columns>
        </asp:GridView>
    </td>
    <td valign="middle">
         <asp:DetailsView CssClass="ui-widget" ID="DetailsView1" runat="server"
        Height="50px" Width="125px" AutoGenerateRows="false">
        <Fields> 
          <asp:BoundField DataField="name" HeaderText="Name" ItemStyle-CssClass="name" />
          <asp:BoundField DataField="designation" HeaderText="Designation" ItemStyle-CssClass="designation" />
        </Fields>
         <HeaderStyle BackColor="Green" ForeColor="White" HorizontalAlign="Center" />  
            <HeaderTemplate>  
                <asp:Label ID="Label1" runat="server" Text="Details" Font-Size="Large"></asp:Label>  
            </HeaderTemplate> 
    </asp:DetailsView>
    </td>
    </tr>
</table>   
    </div>
    </form>
</body>
</html>
