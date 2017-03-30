<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wizard.aspx.cs" Inherits="SymetricMonitor.wizard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <title>Configurimi</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <meta name="description" content="A complete admin panel theme"/>

    <!-- styles -->

    <link href="css/utopia-white.css" rel="stylesheet"/>
    <link href="css/utopia-responsive.css" rel="stylesheet"/>
    <style type="text/css">

        #newCategoryInputCollection td {
            padding-left: 7px;
            padding-right: 7px;
        }

        #customers td {
            vertical-align: middle !important;
        }

        .activeCustomers label {
            margin: 0;
        }

        .activeCustomers td {
            padding: 3px;
        }

        .activeCustomers {
            padding-left: 15px;
        }

        #addCustomer td {
            padding-left: 7px;
            padding-right: 7px;
            width: 200px;
        }

        .generateRandomPass td:first-child {
            width: 10px !important;
            vertical-align: top;
            padding-left: 0 !important;
            padding-right: 0 !important;
        }

        .generateRandomPass td label {
            margin: 0;
            padding-top: 1px;
        }

        .error {
            color: red;
        }

        .productDetails td {
            padding-left: 7px;
            padding-right: 7px;
            width: 200px;
        }

        .colorDetails td {
            padding-left: 7px;
            padding-right: 7px;
        }

        .error {
            color: red;
        }

        #users td {
            vertical-align: middle !important;
        }

        #productsThumb ul {
            list-style: none;
            display: block;
            margin: 0;
            padding: 0;
        }

        #productsThumb ul li {
            float: left;
            padding: 4px;
            margin-right: 8px;
            margin-bottom: 8px;
            cursor: pointer;
        }

        #productsThumb ul li .libImage {
            border: 5px solid white;
            display: block;
            box-shadow: 0 0 10px 0 rgba(0, 0, 0, 0.25);
            -moz-box-shadow: 0 0 10px 0 rgba(0, 0, 0, 0.25);
            -webkit-box-shadow: 0 0 10px 0 rgba(0, 0, 0, 0.25);
        }

        #productsThumb ul li a.removeImage {
            margin-left: 167px;
            margin-top: -6px;
            position: absolute;
            text-decoration: none;
        }

        #productsThumb ul li a.removeImage img {
            border: none;
        }

        .right {
            width: 73px;
        }
        .left {
            width: 209px;
        }
   
      table { 
    border-spacing: 5px;
    border-collapse: separate;
}
         td {
    padding: 5px;
}

        
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid">
    <!-- Header starts -->
<div class="row-fluid">
    <div class="span12">
        <div class="header-top">
              
            <div class="header-wrapper" >
                 
 <h1 ><img src="img/SymmetricDS.png" alt="SymmetricDS"> - Configuration</h1>

                <div class="header-right">

                    <div class="header-divider">&nbsp;</div>

                    <div class="search-panel header-divider">
                        <div class="search-box">
     
                          <!--  <input type="text" name="search" style="width: 270px;" placeholder="Search"/>-->
                        </div>
                    </div>

                    <div class="header-divider">
                        <asp:Button ID="btnlogout" runat="server" Text="Logout" class="btn" OnClick="btnlogout_Click1"  />
                      
                    </div>
                    <!-- User panel end -->
                </div>
                <!-- End header right -->
            </div>
            <!-- End header wrapper -->
        </div>
        <!-- End header -->
    </div>
</div>
      </div>
<!-- Header ends -->


<div class="row-fluid">        
<!-- Sidebar start -->
<div class="span2 sidebar-container">
    <div class="sidebar">
        <div class="nav-collapse collapse leftmenu">
            <ul>
                <li>
                    <a class="dashboard smronju" href="dashboard.aspx" title="Dashboard">
                        <span>Dashboard</span>
                    </a>
                </li>
                <li >
                    <a class="list" href="logs.aspx" title="Users">
                        <span>Logs</span>
                    </a>
                   
                </li>
                 <li >
                    <a class="list" href="wizard.aspx" title="Users">
                        <span>Configuration</span>
                    </a>
                   
                </li>

                
            </ul>

        </div>

    </div>
