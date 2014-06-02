<%@ Page Title="Группы" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsClient._Default" %>
<%@ Register TagName="Captcha" TagPrefix="Controls" Src="~/Controls/Captcha.ascx" %>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
            </hgroup>
            <p>
                На этой странице вы можете добавлять удалять и редактировать студенческие группы.
                А также просматривать список студентов группы. Для зачисления, отчисления и перевода
                студентов перейдите на страницу работы со студентами.
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div style="border-spacing: 10px">
    <div style="float: left; margin-right: 30px;">
        <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            AutoGenerateSelectButton="True" SelectMethod="GetStudentGroups" 
            OnSelectedIndexChanged="Select" DataKeyNames="Id" Caption="Список групп" ShowHeader="False">
            <Columns>
            <asp:BoundField HeaderText="Название" DataField="Name"/>
                </Columns>
            <SelectedRowStyle BackColor="#00CCFF" />          
        </asp:GridView>
        <div style="height: 30px;">            
            <div style="display: inline-block; margin-right: 30px; height: 30px">
            </div>            
        </div>  
            </div>   
    </div>
    <div style="float: left" ID="Details">
        <table style="width:100%;">
            <tr>
                <td colspan="2">
                    <asp:Label ID="TitleLabel" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Название группы</td>
                <td>
                    <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <Controls:Captcha id ="Captcha1" runat="Server" OnSuccess="OnSuccess" OnFailure="OnFailure"/>
                </td>
            </tr>
        </table>
    </div>
  </div>     
</asp:Content>
