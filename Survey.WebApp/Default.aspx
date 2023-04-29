<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="Survey.WebApp.Default" %>

<asp:Content ID="DefaultMainContent" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        <div>
            <h1>Primeiro ele abre aqui</h1>
            <asp:Button ID="buttonSurvey" runat="server" Text="Survey" OnClick="buttonSurvey_Click" />
            <asp:Button ID="buttonLogin" runat="server" Text="Login" OnClick="buttonLogin_Click" />
        </div>
    </form>
</asp:Content>
