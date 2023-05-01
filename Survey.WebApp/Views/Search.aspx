<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Search.aspx.cs" Inherits="Survey.WebApp.Views.Search" %>

<asp:Content ID="SearchMainContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        #Container {
            display: flex;
            flex-direction: column;
            justify-content: flex-start;
            align-items: center;
            margin-top: -5%;
            width: 1200px;
            height: 100vh;
        }

        #TableHeader {
            display: flex;
            justify-content: space-between;
            align-items: center;
            width: 80%;
            height: 60px;
            margin-top: 10%;
            flex-wrap: wrap;
            margin-bottom: 30px;
            position: relative;
        }

        .Dropdown {
            height: 40px;
            width: 205px;
            border: none;
            outline: none;
            margin-bottom: 15px;
        }

        .ButtonFilter {
            height: 40px;
            width: 100px;
            border: none;
            outline: none;
            background-color: #4E944F;
            font-size: 16px;
            color: #FFF;
            margin-top: 15px;
            border-radius: 8px;
            transition: 0.1s ease-in-out;
            margin-bottom: 10px;
        }

        .ButtonFilter:hover {
            background-color: #6EBD6F;
            cursor: pointer;
        }

        .ButtonFilterClear {
            height: 40px;
            width: 100px;
            border: none;
            outline: none;
            background-color: #D63232;
            font-size: 16px;
            color: #FFF;
            margin-top: 15px;
            border-radius: 8px;
            transition: 0.1s ease-in-out;
            margin-bottom: 10px;
        }

        .ButtonFilterClear:hover {
            background-color: #FF3B3B;
            cursor: pointer;
        }

        .InputForm {
            height: 40px;
            width: 200px;
            border: none;
            color: transparent;
            outline: none;
            color: #000;
            padding-left: 5px;
            font-size: 14px;
            margin-bottom: 15px;
        }

        #Table {
            display: flex;
            justify-content: center;
            align-items: center;
            margin-top: 5%;
            border-top: 1px solid #00000090;
            width: 80%;
        }

        #FilterAreaButton {
            display: flex;
            justify-content: flex-end;
            width: 80%;
            margin-top: 5%;
            gap: 8px;
        }

        #Title {
            width: 80%;
            margin-top: 7%;
            margin-bottom: -7%;
            display: flex;
            flex-direction: column;
        }

        .TableContent {
            margin-top: 2%;
            width: 960px;
        }

        .TextError {
            margin-top: 3%;
        }
    </style>

    <div id="Container">
        <div id="Title">
            <h1>Searching Page</h1>
        </div>
        <div id="TableHeader">
            <asp:DropDownList ID="Gender" CssClass="Dropdown" runat="server">
                <asp:ListItem>Select the gender</asp:ListItem>
                <asp:ListItem>Male</asp:ListItem>
                <asp:ListItem>Female</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="AgeRange" CssClass="Dropdown" runat="server">
                <asp:ListItem>Select the age</asp:ListItem>
                <asp:ListItem>Over 18 to 24</asp:ListItem>
                <asp:ListItem>25 to 34</asp:ListItem>
                <asp:ListItem>35 to 44</asp:ListItem>
                <asp:ListItem>45 to 54</asp:ListItem>
                <asp:ListItem>55 to 64</asp:ListItem>
                <asp:ListItem>65 and over</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="StateOrTerritory" CssClass="Dropdown" runat="server">
                <asp:ListItem>State Or Territory</asp:ListItem>
                <asp:ListItem>NSW</asp:ListItem>
                <asp:ListItem>VIC</asp:ListItem>
                <asp:ListItem>QLD</asp:ListItem>
                <asp:ListItem>WA</asp:ListItem>
                <asp:ListItem>SA</asp:ListItem>
                <asp:ListItem>TAS</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="HomeSuburb" runat="server" CssClass="InputForm" placeholder="Home suburb"></asp:TextBox>
            <asp:TextBox ID="PostCode" runat="server" CssClass="InputForm" placeholder="Home PostCode"></asp:TextBox>
            <asp:TextBox ID="Email" runat="server" CssClass="InputForm" placeholder="Email" TextMode="Email"></asp:TextBox>
            <asp:DropDownList ID="BankUsed" CssClass="Dropdown" runat="server">
                <asp:ListItem>Bank Used</asp:ListItem>
                <asp:ListItem>Commonwealth bank</asp:ListItem>
                <asp:ListItem>Westpac</asp:ListItem>
                <asp:ListItem>National Australia Bank</asp:ListItem>
                <asp:ListItem>Macquarie Bank</asp:ListItem>
                <asp:ListItem>Others</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="AditionalServices" CssClass="Dropdown" runat="server">
                <asp:ListItem>Additional Services</asp:ListItem>
                <asp:ListItem>Internet Banking</asp:ListItem>
                <asp:ListItem>Home Loan</asp:ListItem>
                <asp:ListItem>Credit card</asp:ListItem>
                <asp:ListItem>Share Investment</asp:ListItem>
                <asp:ListItem>Other</asp:ListItem>
                <asp:ListItem>None</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="Newspaper" CssClass="Dropdown" runat="server">
                <asp:ListItem>Newspaper</asp:ListItem>
                <asp:ListItem>The Guardian</asp:ListItem>
                <asp:ListItem>Herald Sun</asp:ListItem>
                <asp:ListItem>The advertiser</asp:ListItem>
                <asp:ListItem>The Sydney Morning Herald</asp:ListItem>
                <asp:ListItem>The Courier</asp:ListItem>
                <asp:ListItem>The Border Mail</asp:ListItem>
                <asp:ListItem>Gippsland Times</asp:ListItem>
                <asp:ListItem>None</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="SectionRead" CssClass="Dropdown" runat="server">
                <asp:ListItem>Most Read Section</asp:ListItem>
                <asp:ListItem>Property</asp:ListItem>
                <asp:ListItem>Sport</asp:ListItem>
                <asp:ListItem>Financial</asp:ListItem>
                <asp:ListItem>Entertainment</asp:ListItem>
                <asp:ListItem>Lifestyle</asp:ListItem>
                <asp:ListItem>Travel</asp:ListItem>
                <asp:ListItem>Politics</asp:ListItem>
                <asp:ListItem>Other</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="FavoriteSports" CssClass="Dropdown" runat="server">
                <asp:ListItem>Favorite Sports</asp:ListItem>
                <asp:ListItem>AFL</asp:ListItem>
                <asp:ListItem>Football</asp:ListItem>
                <asp:ListItem>Cricket</asp:ListItem>
                <asp:ListItem>Racing</asp:ListItem>
                <asp:ListItem>Motorsport</asp:ListItem>
                <asp:ListItem>Basketball</asp:ListItem>
                <asp:ListItem>Tennis</asp:ListItem>
                <asp:ListItem>Other</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="TravelDestination" CssClass="Dropdown" runat="server">
                <asp:ListItem>Travel Destination</asp:ListItem>
                <asp:ListItem>Australia</asp:ListItem>
                <asp:ListItem>Europe</asp:ListItem>
                <asp:ListItem>Pacific</asp:ListItem>
                <asp:ListItem>North America</asp:ListItem>
                <asp:ListItem>South America</asp:ListItem>
                <asp:ListItem>Asia</asp:ListItem>
                <asp:ListItem>Middle East</asp:ListItem>
                <asp:ListItem>Other</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="UserName" runat="server" CssClass="InputForm" placeholder="Username"></asp:TextBox>
        </div>
        <span id="FilterAreaButton">
            <asp:Button ID="Button1" runat="server" CssClass="ButtonFilter" Text="Filter" OnClick="ButtonSearch_Click" />
            <asp:Button ID="ClearFilters" runat="server" CssClass="ButtonFilterClear" Text="Clear" OnClick="ButtonClear_Click" />
        </span>
        <div id="Table">
            <asp:GridView ID="Gridview" CssClass="TableContent" runat="server"></asp:GridView>
            <asp:Label ID="TableError" runat="server" CssClass="TextError" Visible="false">No records found in database.</asp:Label>
        </div>
    </div>

</asp:Content>

