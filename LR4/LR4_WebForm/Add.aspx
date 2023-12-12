<%@ Page Title="Add" Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="LR4_WebForm.Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-bottom: 10px;">
            <asp:TextBox runat="server" ID="first" placeholder="int" />
            <asp:TextBox runat="server" ID="second" placeholder="int" />
            <asp:Button runat="server" ID="sum" OnClick="Sum_Click" Text="Add" />
        </div>
        <div>
            <asp:TextBox runat="server" ID="result" />
        </div>
    </form>
</body>
</html>
