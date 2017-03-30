<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="SymetricMonitor.dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <title>Dashboard</title>
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

        .auto-style2 {
            width: 239px;
        }
        .auto-style3 {
            width: 168px;
        }

        .auto-style5 {
            width: 191px;
        }

        .auto-style10 {
            width: 166px;
        }
        .auto-style11 {
            width: 190px;
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
                 
 <h1 ><img src="img/SymmetricDS.png" alt="SymmetricDS">  - Dashboard</h1>

                          
           <h3 >  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;Server:<asp:Label ID="lblServer" runat="server" Text="Offline"  Font-Bold="True" ForeColor="Red"></asp:Label>
  &nbsp;&nbsp;&nbsp;&nbsp;Client:<asp:Label ID="lblClient" runat="server" Text="Offline"  Font-Bold="True" ForeColor="Red" ></asp:Label></h3>
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
                     <asp:LinkButton ID="lnkconfig" runat="server" class="list" OnClick="lnkconfig_Click" > <span>Configuration</span></asp:LinkButton>
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

                                        <asp:Button ID="btnstartclient" runat="server" Text="Start Client" class="btn addNewAgency" OnClick="btnstartclient_Click"  />&nbsp;
                                       <!-- <a href="javascript:;" class="btn addNewAgency">
                                            <i class="icon-plus"></i>
                                            New Agency
                                        </a>-->
                                   <asp:Button ID="btnstartserver" runat="server" Text="Start Server" class="btn addNewAgency" OnClick="btnstartserver_Click"  />
                                        
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
                                                   
                                                    <a href="dashboard.aspx" class="btn" style="margin-left:-5px">
                                                        <i class="icon-refresh"></i>
                                                        Refresh
                                                    </a>
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
                                        
          <table id="customers" class="table table-striped table-bordered"  >
                                                <caption>
                                                    </caption>
                                                <thead>
                                                  <tr >
                                                    <th class="auto-style11" >Status</th>
                                                    <th class="auto-style10" >Incoming</th>
                                                    <th class="auto-style2">Outgoing</th>
                                                    
                                                  </tr>
                                                </thead>
                                                <tbody>
                                                     <tr>
                                    <td class="auto-style11">
                                        
                                            Errors:
                                      
                                    </td>
                                    <td class="auto-style10">
                                         <asp:LinkButton ID="lnkb_error_in" runat="server" OnClick="lnkb_error_in_Click">0</asp:LinkButton>
      
                                    </td>
                                     <td class="auto-style2" >
                                        
                                       <asp:LinkButton ID="lnkb_error_out" runat="server" OnClick="lnkb_error_out_Click">0</asp:LinkButton>
                                         
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style11">
                                         Loading:
                                           </td>
                                    
                                    <td  class="auto-style10">
                                        <asp:LinkButton ID="lnkb_load_in" runat="server" OnClick="lnkb_load_in_Click">0</asp:LinkButton>
                                                
                                         </td>
                                    <td  class="auto-style2">
                                        <asp:LinkButton ID="lnkb_load_out" runat="server" OnClick="lnkb_load_out_Click">0</asp:LinkButton>
                                               
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style11">
                                       New:
                                    </td>
                                    <td  class="auto-style10">
                                        
                                                   <asp:LinkButton ID="lnkb_new_in" runat="server" OnClick="lnkb_new_in_Click">0</asp:LinkButton>
                                     
                                    </td>
                                    <td  class="auto-style2">
                                        
                                               
                                                 <asp:LinkButton ID="lnkb_new_out" runat="server" OnClick="lnkb_new_out_Click">0</asp:LinkButton></td>
                                        
                                </tr>
                                                    <tr>
                                    <td class="auto-style11">
                                       OK:
                                    </td>
                                    <td  class="auto-style10">
                                        
                                                <asp:LinkButton ID="lnkb_ok_in" runat="server" OnClick="lnkb_ok_in_Click">0</asp:LinkButton>
                                     
                                    </td>
                                    <td  class="auto-style2">
                                        
                                               
                                               <asp:LinkButton ID="lnkb_ok_out" runat="server" OnClick="lnkb_ok_out_Click">0</asp:LinkButton></td>
                                        
                                </tr>
                                                </tbody>
                                            </table>
                                           <br />
                                              <table id="type" class="table table-striped table-bordered"  >
                                                <caption>
                                                    </caption>
                                                <thead>
                                                  <tr >
                                                    <th class="auto-style5" >Tipi</th>
                                                    <th class="auto-style3" >Incoming</th>
                                                    <th class="auto-style2">Outgoing</th>
                                                    
                                                  </tr>
                                                </thead>
                                                <tbody>
                                                     <tr>
                                    <td class="auto-style5">
                                        
                                            Insert:
                                      
                                    </td>
                                    <td class="auto-style3" >
                                         <asp:LinkButton ID="lnkb_insert_in" runat="server" OnClick="lnkb_insert_in_Click">0</asp:LinkButton>
              
                                    </td>
                                     <td class="auto-style2" >
                                         <asp:LinkButton ID="lnkb_insert_out" runat="server" OnClick="lnkb_insert_out_Click">0</asp:LinkButton>
                                      
                                         
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5">
                                         Update:
                                           </td>
                                    
                                    <td  class="auto-style3">
                                        <asp:LinkButton ID="lnkb_update_in" runat="server" OnClick="lnkb_update_in_Click">0</asp:LinkButton>
                                           
                                         </td>
                                    <td  class="auto-style2">
                                        <asp:LinkButton ID="lnkb_update_out" runat="server" OnClick="lnkb_update_out_Click">0</asp:LinkButton>
                              
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5">
                                       Delete:
                                    </td>
                                    <td  class="auto-style3">
                                        <asp:LinkButton ID="lnkb_delete_in" runat="server" OnClick="lnkb_delete_in_Click">0</asp:LinkButton>
                                                
                                     
                                    </td>
                                    <td  class="auto-style2">
                                        
                                               
                                 <asp:LinkButton ID="lnkb_delete_out" runat="server" OnClick="lnkb_delete_out_Click">0</asp:LinkButton>
                                    </td>    
                                </tr>
                                                    <tr>
                                    <td class="auto-style5">
                                       Reload:
                                    </td>
                                    <td  class="auto-style3">
                                        <asp:LinkButton ID="lnkb_reload_in" runat="server" OnClick="lnkb_reload_in_Click">0</asp:LinkButton>
                                     
                                    </td>
                                    <td  class="auto-style2">
                                        
                                        <asp:LinkButton ID="lnkb_reload_out" runat="server" OnClick="lnkb_reload_out_Click">0</asp:LinkButton>
                                            </td>
                                        
                                </tr>
                                                </tbody>
                                            </table>
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