</div>
<!-- Sidebar end -->
        <!-- Body start -->
        <div class="span10 body-container">
            <div class="row-fluid">
                <div class="span12">
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="row-fluid">
                                <div class="span3" style="width: 250px;">
                                    <div class="btn-group">

                                    
                                    </div>
                                </div>

                                <div class="span9" style="width: 608px;">
                                    <table>
                                        <tr>
                                            <td valign="middle">
                                                <div class="input-append">
                                                   
                                                    <a href="wizard.aspx" class="btn" style="margin-left:-5px">
                                                        <i class="icon-refresh"></i>
                                                        Refresh
                                                    </a>
                                                     &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="updatedbbtn" runat="server" class=" btn btn-primary" Text="Update Database" OnClick="updatedbbtn_Click" />
                                                   &nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="msglbl" runat="server" Text="Message" Visible="False"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <div class="row-fluid" style="margin-top:11px;">
                                <div class="span12">
                                    <div class="utopia-widget-content"  >
                                        <br />
                                    <!--    <div id="customers_ctl" style="display:inline;">-->
        
                                      <!--  </div>-->
                                        <asp:Panel ID="Panel1" runat="server" >
                                            <asp:Wizard ID="Wizard1" runat="server" Height="80%" Width="80%" ActiveStepIndex="2" OnNextButtonClick="Wizard1_NextButtonClick1" BorderWidth="1px"  CellPadding="4" CellSpacing="2"
          SideBarStyle-VerticalAlign="Top" SideBarStyle-Wrap="False" SideBarButtonStyle-CssClass="btn btn-default btn-lg active" style=" background-color:azure" SideBarButtonStyle-Width="130px" OnFinishButtonClick="Wizard1_FinishButtonClick"  >
                                                <FinishCompleteButtonStyle CssClass="btn btn-success" />
                                                <FinishPreviousButtonStyle CssClass="btn btn-warning" />
                                                <StartNextButtonStyle CssClass="btn btn-info" />
                                                <StepNextButtonStyle CssClass="btn btn-info" />
                                                <StepPreviousButtonStyle CssClass="btn btn-warning" />
                                                <SideBarButtonStyle CssClass="btn btn-default btn-lg active" Width="130px" />
                                                <SideBarStyle VerticalAlign="Top" />
                                                <WizardSteps >
                                                    <asp:WizardStep ID="WizardStep1"   runat="server" StepType="Start" Title="Server Node Config" >
                                                       <table>
                                                           <tr>
                                                               <td>Database</td>
                                                               <td><asp:DropDownList ID="dbserver" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dbserver_SelectedIndexChanged">
                                                        </asp:DropDownList></td>
                                                           </tr>
                                                           <tr>
                                                               <td>UrlConnection:</td>
                                                               <td><asp:TextBox ID="urlctxt" runat="server" Width="298px"></asp:TextBox></td>
                                                           </tr>
                                                           <tr>
                                                               <td> User:</td>
                                                               <td><asp:TextBox ID="usertxt" runat="server" Width="197px"></asp:TextBox></td>
                                                           </tr>
                                                           <tr>
                                                               <td>  Password:</td>
                                                               <td><asp:TextBox ID="passtxt" runat="server" Width="197px"></asp:TextBox></td>
                                                           </tr>
                                                             <tr>
                                                               <td>UrlRegistation:</td>
                                                               <td><asp:TextBox ID="urlregtxt" runat="server" Width="197px"></asp:TextBox></td>
                                                           </tr>
                                                       </table>
 
                                                        
                                                    </asp:WizardStep>
                                                    <asp:WizardStep ID="WizardStep2"  runat="server" StepType="Step" Title="Client Node Config">
                                                        <table>
                                                            <tr>
                                                                <td>Database</td>
                                                                <td> <asp:DropDownList ID="dbclient" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dbclient_SelectedIndexChanged">
                                                        </asp:DropDownList></td>
                                                            </tr>
                                                              <tr>
                                                                <td> UrlConnection:</td>
                                                                <td> <asp:TextBox ID="urlconcltxt" runat="server" Width="298px"></asp:TextBox>
                                                      </td>
                                                            </tr>
                                                            <tr>
                                                                <td>User:</td>
                                                                <td>  <asp:TextBox ID="usercltxt" runat="server" Width="197px"></asp:TextBox></td>
                                                            </tr>
                                                             <tr>
                                                                <td>Password:</td>
                                                                <td><asp:TextBox ID="passcltxt" runat="server" Width="197px"></asp:TextBox></td>
                                                            </tr>
                                                              <tr>
                                                                <td>UrlRegistation:</td>
                                                                <td><asp:TextBox ID="urlclregtxt" runat="server" Width="197px"></asp:TextBox></td>
                                                            </tr>
                                                        </table>

                                                    </asp:WizardStep>
                                                    <asp:WizardStep ID="WizardStep3" runat="server" Title="Node Rename">
                                                       <table>
                                                           <tr>
                                                               <td><asp:Button ID="procesbtn" runat="server" OnClick="procesbtn_Click" Text="Proces" Width="170px" class="btn" />&nbsp;&nbsp;&nbsp;</td>
                                                               <td><asp:CheckBox ID="processsymchk" runat="server" Height="22px"  Width="27px" />Table Create</td>
                                                           </tr>
                                                            <tr>
                                                               <td> <asp:Button ID="scriptbtn" runat="server" OnClick="scriptbtn_Click" Text="nodecreate" Width="170px" class="btn" />&nbsp;&nbsp;&nbsp;</td>
                                                               <td><asp:CheckBox ID="scriptchk" runat="server" Height="22px" Width="27px" />Script Execute</td>
                                                           </tr>
                                                             <tr>
                                                               <td><asp:Button ID="StartServerbtn" class="btn" runat="server" OnClick="StartServerbtn_Click" Text="Start Server" Width="172px" />&nbsp;&nbsp;&nbsp;</td>
                                                               <td>
                                                                   <asp:Button ID="StartClientbtn" runat="server" class="btn" OnClick="StartClientbtn_Click" Text="Start Client" Width="172px" />
                                                                 </td>
                                                           </tr>
                                                             <tr>
                                                               <td><asp:Button ID="registatenodebtn" runat="server" OnClick="registatenodebtn_Click" Text="Registrate Node"  Width="172px" class="btn"/>&nbsp;&nbsp;&nbsp;</td>
                                                               <td> 
                                                                   <asp:CheckBox ID="reloadchk" runat="server" AutoPostBack="True" Height="22px" OnCheckedChanged="reloadchk_CheckedChanged" Width="27px" />
                                                                   Reload</td>
                                                           </tr>
                                                              <tr>
                                                               <td> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Disable Trigger:</td>
                                                               <td>   <asp:DropDownList ID="Switch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Switch_SelectedIndexChanged"></asp:DropDownList></td>
                                                           </tr>
                                                       </table>
  
                                                    </asp:WizardStep>
                                                </WizardSteps>
                                            </asp:Wizard>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

 <div class="navbar navbar-inner  navbar-fixed-bottom">
    <p class="muted credit" align="center"> Copyright&nbsp;&nbsp;&nbsp; &nbsp; <a href="http://ideal.al"  style="margin-left:-5px" >Ideal Solution</a></p>
 </div>
    </form>
</body>
</html>
