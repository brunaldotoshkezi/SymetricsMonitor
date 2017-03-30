<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="logs.aspx.cs" Inherits="SymetricMonitor.logs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <title>Logs</title>
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

        </style>

</head>
<body>
    <form id="form1" runat="server">
  <div class="container-fluid">
    <!-- Header starts -->
<div class="row-fluid">
    <div class="span12">
        <div class="header-top">
            <div class="header-wrapper">
                    
                <h1 ><img src="/img/SymmetricDS.png" alt="SymmetricDS">  - Logs</h1>
                <div class="header-right">

                    <div class="header-divider">&nbsp;</div>

                    <div class="search-panel header-divider">
                        <div class="search-box">
      <asp:DropDownList ID="Switch" runat="server" AutoPostBack="True" ></asp:DropDownList>
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
                     <asp:LinkButton ID="lnkconfig" runat="server" class="list" OnClick="lnkconfig_Click"> <span>Configuration</span></asp:LinkButton>
                    <!--<a class="list" href="wizard.aspx" title="Users">
                       
                    </a>-->
                   
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

                                        &nbsp;
                                       <!-- <a href="javascript:;" class="btn addNewAgency">
                                            <i class="icon-plus"></i>
                                            New Agency
                                        </a>-->
                                        
                                       <!-- <a href="javascript:void(0);" class="btn newCategory">
                                            <i class="icon-plus"></i>
                                            New Category
                                        </a>-->
                                    </div>
                                </div>

                                <div class="span9" style="width: 611px;">
                                    <table>
                                        <tr>
                                            <td valign="middle">
                                                <div class="input-append">
                                                     <asp:TextBox ID="searchBox" runat="server" class="textbox"></asp:TextBox>
                                                 <!--   <input type="text" name="searchBox" id="searchBox" value="" class="textbox" />  --> 
                                                  <asp:Button ID="searchBtn" runat="server" Text="Search " class="btn searchBtn"  style="margin-left:-5px" OnClick="searchBtn_Click" />
                                                    <asp:LinkButton ID="lnkRefresh" class="btn" runat="server" style="margin-left:-5px" OnClick="lnkRefresh_Click" ><i class="icon-refresh"></i>Refresh</asp:LinkButton>  <!--<a href="logs.aspx" class="btn" style="margin-left:-5px">
                                                        <i class="icon-refresh"></i>
                                                        Refresh
                                                    </a>-->
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <div class="row-fluid" style="margin-top:11px;">
                                <div class="span12">
                                    <div class="utopia-widget-content">
                                        <div id="customers_ctl" style="display:inline;">
                                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" border="0" cellpadding="0" class="table table-striped table-bordered" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand" OnRowUpdating="GridView1_RowUpdating">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="id" ShowHeader="false" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("id") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="channel" ShowHeader="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="channel_id" runat="server" Text='<%# Eval("channel_id") %>' Width="50%"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="table" ShowHeader="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="table_name" runat="server" Text='<%# Eval("table_name") %>' Width="70%"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="event" ShowHeader="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="event_type" runat="server" Text='<%# Eval("event_type") %>' Width="30%"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="data" ShowHeader="true">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="row_data" runat="server" Text='<%# Eval("row_data") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="router" ShowHeader="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="router_id" runat="server" Text='<%# Eval("router_id") %>' Width="70%"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="status" ShowHeader="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="status" runat="server" Text='<%# Eval("status") %>' Width="50"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="message" ShowHeader="true">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="error" runat="server" Text='<%# Eval("error") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ShowHeader="false">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnedit" runat="server" class="btn" CommandArgument='<%# Eval("id") %>' commandname="Update" Text="Edit" Visible='<%# Convert.ToString(Eval("status"))=="ER" && Convert.ToString(Session["UserName"])=="admin" %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            Nuk u gjenden te dhena!
                                                        </EmptyDataTemplate>
                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" Position="Top" />
                                                    </asp:GridView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="searchBtn" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="lnkRefresh" EventName="Click" />
                                                </Triggers>
                                                
                                            </asp:UpdatePanel>
           
                                           <br />
                                              
                                        </div>
                                    </div>
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
    <p class="muted credit" align="center"> Copyright &nbsp; <a href="http://ideal.al"  style="margin-left:-5px" >Ideal Solution</a></p>
 </div>
    </form>
    
</body>
</html>
