<%@ Page Language="C#" AutoEventWireup="true" CodeFile="send_msg.aspx.cs" Inherits="send_msg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>发送私信</title>
    <style type="text/css">
        body {
            overflow: scroll;
            overflow-x: hidden;
            padding: 51px 0;
            background-image: url('images/bg.jpg');
        }

        a {
            font-size: 15px;
        }
    </style>
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/common.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container-fluid">
                <nav class="navbar navbar-default navbar-fixed-top" role="navigation">
                    <div class="container-fluid">
                        <div class="navbar-header">
                            <a class="navbar-brand" href="index.aspx">Newbee Blog</a>
                        </div>
                        <div>
                            <!--向左对齐-->
                            <ul class="nav navbar-nav navbar-left">
                                <% if (Session["username"] != null)
                                    { %>
                                <li><a href="article-write.aspx">PO文 <span class="glyphicon glyphicon-pencil"></span></a></li>
                                <%} %>
                            </ul>
                            <!--向右对齐-->
                            <ul class="nav navbar-nav navbar-right">
                                <li><a href="msgbook.aspx"><span class="glyphicon glyphicon-book"></span>留言板</a></li>
                                <% if (Session["username"] != null)
                                    { %>
                                <li><a href="photo_dir.aspx"><span class="glyphicon glyphicon-picture"></span> 我的相册</a></li>
                                <li><a href="msg.aspx"><span class="glyphicon glyphicon-book"></span>消息</a></li>
                                <li><a href="friends.aspx"><span class="glyphicon glyphicon-user"></span>博友</a></li>
                                <%} %>
                                <% if (Session["username"] == null)
                                    { %>
                                <li>
                                    <button style="margin: 5px;" type="button" class="btn btn-success" onclick="window.location.href='login.aspx'"><span class="glyphicon glyphicon-log-out"></span>登陆</button></li>
                                <li>
                                    <button style="margin: 5px;" type="button" class="btn btn-success" onclick="window.location.href='register.aspx'"><span class="glyphicon glyphicon-log-out"></span>注册</button></li>
                                <% }
                                    else
                                    { %>
                                <li>
                                    <asp:Button ID="logout" runat="server" Style="margin: 5px;" Text="注销" class="btn btn-success" OnClick="logout_Click"></asp:Button></li>
                                <% } %>
                            </ul>
                        </div>
                    </div>
                </nav>
            </div>
            <div class="container">
                <div class="panel">
                    <h4 style="margin: 0 auto">发送私信给<asp:Label ID="user" runat="server" Text=""></asp:Label>: </h4>
                    <textarea style="resize: none;" required="required" runat="server" id="msg" rows="6" cols="59" placeholder="发送私信"></textarea>
                    <asp:Button style="margin-top: 5px;float: right" ID="send_msg_btn" class="btn btn-primary" OnClick="send_msg_btn_Click" runat="server" Text="私信" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
