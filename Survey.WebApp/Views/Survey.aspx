<%@ Page Language="C#" EnableEventValidation="true" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Survey.aspx.cs" Inherits="Survey.WebApp.Views.Survey" %>


<asp:Content ID="SurveyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        #Container {
            background-color: #fff;
            height: 500px;
            width: 1000px;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            margin-top: -7%;
            border-radius: 8px;
        }

        .QuestionTitle {
            display: flex;
            justify-content: center;
            width: 100%;
            align-self: flex-start;
            font-size: 30px;
            font-weight: bold;
            margin-bottom: 12%;
            background-color: #609966;
            height: 60px;
            padding-top: 20px;
            color: #fff;
        }

        .QuestionArea {
            align-items: start;
            height: 40%;
            width: 60%;
            margin-top: -10%;
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
            margin-top: 25px;
            height: 50px;
            width: 100%;
            border: none;
            background-color: #00000010;
            outline: none;
            border-bottom: 1px solid #00000090;
            color: #000;
            padding-left: 8px;
            font-size: 14px;
        }    
        
        .Radio {
            margin-top: 25px;
        }

         #LoginContainer {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            height: 400px;
            width: 450px;
            background-color: #FFF;
            border-radius: 10px;
        }

        .ButtonLogin {
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

        .ButtonLogin:hover {
            background-color: #6EBD6F;
            transform: scale(1.1);
            cursor: pointer;
        }

        #FormItens {
            display: flex;
            justify-content: center;
            align-items: center;
            flex-direction: column;
            gap: 5px;
            width: 400px
        }

        .ErrorColor {
            color: red;
        }
    </style>

    <div id="Container">
        <asp:Panel ID="QuestionTitle" runat="server" CssClass="QuestionTitle">
            <asp:Label ID="QuestionText" CssClass="question" runat="server"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="QuestionArea" runat="server" CssClass="QuestionArea">
            <asp:TextBox ID="TextBoxForRender" runat="server" placeholder="Your answer here" CssClass="InputForm" Visible="false"></asp:TextBox>
            <asp:RadioButtonList ID="RadioButtonListForRender" CssClass="Radio" Font-Size="Large" runat="server" Visible="false"></asp:RadioButtonList>
            <asp:CheckBoxList ID="CheckBoxListForRender" runat="server" Visible="false"></asp:CheckBoxList>
        </asp:Panel>
        <asp:Panel ID="RegisterForm" runat="server" Visible="false">
            <div id="LoginContainer">
                <h1>Register Into Survey</h1>
                <div id="FormItens">
                    <asp:TextBox ID="TextBoxGivenName" Height="40" placeholder="Given Name" CssClass="InputForm" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBoxLastName" Height="40" placeholder="Last Name" CssClass="InputForm" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBoxDateBirth" Height="40" placeholder="Date Birth" CssClass="InputForm" TextMode="Date" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBoxPhoneNumber" Height="40" placeholder="Phone Number" CssClass="InputForm" runat="server"></asp:TextBox>
                    <asp:Button ID="ButtonSaveAnswersRegisterForm" runat="server" Text="Register" CssClass="ButtonLogin" OnClick="ButtonSaveAnswersRegisterForm_Click" />
                </div>
                <asp:Label ID="ErrorLabel" CssClass="ErrorColor" runat="server" Text="Label" Visible="false"></asp:Label>
            </div>
        </asp:Panel>
        <div id="ButtonArea">
            <asp:Button ID="ButtonPrevious" CssClass="Button" runat="server" Text="Previous" OnClick="ButtonPrevious_Click" />
            <asp:Button ID="ButtonNext" CssClass="Button" runat="server" Text="Next" OnClick="ButtonNext_Click" />
            <asp:Button ID="ButtonSkip" CssClass="ButtonSkip btnMargin" runat="server" Text="Skip" OnClick="ButtonSkip_Click" />
        </div>
        <asp:Button ID="ButtonSaveAnswers" runat="server" Text="Save Answers" OnClick="ButtonSaveAnswers_Click" Visible="false" />
        <div style="padding: 15px;">
            <asp:Label ID="LabelWarning" CssClass="ErrorColor" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>

