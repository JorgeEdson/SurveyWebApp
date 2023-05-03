<%@ Page Language="C#" EnableEventValidation="true" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Survey.aspx.cs" Inherits="Survey.WebApp.Views.Survey" %>


<asp:Content ID="SurveyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        #Container {
            background-color: #fff;
            height: 600px;
            width: 1000px;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            margin-bottom: 7%;
            border-radius: 8px;
            -webkit-box-shadow: inset 1px 1px 26px 3px rgba(0,0,0,0.19);
            -moz-box-shadow: inset 1px 1px 26px 3px rgba(0,0,0,0.19);
            box-shadow: inset 1px 1px 26px 3px rgba(0,0,0,0.19);
        }

        #QuestionTitle {
            width: 60%;
            font-size: 30px;
            font-weight: bold;
            border-bottom: 1px solid #00000090;
        }

        #QuestionArea {
            align-items: start;
            height: 40%;
            width: 60%;
            margin-top: 5%;
            font-size: 18px;
        }

        #ButtonArea {
            display: flex;
            gap: 20px;
            width: 60%;
        }

        .Button {
            height: 50px;
            width: 100px;
            border: none;
            outline: none;
            background-color: #4E944F;
            font-size: 16px;
            color: #FFF;
            margin-top: 15px;
            border-radius: 8px;
            transition: 0.1s ease-in-out;
        }

        .ButtonSkip {
            height: 50px;
            width: 100px;
            border: none;
            outline: none;
            background-color: #D63232;
            font-size: 16px;
            color: #FFF;
            margin-top: 15px;
            border-radius: 8px;
            transition: 0.1s ease-in-out;
        }

        .btnMargin {
            margin-left: 43%;
        }

        .Button:hover {
            background-color: #6EBD6F;
            transform: scale(1.1);
            cursor: pointer;
        }

        .ButtonSkip:hover {
            background-color: #FF3C3C;
            transform: scale(1.1);
            cursor: pointer;
        }

        #title {
            margin-bottom: 10%;
        }

        .InputForm {
            height: 50px;
            width: 70%;
            border: none;
            background-color: #00000010;
            outline: none;
            border-bottom: 1px solid #00000090;
            color: #000;
            padding-left: 8px;
            font-size: 14px;
        }      
    </style>

    <div id="Container">
        <h1 id="title">Answer the questions bellow.</h1>
        <div id="QuestionTitle">
            <asp:Label ID="QuestionText" CssClass="question" runat="server"></asp:Label>
        </div>
        <div id="QuestionArea">
            <asp:TextBox ID="TextBoxForRender" runat="server" placeholder="Your answer here" CssClass="InputForm" Visible="false"></asp:TextBox>
            <asp:RadioButtonList ID="RadioButtonListForRender" Font-Size="Large" runat="server" Visible="false"></asp:RadioButtonList>
            <asp:CheckBoxList ID="CheckBoxListForRender" runat="server" Visible="false"></asp:CheckBoxList>
        </div>
        <div id="ButtonArea">
            <asp:Button ID="ButtonPrevious" CssClass="Button" runat="server" Text="Previous" OnClick="ButtonPrevious_Click" />
            <asp:Button ID="ButtonNext" CssClass="Button" runat="server" Text="Next" OnClick="ButtonNext_Click" />
            <asp:Button ID="ButtonSkip" CssClass="ButtonSkip btnMargin" runat="server" Text="Skip" OnClick="ButtonSkip_Click" />
        </div>
        <asp:Button ID="ButtonSaveAnswers" runat="server" Text="Save Answers" OnClick="ButtonSaveAnswers_Click" Visible="false" />
        <asp:Label ID="LabelQuantidadeAnswers" runat="server"></asp:Label>
        <asp:Label ID="LabelWarning" runat="server"></asp:Label>
    </div>

</asp:Content>

