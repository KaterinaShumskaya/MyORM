<%@ Control Language="C#" AutoEventWireup="true" Inherits="CaptchaControl" Codebehind="Captcha.ascx.cs" %>
<style type="text/css">
    .auto-style1 {
        height: 16px;
        width: 45px;
    }
</style>
<table border="0" cellpadding="0" cellspacing="5" style="<% =style %>">
    <tr>
        <td style="font-size: 10pt; " class="auto-style1" colspan="3">
            <asp:Label ID="lblCMessage" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td style="font-size: 10pt; " class="auto-style1" colspan="3">
            <asp:Image ID="imgCaptcha" runat="server" Height="92px" Width="305px"/></td>
    </tr>
    <tr>
        <td style="font-size: 10pt; " class="auto-style1" colspan="3">
            <asp:TextBox ID="txtCaptcha" runat="server"
                Width="298px"></asp:TextBox>
                </td>
    </tr>
    <tr>
        <td style="font-size: 10pt; " class="auto-style1">
            <asp:Button ID="btnAdd" runat="server"
                Text="Добавить" OnClick="btnAdd_Click" Width="69px" />
        </td>
        <td>
            <asp:Button ID="btnDelete" runat="server" Text="Удалить" OnClick="btnDelete_Click"/>
        </td>
        <td>
            <asp:Button ID="btnEdit" runat="server" Text="Редактировать" OnClick="btnEdit_Click"/>
        </td>
    </tr>
</table>
