<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Survey.aspx.cs" Inherits="Survey.WebApp.Views.Survey" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Survey here</h1>
            <asp:Label ID="QuestionText" runat="server"></asp:Label>
            <asp:PlaceHolder ID="QuestionOptionsPlaceHolder" runat="server"></asp:PlaceHolder>
            <asp:Label ID="LabelTestePilha" runat="server"></asp:Label>
            <asp:Label ID="LabelTesteLista" runat="server"></asp:Label>
            <asp:Button ID="ButtonNext" runat="server" Text="Next" OnClick="ButtonNext_Click"  />
        </div>        
    </form>
</body>
</html>
