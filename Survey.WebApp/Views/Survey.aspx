<%@ Page Language="C#" EnableEventValidation="true" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Survey.aspx.cs" Inherits="Survey.WebApp.Views.Survey" %>


<asp:Content ID="SurveyContent" ContentPlaceHolderID="MainContent" runat="server">    
        <div>
            <h1>Survey here</h1>
            <asp:Label ID="QuestionText" runat="server"></asp:Label>
            <asp:TextBox ID="TextBoxForRender" runat="server" Visible="false"></asp:TextBox>
            <asp:RadioButtonList ID="RadioButtonListForRender" runat="server" Visible="false"></asp:RadioButtonList>
            <asp:CheckBoxList ID="CheckBoxListForRender" runat="server" Visible="false"></asp:CheckBoxList>
            <asp:Button ID="ButtonPrevious" runat="server" Text="Previous" OnClick="ButtonPrevious_Click"  />
            <asp:Button ID="ButtonNext" runat="server" Text="Next" OnClick="ButtonNext_Click"  />
            <asp:Button ID="ButtonSaveAnswers" runat="server" Text="Save Answers" OnClick="ButtonSaveAnswers_Click" Visible="false"  />
            <asp:Label ID="LabelQuantidadeAnswers" runat="server"></asp:Label>            
        </div>       
</asp:Content>

