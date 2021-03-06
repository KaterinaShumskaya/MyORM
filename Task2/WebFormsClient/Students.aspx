﻿<%@ Page Language="C#" Title="Студенты" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="WebFormsClient.Students"  CodeBehind="Students.aspx.cs" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>Modify this template to jump-start your ASP.NET application.</h2>
            </hgroup>
            <p>
                To learn more about ASP.NET, visit <a href="http://asp.net" title="ASP.NET Website">http://asp.net</a>.
                The page features <mark>videos, tutorials, and samples</mark> to help you get the most from ASP.NET.
                If you have any questions about ASP.NET visit
                <a href="http://forums.asp.net/18.aspx" title="ASP.NET Forum">our forums</a>.
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   <div style="border-spacing: 10px">     
         <div style="float: left; margin-right: 30px;">
            <asp:DropDownList ID="DropDownList1" runat="server" SelectMethod="GetStudentGroups" AppendDataBoundItems="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                 <asp:ListItem Text="Name" Value="Id" /> 
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
            <asp:Label ID="ErrorLabel" runat="server" Text="" ForeColor="Red"></asp:Label>
            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="DeleteStudent">Удалить студента</asp:LinkButton>   
        </div>  
        <div style="float: left; margin-right: 30px;">

                <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px" AutoGenerateRows="False" 
                    AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" AutoGenerateInsertButton="True" 
                    UpdateMethod="UpdateStudent" SelectMethod="SelectSt" OnItemInserted="InsertSt" InsertMethod="InsertStudent">
                    <Fields>
                <asp:BoundField HeaderText="Фамилия" DataField="LastName"/>
                    <asp:BoundField HeaderText="Имя" DataField="FirstName"/>
                    <asp:BoundField HeaderText="Отчество" DataField="MiddleName"/>
                    <asp:BoundField DataField="DateOfBirth" HeaderText="Дата рождения" />
                    </Fields>
                </asp:DetailsView>
                
        </div>
  </div>
</asp:Content>

