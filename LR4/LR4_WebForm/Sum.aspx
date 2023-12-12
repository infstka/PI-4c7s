<%@ Page Title="Sum" Language="C#" AutoEventWireup="true" CodeBehind="Sum.aspx.cs" Inherits="LR4_WebForm.Sum" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-bottom: 10px;">
            <asp:TextBox runat="server" ID="A1_s" placeholder="string" />
            <asp:TextBox runat="server" ID="A1_k" placeholder="int" />
            <asp:TextBox runat="server" ID="A1_f" placeholder="float" /><br />
        </div>

        <div style="margin-bottom: 10px;">
            <asp:TextBox runat="server" ID="A2_s" placeholder="string" />
            <asp:TextBox runat="server" ID="A2_k" placeholder="int" />
            <asp:TextBox runat="server" ID="A2_f" placeholder="float" />
        </div>

        <div style="margin-bottom: 10px;">
            <asp:TextBox runat="server" ID="result_s" />
            <asp:TextBox runat="server" ID="result_k" />
            <asp:TextBox runat="server" ID="result_f" />
        </div>

        <asp:Button runat="server" ID="sum" OnClick="Sum_Click" Text="Sum" />

    </form>
</body>
</html>
