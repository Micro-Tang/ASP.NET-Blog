<%@ Page Language="C#" AutoEventWireup="true" CodeFile="manage_index.aspx.cs" Inherits="manage_index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Blog--后台管理</title>
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
                            <li class="active"><a href="manage_index.aspx">系统详情</a></li>
                            <li><a href="manage_user.aspx">用户管理</a></li>
                            <li><a href="manage_post.aspx">文章管理</a></li>
                            <li><a href="manage_msgbook.aspx">留言板管理</a></li>
                            <%if(Session["admin"].ToString() == "2") {%>
                            <li><a href="manager.aspx">管理员管理</a></li>
                            <%} %>
                        </ul>

                        <!--主面板-->
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-3"><strong>系统名称：</strong></div>
                                    <div class="col-md-9"><strong>Newbee Blog</strong></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3"><strong>系统管理员：</strong></div>
                                    <div class="col-md-9"><strong><%=Session["admin_username"].ToString() %></strong></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3"><strong>系统开发人员：</strong></div>
                                    <div class="col-md-9"><strong>唐堃，常雪姣，刘靖雯，古丽娜孜</strong></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3"><strong>服务器机器名：</strong></div>
                                    <div class="col-md-9"><strong><%=Server.MachineName %></strong></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3"><strong>服务器IP：</strong></div>
                                    <div class="col-md-9"><strong><%=Request.ServerVariables["LOCAL_ADDR"] %></strong></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3"><strong>服务器域名：</strong></div>
                                    <div class="col-md-9"><strong><%=Request.ServerVariables["SERVER_NAME"] %></strong></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3"><strong>服务器操作系统版本：</strong></div>
                                    <div class="col-md-9"><strong><%=System.Environment.OSVersion.ToString() %></strong></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3"><strong>服务器IIS版本：</strong></div>
                                    <div class="col-md-9"><strong><%=Request.ServerVariables["SERVER_SOFTWARE"] %></strong></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3"><strong>访客信息：</strong></div>
                                    <div class="col-md-9"><strong><%=Request.ServerVariables["HTTP_USER_AGENT"] %></strong></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
