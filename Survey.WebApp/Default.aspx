<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="Survey.WebApp.Default" %>

<asp:Content ID="DefaultMainContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #Container {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            margin-top: -5%;
        }

        .ButtonSurvey {
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

            .ButtonSurvey:hover {
                background-color: #6EBD6F;
                transform: scale(1.1);
                cursor: pointer;
            }

        #Title {
            font-size: 50px;
        }
    </style>

    <div id="Container">
        <h1 id="Title">Welcome to Survey!</h1>
        <div id="ChecklistImage">
            <img src="Assets/images/Checklist-bro.png" width="500" />
        </div>
        <p id="red">Click the button bellow to start your survey.</p>
        <div id="ButtonArea">
            <asp:Button ID="buttonSurvey" runat="server" Text="Let's Go" CssClass="ButtonSurvey" OnClick="buttonSurvey_Click" />
        </div>
    </div>
</asp:Content>

