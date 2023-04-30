<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Survey.aspx.cs" Inherits="Survey.WebApp.Views.Survey" %>


<asp:Content ID="SurveyContent" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
        <div>
            <h1>Survey here</h1>
            <asp:Label ID="QuestionText" runat="server"></asp:Label>
            <asp:PlaceHolder ID="QuestionOptionsPlaceHolder" runat="server"></asp:PlaceHolder>
            <asp:Button ID="ButtonPrevious" runat="server" Text="Previous" OnClick="ButtonPrevious_Click"  />
            <asp:Button ID="ButtonNext" runat="server" Text="Next" OnClick="ButtonNext_Click"  />
            <asp:Button ID="ButtonSaveAnswers" runat="server" Text="Save Answers" OnClick="ButtonSaveAnswers_Click" Visible="false"  />
            <asp:Label ID="LabelQuantidadeAnswers" runat="server"></asp:Label>            
        </div>        
    </form>
</asp:Content>

