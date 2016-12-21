<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>个人博客--注册</title>
    <link rel="stylesheet" href="./css/bootstrap.css"/>
    <link rel="stylesheet" href="./css/register.css"/>
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" class="form-group" runat="server">
    <div>
    
<div class="container">  <!--使用支持响应式布局的容器类型，另一个是container-fluid占据全部视口（viewpoint）的容器-->
        <div class="main">
            <div class="title">
                个人博客登录
            </div>
            <div class="div-input-box">
                <label for="username">用户名</label>
                <asp:TextBox ID="username" runat="server" placeholder="必填：长度在2-20个" 
                    required="required"></asp:TextBox>
            </div>
            <div class="div-input-box">
                <label for="email">邮箱</label>
                <asp:TextBox ID="email" runat="server" TextMode="Email" placeholder="必填：example@email.com"
                    required="required" ></asp:TextBox>
            </div>
            <div class="div-input-box">
                <label for="birth">破蛋日</label>
                <asp:TextBox ReadOnly="true" ID="birth" runat="server" placeholder="请先在下列输入框输入年份再在日历选择具体日期"></asp:TextBox>
                <asp:TextBox ID="year_s" runat="server" AutoPostBack="true" TextMode="Number" OnTextChanged="year_s_TextChanged" placeholder="请输入年份"></asp:TextBox>
                <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
            </div>
            <div class="div-input-box">
                <label for="pwd">密码</label>
                <asp:TextBox ID="pwd" runat="server" TextMode="Password" placeholder="必填"
                    required="required" ></asp:TextBox>
            </div>
            <div class="div-input-box">
                <label for="confirm-pwd">确认密码</label>
                <asp:TextBox ID="confirm_pwd" runat="server" TextMode="Password" placeholder="必填"
                    required="required" ></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="Reg" style="margin: auto 0 auto 80px;" runat="server" OnClick="register_Click" Text="注册" />
            </div>
        </div>
</div>
    </div>
    </form>
</body>
</html>
