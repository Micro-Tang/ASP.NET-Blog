<%@ Page Language="C#" AutoEventWireup="true" CodeFile="msgbook.aspx.cs" Inherits="msgbook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Blog--留言板</title>
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
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
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

            <div style="width: 85%; margin: 0 auto; background-color: #eee;">
                <textarea style="width: 98%; margin: 5px; resize: none" runat="server" required="required"
                    placeholder="请输入内容" id="msg_content" rows="5"></textarea>
                <asp:Button ID="Button1" style="margin-left: 5px" class="btn btn-primary" OnClick="Button1_Click" runat="server" Text="留言" />

                <div class="container-fluid" style="margin-top: 10px;">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <div class="panel">
                                <div class="panel-body">
                                    <div class="row" style="margin-bottom: 2px;">
                                        <div class="col-md-2">
                                            <strong><%#Eval("username") %>：</strong>
                                        </div>
                                        <div class="col-md-10">
                                            <p>
                                                <strong><%#Eval("content") %></strong>
                                            </p>
                                            <h4><small><%#Eval("date") %></small></h4>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
