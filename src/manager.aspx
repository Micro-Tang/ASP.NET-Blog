﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="manager.aspx.cs" Inherits="add_manager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Blog--留言板管理</title>
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
                            <a class="navbar-brand" href="manage_index.aspx">Newbee Blog--后台管理</a>
                        </div>
                        <div>
                            <!--向左对齐-->
                            <!--向右对齐-->
                            <ul class="nav navbar-nav navbar-right">
                                <li>
                                    <asp:Button ID="logout" runat="server" Style="margin: 5px;" Text="注销" class="btn btn-primary" OnClick="logout_Click"></asp:Button></li>
                            </ul>
                        </div>
                    </div>
                </nav>
            </div>

            <div class="container-fluid">
                <div class="row">
                    <!--侧边栏-->
                    <div class="container-fluid">
                        <ul class="nav nav-tabs">
                            <li><a href="manage_index.aspx">系统详情</a></li>
                            <li><a href="manage_user.aspx">用户管理</a></li>
                            <li><a href="manage_post.aspx">文章管理</a></li>
                            <li><a href="manage_msgbook.aspx">留言板管理</a></li>
                            <%if (Session["admin"].ToString() == "2")
                              {%>
                            <li class="active"><a href="manager.aspx">管理员管理</a></li>
                            <%} %>
                        </ul>

                        <!--主面板-->
                        <div class="panel">
                            <div class="panel-body">
                                <table class="table">
                                    <tr>
                                        <th colspan="2">添加管理员</th>
                                    </tr>
                                    <tr>
                                        <th>用户</th>
                                        <th style="width: 10%">操作</th>
                                    </tr>
                                    <asp:Repeater ID="user" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("username") %></td>
                                                <td style="width: 10%">
                                                    <asp:LinkButton ID="add" class="btn btn-primary" runat="server" CommandArgument='<%#Eval("user_id") %>' OnClientClick="return confirm('是否添加')" OnClick="add_Click">添加</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </div>
                        <ul class="nav nav-list">
                            <li class="divider"></li>
                        </ul>
                        <div class="panel">
                            <div class="panel-body">
                                <table class="table">
                                    <tr>
                                        <th colspan="2">删除管理员</th>
                                    </tr>
                                    <tr>
                                        <th>用户</th>
                                        <th style="width: 10%">操作</th>
                                    </tr>
                                    <asp:Repeater ID="manager_lst" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("username") %></td>
                                                <td style="width: 10%">
                                                    <asp:LinkButton ID="del" class="btn btn-danger" runat="server" CommandArgument='<%#Eval("user_id") %>' OnClientClick="return confirm('是否删除')" OnClick="del_Click">删除
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
