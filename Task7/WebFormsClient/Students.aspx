<%@ Page Language="C#" Title="Студенты" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="WebFormsClient.Students"  CodeBehind="Students.aspx.cs" %>
<%@ Register TagName="Captcha" TagPrefix="Controls" Src="~/Controls/Captcha.ascx" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %></h1>               
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   <div style="border-spacing: 10px">     
         <div style="float: left; margin-right: 30px;">
            <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true" 
                OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList>
            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                AutoGenerateSelectButton="True" OnSelectedIndexChanged="SelectStudent"
                 DataKeyNames="Id" DeleteMethod="DeleteStudent"
                 UpdateMethod="UpdateStudent" SelectMethod="GetStudents">
                <Columns>
                <asp:BoundField HeaderText="Фамилия" DataField="LastName"/>
                    <asp:BoundField HeaderText="Имя" DataField="FirstName"/>
                    <asp:BoundField HeaderText="Отчество" DataField="MiddleName"/>
                    <asp:BoundField HeaderText="Дата рождения" DataField="DateOfBirth"/>
                    </Columns>
                <SelectedRowStyle BackColor="#00CCFF" />
            </asp:GridView>
        </div>  
        <div style="float: left" ID="Details">
        <table style="width:100%;">
            <tr>
                <td colspan="2">
                    <asp:Label ID="StudentTitle" runat="server" Text="Информация о студенте"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Фамилия</td>
                <td>
                    <asp:TextBox ID="tbLastName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Имя</td>
                <td>
                    <asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Отчество</td>
                <td>
                    <asp:TextBox ID="tbMiddleName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Дата рождения</td>
                <td>
                    <asp:TextBox ID="tbBirthday" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <Controls:Captcha ID ="captcha" runat="Server" OnSuccess="OnSuccess" OnFailure="OnFailure"/>
                </td>
            </tr>
        </table>
    </div>
  </div>
</asp:Content>

