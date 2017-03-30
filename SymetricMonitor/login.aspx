<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SymetricMonitor.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <title>Logini</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <meta name="description" content="A complete admin panel theme"/>
    <meta name="author" content="ideal Solution"/>

    <!-- styles -->
    <link href="css/utopia-white.css" rel="stylesheet"/>
    <link href="css/utopia-responsive.css" rel="stylesheet"/>
    <link href="css/validationEngine.jquery.css" rel="stylesheet"/>

    <!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
    <script src="http://uniformlocal.org/assets/js/html5.js"></script>
    <![endif]-->

</head>
<body>
    <form id="form1" runat="server">
       <div class="container-fluid">
    <div class="row-fluid" style="margin-top: 150px; height: 223px;">
        <div class="span12">
            <div class="row-fluid">
                <div style="margin:0 auto; width: 230px;">
                    <div class="utopia-login">
                        <label>Username:</label>
                        <asp:TextBox ID="txtUsername" runat="server" class="span12 utopia-fluid-input"></asp:TextBox>
                       <!-- <input type="text" name="email" id="email" value="" class="span12 utopia-fluid-input"  />-->
                        <label>Password:</label>
                        <asp:TextBox ID="txtPassword" runat="server"  class="span12 utopia-fluid-input" TextMode="Password"></asp:TextBox>
                       <!-- <input type="password" name="password" id="password" value="" class="span12 utopia-fluid-input"  />-->
                        <ul class="utopia-login-action">
                            <li>
       <asp:Button ID="Login" runat="server" Text="Login" class="btn span4" OnClick="Login_Click" />
                      
                                 <!--         <button type="button" name="login" id="login" class="btn span4"  > Login </button>-->
                            </li>
                        </ul>
                    </div>
                    <asp:Label ID="lblmsg" runat="server" Text="Label" Visible="False"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</div>
    </form>
</body>
</html>
