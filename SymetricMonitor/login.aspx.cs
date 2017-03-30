using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

namespace SymetricMonitor
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var username = WebConfigurationManager.AppSettings["admin_username"].ToString();
                var pass = WebConfigurationManager.AppSettings["admin_password"].ToString();
                var user= WebConfigurationManager.AppSettings["user_username"].ToString();
                var passwd = WebConfigurationManager.AppSettings["user_password"].ToString();

                if ((txtUsername.Text == username && txtPassword.Text == pass) || (txtUsername.Text == user && txtPassword.Text == passwd))
                {
                    Session["Auth"] = true;
                    Session["UserName"] = txtUsername.Text;
                    Response.Redirect("dashboard.aspx");
                }

                else {
                    lblmsg.Visible = true;
                    lblmsg.Text = "   Username ose Pasword i gabuar     .Provo Perseri!";
                }

            }
        }
    }
}