<%@ Page Language="C#" AutoEventWireup="true" CodeFile="manage_post.aspx.cs" Inherits="manage_post" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Blog--文章管理</title>
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
                            <li class="active"><a href="manage_post.aspx">文章管理</a></li>
                            <li><a href="manage_msgbook.aspx">留言板管理</a></li>
                            <%if (Session["admin"].ToString() == "2")
                              {%>
                            <li><a href="manager.aspx">管理员管理</a></li>
                            <%} %>
                        </ul>

                        <!--主面板-->
                        <div class="panel">
                            <div class="panel-body">
                                <table class="table">
                                    <tr>
                                        <th style="width: 10%">标题</th>
                                        <th>概要</th>
                                        <th>时间</th>
                                        <th>操作</th>
                                    </tr>
                                    <asp:Repeater ID="post_lst" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="width: 10%"><%#Eval("title") %></td>
                                                <td><%#Eval("shortcontent") %></td>
                                                <td><%#Eval("post_date") %></td>
                                                <td>
                                                    <asp:LinkButton ID="del" class="btn btn-danger" runat="server" CommandArgument='<%#Eval("post_id") %>' OnClientClick="return confirm('是否删除')" OnClick="del_Click">删除</asp:LinkButton>

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
