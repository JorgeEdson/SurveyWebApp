<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Login.aspx.cs" Inherits="Survey.WebApp.Views.Login" %>

<asp:Content ID="LoginMainContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <style>
        #Container {
            display: flex;
            gap: 100px;
            margin-top: -5%;
        }

        #LoginContainer {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            height: 550px;
            width: 500px;
            gap: 60px;
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
            gap: 20px;
            width: 400px
        }

        .InputForm {
            height: 50px;
            width: 70%;
            border: none;
            color: transparent;
            outline: none;
            color: #000;
            border-bottom: 1px solid #00000080;
            padding-left: 5px;
            font-size: 14px;
        }

        .ErrorColor {
            color: red;
        }

        @media screen and (max-width: 1100px) {
            #ImageContainer {
                display: none;
            }
        }
    </style>

    <div id="Container">
        <div id="ImageContainer">
            <img src="../Assets/images/Authentication-bro.png" width="500" />
        </div>
        <div id="LoginContainer">
            <h1>Log Into Survey</h1>
            <div id="FormItens">
                <asp:TextBox ID="LoginName" placeholder="Username*" CssClass="InputForm" runat="server"></asp:TextBox>
                <asp:TextBox ID="Password" placeholder="Password*   " CssClass="InputForm" TextMode="Password" runat="server"></asp:TextBox>
                <asp:Button ID="buttonLogin" runat="server" Text="Login" CssClass="ButtonLogin" OnClick="ButtonLogin_Click" />
            </div>
            <asp:Label ID="ErrorLabel" CssClass="ErrorColor" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
    </div>

</asp:Content>
