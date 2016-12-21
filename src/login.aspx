<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>个人博客--登陆</title>
    <link rel="stylesheet" href="css/login.css" />
    <link rel="stylesheet" href="css/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="login">
                <div class="title">
                    个人博客登录
                </div>
                <asp:TextBox ID="username" runat="server" class="input-text"
                    placeholder="请输入用户名" required="required"></asp:TextBox>
                <br />
                <asp:TextBox ID="password" runat="server" TextMode="Password" class="input-text"
                    placeholder="请输入密码" required="required"></asp:TextBox>
                <div>
                    <asp:Button class="btn btn-default" ID="submit" runat="server" OnClick="submit_Click" Text="登陆" />
                    <button type="button" class="btn btn-default" onclick="javascript:window.location.href='register.aspx'">注册</button>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
