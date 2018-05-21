<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="barraAcciones.ascx.cs" Inherits="Backoffice.barraAcciones" %>
<asp:Panel ID="Panel1" runat="server" BackColor="White" ForeColor="Black" Height="66px" Width="325px">
    <br />
    <asp:ImageButton ID="ImageButton6" runat="server" Height="32px" ImageUrl="~/fonts/add.png" Width="32px" />
    &nbsp;<asp:ImageButton ID="ImageButton7" runat="server" Height="32px" ImageUrl="~/fonts/delete.png" style="margin-top: 0px" Width="32px" />
    &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" Height="32px" ImageUrl="~/fonts/save.png" OnClick="ImageButton1_Click" Width="32px" />
    &nbsp;<asp:ImageButton ID="ImageButton2" runat="server" Height="32px" ImageUrl="~/fonts/cancel.png" Width="32px" />
    &nbsp;<asp:ImageButton ID="ImageButton3" runat="server" Height="32px" ImageUrl="~/fonts/edit-file.png" Width="32px" />
    &nbsp;<asp:ImageButton ID="ImageButton4" runat="server" Height="32px" ImageUrl="~/fonts/export.png" Width="32px" />
    &nbsp;<asp:ImageButton ID="ImageButton5" runat="server" Height="32px" ImageUrl="~/fonts/print.png" Width="32px" />
</asp:Panel>

