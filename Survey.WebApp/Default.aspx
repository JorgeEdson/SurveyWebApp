<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Survey.WebApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Primeiro ele abre aqui</h1>
            <asp:Button ID="buttonSurvey" runat="server" Text="Survey" OnClick="buttonSurvey_Click" />
            <asp:Button ID="buttonLogin" runat="server" Text="Login" OnClick="buttonLogin_Click" />
        </div>
    </form>
</body>

</html>
