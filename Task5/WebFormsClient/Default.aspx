<%@ Page Title="Группы" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsClient._Default" %>

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
            OnSelectedIndexChanged="ShowStudents" DataKeyNames="Id" DeleteMethod="DeleteGroup" UpdateMethod="UpdateGroup">
            <Columns>
            <asp:BoundField HeaderText="Название" DataField="Name"/>
                </Columns>
            <SelectedRowStyle BackColor="#00CCFF" />
           
        </asp:GridView>

        <asp:Label ID="ErrorLabel" runat="server" Text="" ForeColor="Red"></asp:Label>
        <div style="height: 30px;">
            
            <div style="display: inline-block; margin-right: 30px; height: 30px">
              <asp:TextBox ID="TextBox1" runat="server" Width="95px" Height="20px"></asp:TextBox>
            </div>
             
            <div style="display: inline-block; height: 30px" >
                 <asp:LinkButton ID="LinkButton1" runat="server" OnClick="AddGroup">Добавить группу</asp:LinkButton>
            </div>
            <div style="display: inline-block; height: 30px" > 
        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="DeleteGroup">Удалить группу</asp:LinkButton>
         </div>    
        </div>  
            </div>   
    </div>
    <div style="float: left" ID="Details">
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellSpacing="15">
            <Columns>
                <asp:BoundField HeaderText="Фамилия" DataField="LastName"/>
                <asp:BoundField HeaderText="Имя" DataField="FirstName"/>
                <asp:BoundField HeaderText="Отчество" DataField="MiddleName"/>
            </Columns>
            <SelectedRowStyle BackColor="#00CCFF" />
        </asp:GridView>
    </div>
  </div>
        
</asp:Content>
