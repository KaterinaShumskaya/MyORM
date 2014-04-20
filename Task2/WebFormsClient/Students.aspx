<%@ Page Language="C#" Title="Students Page" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="WebFormsClient._Default"  CodeBehind="Students.aspx.cs" %>

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
        <asp:DropDownList ID="DropDownList1" runat="server" SelectMethod="GetStudentGroups">
           </asp:DropDownList>
    <div style="float: left; margin-right: 30px;">
        <div>
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
            AutoGenerateSelectButton="True"
            OnSelectedIndexChanged="ChangeSelection" DataKeyNames="Id" DeleteMethod="DeleteStudent"
             UpdateMethod="UpdateStudent">
            <Columns>
            <asp:BoundField HeaderText="Фамилия" DataField="LastName"/>
                <asp:BoundField HeaderText="Имя" DataField="FirstName"/>
                <asp:BoundField HeaderText="Отчество" DataField="MiddleName"/>
                <asp:BoundField HeaderText="Дата рождения" DataField="DateOfBirth"/>
                </Columns>
            <SelectedRowStyle BackColor="#00CCFF" />
           
        </asp:GridView>

        <asp:Label ID="ErrorLabel" runat="server" Text="" ForeColor="Red"></asp:Label>
        <div style="height: 30px;">
            
            <div style="display: inline-block; margin-right: 30px; height: 30px">
              <asp:TextBox ID="TextBox1" runat="server" Width="95px" Height="20px"></asp:TextBox>
            </div>
             
            <div style="display: inline-block; height: 30px" >
                 <asp:LinkButton ID="LinkButton1" runat="server" OnClick="AddStudent">Добавить студента</asp:LinkButton>
            </div>
            <div style="display: inline-block; height: 30px" > 
        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="DeleteStudent">Удалить студента</asp:LinkButton>
         </div>    
        </div>  
            </div>   
    </div>
   
          
  </div>
</asp:Content>

