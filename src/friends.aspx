﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="friends.aspx.cs" Inherits="friend" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Blog--好友列表</title>
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
                <div class="row">
                    <div class="col-md-8" style="margin: 50px 0 0 0;">
                        <div class="container-fluid" style="padding: 10px">
                            <div class="row">
                                <asp:Repeater ID="f_lst" runat="server">
                                    <ItemTemplate>
                                        <div class="col-lg-4">
                                            <div class="panel" style="width: 250px; height: 250px;">
                                                <div class="panel-body">
                                                    <p>
                                                        <img src="./images/face_<%=new Random().Next(44).ToString() %>.png"><span style="font-size: 21px; margin: 0 0 0 10px"><%#Eval("username") %></span>
                                                    </p>
                                                    <hr style="border: solid 1px #eee; margin: 0 0 20px 0" />
                                                    <p>邮箱：<strong><%#Eval("email") %></strong></p>
                                                    <p>破蛋日：<strong><%#Eval("birthday") %></strong></p>
                                                    <ul class="nav nav-pills" role="tablist">
                                                        <li role="presentation" class="active"><a href="add_tag.aspx?pid=<%#Eval("user_id") %>">添加标签</a></li>
                                                        <li role="presentation" class="active"><a href="send_msg.aspx?pid=<%#Eval("user_id") %>">发送私信</a></li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4"
                        style="min-height: 620px; background-color: #eee; border-left: solid 20px #eee; margin: 50px 0 0 0">
                        <div class="container-fluid" style="min-height: 300px; margin: 0; padding: 0">
                            <h4>
                                <small>Po主信息：</small>
                            </h4>
                            <hr style="border: solid 1px white;" />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="panel" style="min-height: 200px;">
                                        <div class="panel-body">
                                            <p>
                                                <img src="./images/face_<%=new Random().Next(44).ToString() %>.png" /><span style="font-size: 21px; margin: 0 0 0 10px"><asp:Label ID="username" runat="server" Text=""></asp:Label></span>
                                            </p>
                                            <hr style="border: solid 1px #eee; margin: 0 0 20px 0" />
                                            <p>邮箱：<strong><asp:Label ID="email" runat="server" Text=""></asp:Label></strong></p>
                                            <p>破蛋日：<strong><asp:Label ID="birthday" runat="server" Text=""></asp:Label></strong></p>
                                            <p>
                                                标签：<strong><asp:Label ID="tags" runat="server" Text=""></asp:Label></strong>&nbsp;
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <h4>
                                <small>友情链接：</small>
                            </h4>
                            <hr style="border: solid 1px white;" />
                            <div class="row">
                                <%if (Session["username"] != null)
                                    { %>
                                <asp:Repeater ID="flink" runat="server">
                                    <ItemTemplate>
                                        <div class="col-md-12">
                                            <div class="panel" style="margin: 5px 0">
                                                <div class="panel-body">
                                                    <p>
                                                        <span style="font-size: 21px; margin: 0 0 0 10px"><asp:HyperLink runat="server" Target ="_blank" NavigateUrl='<%#Eval("url") %>'><%#Eval("title") %></asp:HyperLink></span>
                                                        <asp:LinkButton style="float:right" ID="del_flink_btn" runat="server" CommandArgument='<%#Eval("friend_url_id") %>'  OnClientClick="return confirm('是否删除')" OnClick="Delflink" >删除</asp:LinkButton>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <button type="button" onclick="window.location.href='add_flink.aspx'" class="form-control btn-success">添加友链</button>
                                <%} %>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
