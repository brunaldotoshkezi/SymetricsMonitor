using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Npgsql;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Net;
using System.Web.Security;
using System.Security.Principal;

using System.Web.Configuration;
namespace SymetricMonitor
{
    public partial class dashboard : System.Web.UI.Page
    {
        string connserver = "";
        string connclient = "";
        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        WindowsImpersonationContext impersonationContext;

        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);



        //procceses in execution
        Process[] proceset = Process.GetProcessesByName("cmd");
        protected void Page_Load(object sender, EventArgs e)
        {
           
            
            if (!IsPostBack) {
                string strip = ""; string con = "";
                //read properties of server
                string propertiesfilename = @"..\..\sym-corp\symmetric-3.5.19\engines\corp-000.properties";

                string[] full_file = System.IO.File.ReadAllLines(propertiesfilename);

                List<string> l = new List<string>();

                l.AddRange(full_file);
                for (int i = 0; i < l.Count(); i++)
                {
                    if (i >= 24 && i <= 36)
                    {
                        if (l[i].Substring(0, 1) != "#")
                        {
                            con = l[i + 15];
                        }

                    }

                }
                string[] s = con.Split('/');
                string[] st = s[2].Split(':');
                if (st[0] == "localhost")
                {
                    string strHostName = System.Net.Dns.GetHostName();

                    IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

                    foreach (IPAddress ipAddress in ipEntry.AddressList)
                    {
                        if (ipAddress.AddressFamily.ToString() == "InterNetwork")
                        {
                            strip = ipAddress.ToString();
                        }
                    }


                }
                else
                {
                    strip = st[0];
                }
                //connection string for first database in postgresql
                connserver = "Server=" + strip + ";" +
                       "Database=openbravo;" +
                       "User ID=tad;" +
                       "Password=tad;";

                Session["cons"] = connserver;
                string con2 = "";
                string strip2 = "";
                //read properties of backup server
                string propertiesfilename2 = @"..\..\sym-store001\symmetric-3.5.19\engines\store-001.properties";

                string[] full_file2 = System.IO.File.ReadAllLines(propertiesfilename2);

                List<string> l2 = new List<string>();

                l2.AddRange(full_file2);
                for (int i = 0; i < l2.Count(); i++)
                {
                    if (i >= 24 && i <= 36)
                    {
                        if (l2[i].Substring(0, 1) != "#")
                        {
                            con2 = l2[i + 15];
                        }

                    }

                }
                string[] s2 = con2.Split('/');
                string[] st2 = s2[2].Split(':');

                strip2 = st2[0];
                //connection string for first backup database in postgresql
                connclient = "Server=" + strip2 + ";" +
                               "Database=openbravo;" +
                               "User ID=tad;" +
                               "Password=tad;";
                Session["conc"] = connclient;
                if (Session["UserName"].Equals(WebConfigurationManager.AppSettings["user_username"].ToString())) { lnkconfig.Visible = false; }
                Bind();
          }
          
            
            if (Session["Auth"] == null)
            {
                Response.Redirect("login.aspx");
            }
            if (proceset.Length>1)
            { btnstartclient.Text = "Stop Client";
            btnstartserver.Text = "Stop Server";
            lblServer.Text = " Online";
            lblServer.Attributes["style"] = "color:green; font-weight:bold;";
            lblClient.Text = " Online";
            lblClient.Attributes["style"] = "color:green; font-weight:bold;";
            }
             if (Exist()){DataTable dt = populate();
           
                string[] columnNames = dt.Columns.Cast<DataColumn>()
                                             .Select(x => x.ColumnName)
                                             .ToArray();
                lnkb_error_in.Text = dt.Rows[0][columnNames[0]].ToString();
                lnkb_error_out.Text = dt.Rows[1][columnNames[0]].ToString();
                lnkb_load_in.Text = dt.Rows[0][columnNames[3]].ToString();
                lnkb_load_out.Text = dt.Rows[1][columnNames[3]].ToString();
                lnkb_new_in.Text = dt.Rows[0][columnNames[2]].ToString();
                lnkb_new_out.Text = dt.Rows[1][columnNames[2]].ToString();
                lnkb_ok_in.Text = dt.Rows[0][columnNames[1]].ToString();
                lnkb_ok_out.Text = dt.Rows[1][columnNames[1]].ToString();
                lnkb_insert_in.Text = dt.Rows[0][columnNames[4]].ToString();
                lnkb_insert_out.Text = dt.Rows[1][columnNames[4]].ToString();
                lnkb_update_in.Text = dt.Rows[0][columnNames[5]].ToString();
                lnkb_update_out.Text = dt.Rows[1][columnNames[5]].ToString();
                lnkb_delete_in.Text = dt.Rows[0][columnNames[6]].ToString();
                lnkb_delete_out.Text = dt.Rows[1][columnNames[6]].ToString();
                lnkb_reload_in.Text = dt.Rows[0][columnNames[7]].ToString();
                lnkb_reload_out.Text = dt.Rows[1][columnNames[7]].ToString();
            }
            else
            {
                lnkb_error_in.Text = "0";
                lnkb_error_out.Text = "0";
                lnkb_load_in.Text = "0";
                lnkb_load_out.Text = "0";
                lnkb_new_in.Text = "0";
                lnkb_new_out.Text = "0";
                lnkb_ok_in.Text = "0";
                lnkb_ok_out.Text = "0";
                lnkb_insert_in.Text = "0";
                lnkb_insert_out.Text = "0";
                lnkb_update_in.Text = "0";
                lnkb_update_out.Text = "0";
                lnkb_delete_in.Text = "0";
                lnkb_delete_out.Text = "0";
                lnkb_reload_in.Text = "0";
                lnkb_reload_out.Text = "0";

            }
            
        }

        private void Bind()
        {
            Switch.Items.Add("Server");
            Switch.Items.Add("Client");
        }
        private bool Exist()
        {
            bool b;
            string conn = "";
            if (Switch.SelectedValue == "Server")
            {
                string connn = (string)Session["cons"];
                conn = connn;
            }
            else
            {
                string connn = (string)Session["conc"];
                conn = connn;
            }
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            NpgsqlConnection con = new NpgsqlConnection(conn);
            con.Open();
            string sql = "select count(tablename )nr"+
                         " from pg_tables where tablename like '%sym%' ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);

            ds.Reset();
           
                da.Fill(ds);
                dt = ds.Tables[0];
          
            con.Close();
            if (Convert.ToInt32(dt.Rows[0]["nr"])>0) { b = true; }else
            { b = false; }
            return b; 
        }

        private DataTable populate()
        {
            string conn = "";
            if(Switch.SelectedValue=="Server"){
               string connn = (string)Session["cons"];
               conn = connn;
            }
            else
            {
                string connn = (string)Session["conc"];
                conn = connn; 
            }
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            NpgsqlConnection con = new NpgsqlConnection(conn);
            con.Open();
            //"select c_order.c_order_id as orderid ,c_order.ad_org_id  as org ,c_orderline.c_orderline_id as clorderid, c_orderline.ad_org_id  as orgl  FROM  c_order,  c_orderline WHERE  c_order.c_order_id = c_orderline.c_order_id limit 2;";
            string sql = "  SELECT " +
                 "(SELECT  COUNT(*) ernr "+
                "FROM sym_incoming_batch, sym_incoming_error "+
                "WHERE"+ 
                  " sym_incoming_batch.batch_id = sym_incoming_error.batch_id and sym_incoming_batch.status='ER') as er,"+
                "(SELECT "+
                 " COUNT(*)oknr "+
                "FROM  "+
                   "public.sym_incoming_batch,"+ 
                 " public.sym_incoming_error "+
                "WHERE "+ 
                  "sym_incoming_batch.batch_id = sym_incoming_error.batch_id and sym_incoming_batch.status='OK')as ok,"+
                "(SELECT "+
                  "COUNT(*)newnr "+
                " FROM "+ 
                  "public.sym_incoming_batch,"+ 
                 " public.sym_incoming_error "+
                "WHERE "+
                  "sym_incoming_batch.batch_id = sym_incoming_error.batch_id and sym_incoming_batch.status='NE')as new,"+
                "(SELECT "+
                 " COUNT(*) ldnr "+
                "FROM "+
                 " public.sym_incoming_batch, "+
                  "public.sym_incoming_error "+
                "WHERE "+
                  "sym_incoming_batch.batch_id = sym_incoming_error.batch_id and sym_incoming_batch.status='LD')as ld,"+
                "(SELECT "+
                 " COUNT(*) insertnr "+
                "FROM "+
                  "public.sym_incoming_batch,"+ 
                 " public.sym_incoming_error "+
                "WHERE " +
                  "sym_incoming_batch.batch_id = sym_incoming_error.batch_id and sym_incoming_error.event_type='I' ) as insert,"+

                "(SELECT "+
                  "COUNT(*)uptnr "+
                "FROM "+
                  "public.sym_incoming_batch, "+
                  "public.sym_incoming_error "+
                "WHERE "+
                  "sym_incoming_batch.batch_id = sym_incoming_error.batch_id and sym_incoming_error.event_type='U' ) as update,"+
                "(SELECT "+
                  "COUNT(*) deletenr "+
                "FROM "+
                 " public.sym_incoming_batch,"+ 
                 " public.sym_incoming_error "+
                "WHERE "+
                  "sym_incoming_batch.batch_id = sym_incoming_error.batch_id and sym_incoming_error.event_type='D' ) as delete,"+
                 " (SELECT "+
                 " COUNT(*) reloadnr "+
                "FROM "+
                 " public.sym_incoming_batch,"+ 
                  "public.sym_incoming_error "+
                "WHERE "+
                "  sym_incoming_batch.batch_id = sym_incoming_error.batch_id and sym_incoming_error.event_type='R' ) as reload"+
                 " Union all"+
                 " SELECT"+
                " (SELECT "+
                   "COUNT(*) ernr "+
                "FROM "+  
                 " public.sym_data, "+
                 " public.sym_data_event, "+
                "  public.sym_outgoing_batch "+
                "WHERE "+
                 " sym_data.data_id = sym_data_event.data_id AND "+
                  "sym_data_event.batch_id = sym_outgoing_batch.batch_id  AND  sym_outgoing_batch.status='ER')as er,"+
                " (SELECT"+ 
                   " COUNT(*) oknr "+
                    " FROM  "+
                  "public.sym_data,"+ 
                  "public.sym_data_event,"+ 
                  "public.sym_outgoing_batch"+
                " WHERE " +
                 " sym_data.data_id = sym_data_event.data_id AND "+
                 " sym_data_event.batch_id = sym_outgoing_batch.batch_id  AND  sym_outgoing_batch.status='OK')as ok,"+
                " (SELECT " +
                   "COUNT(*) newnr "+
                "FROM "+
                 " public.sym_data,"+ 
                  "public.sym_data_event,"+ 
                 " public.sym_outgoing_batch"+
                " WHERE "+
                 " sym_data.data_id = sym_data_event.data_id AND "+
                 " sym_data_event.batch_id = sym_outgoing_batch.batch_id  AND  sym_outgoing_batch.status='NE')as new,"+
                "(SELECT "+
                   "COUNT(*) ldnr "+
                "FROM "+
                "  public.sym_data, "+
                  "public.sym_data_event,"+ 
                 " public.sym_outgoing_batch "+
                "WHERE "+
                  "sym_data.data_id = sym_data_event.data_id AND "+
                 " sym_data_event.batch_id = sym_outgoing_batch.batch_id  AND  sym_outgoing_batch.status='LD')as ld,"+
                "(SELECT "+
                   "COUNT(*) insertnr "+
                "FROM "+
                 " public.sym_data," +
                 " public.sym_data_event,"+ 
                 " public.sym_outgoing_batch"+
                 " WHERE "+
                 " sym_data.data_id = sym_data_event.data_id AND "+
                 " sym_data_event.batch_id = sym_outgoing_batch.batch_id  AND  sym_data.event_type='I')as insert,"+
                "(SELECT "+
                 "  COUNT(*) upnr "+
                "FROM "+
                 " public.sym_data, "+
                 " public.sym_data_event, "+
                  "public.sym_outgoing_batch "+
                "WHERE "+
                "  sym_data.data_id = sym_data_event.data_id AND "+
                 " sym_data_event.batch_id = sym_outgoing_batch.batch_id  AND  sym_data.event_type='U')as update,"+
                "(SELECT "+
                  " COUNT(*) deletenr "+
                "FROM "+
                  "public.sym_data,"+ 
                 " public.sym_data_event, "+
                 " public.sym_outgoing_batch "+
                "WHERE "+ 
                  "sym_data.data_id = sym_data_event.data_id AND "+
                 " sym_data_event.batch_id = sym_outgoing_batch.batch_id  AND  sym_data.event_type='D')as delete,"+
                "(SELECT "+
                  " COUNT(*) reloadnr "+
                "FROM "+
                  "public.sym_data,"+ 
                  "public.sym_data_event,"+ 
                  "public.sym_outgoing_batch "+
                "WHERE "+
                 " sym_data.data_id = sym_data_event.data_id AND "+
                 " sym_data_event.batch_id = sym_outgoing_batch.batch_id  AND  sym_data.event_type='R')as reload;";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);

            ds.Reset();
            if (da != null)
            {
                da.Fill(ds);
                dt = ds.Tables[0];
            }
            con.Close();
            return dt;
        }

       

     

     

        protected void btnstartclient_Click(object sender, EventArgs e)
        {

            if (impersonateValidUser("Administrator", "", "Admin123"))
            {

                undoImpersonation();
            }

            

				
            if (proceset.Length>1)
            {
                proceset[0].Kill();
                proceset[1].Kill();
                btnstartclient.Text = "Start Client";
                btnstartclient.Text = "Start Server";
                lblClient.Text = " Offline";
                lblClient.Attributes["style"] = "color:red; font-weight:bold;";
            }
            else
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Maximized;
               // startInfo.WorkingDirectory = @"..\..\sym-store001\symmetric-3.5.19\samples";
                startInfo.FileName = "cmd.exe";
               // startInfo.Arguments = @"/C ..\bin\sym --engine store-001 --port 9090";
     
                startInfo.UseShellExecute = true;
                startInfo.CreateNoWindow = false;
                process.StartInfo = startInfo;
                process.Start();
                
                btnstartclient.Text="Stop Client";
                lblClient.Text = " Online";
                lblClient.Attributes["style"] = "color:green; font-weight:bold;";
            }
        }

        private bool impersonateValidUser(string userName, string domain, string password)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }
            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
            return false;
        }

        private void undoImpersonation()
        {
            impersonationContext.Undo();
        }

        protected void btnstartserver_Click(object sender, EventArgs e)
        {
            
            if (proceset.Length>1)
            {
                proceset[0].Kill();
                proceset[1].Kill();
                btnstartclient.Text = "Start Client";
                btnstartclient.Text = "Start Server";
                lblServer.Text = " Offline";
                lblServer.Attributes["style"] = "color:red; font-weight:bold;";
            }
            else
            {
                 Process process1 = new Process();
                 ProcessStartInfo startInfo1 = new ProcessStartInfo();
                 startInfo1.WindowStyle = ProcessWindowStyle.Maximized;
                 startInfo1.WorkingDirectory = @"..\..\sym-corp\symmetric-3.5.19\samples";
                 startInfo1.FileName ="cmd.exe";
                 startInfo1.UseShellExecute = true;
                 startInfo1.Arguments = @"/C ..\bin\sym--engine corp-000 --port 31418";
                 process1.StartInfo = startInfo1;
                 process1.Start();
                 btnstartclient.Text = "Stop Server";
                 lblServer.Text = " Online";
                 lblServer.Attributes["style"] = "color:green; font-weight:bold;";
             }
        }
        

     

        protected void btnlogout_Click1(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        protected void lnkb_error_in_Click(object sender, EventArgs e)
        {
            Response.Redirect("logs.aspx?type=ER,in,"+Switch.SelectedValue);
        }

        protected void lnkb_load_in_Click(object sender, EventArgs e)
        {
            Response.Redirect("logs.aspx?type=LD,in," + Switch.SelectedValue);
        }

        protected void lnkb_new_in_Click(object sender, EventArgs e)
        {
            Response.Redirect("logs.aspx?type=NE,in," + Switch.SelectedValue);
        }

        protected void lnkb_ok_in_Click(object sender, EventArgs e)
        {
            Response.Redirect("logs.aspx?type=OK,in," + Switch.SelectedValue);
        }

        protected void lnkb_error_out_Click(object sender, EventArgs e)
        {
            Response.Redirect("logs.aspx?type=ER,out," + Switch.SelectedValue);
        }

        protected void lnkb_load_out_Click(object sender, EventArgs e)
        {
            Response.Redirect("logs.aspx?type=LD,out," + Switch.SelectedValue);
        }

        protected void lnkb_new_out_Click(object sender, EventArgs e)
        {
            Response.Redirect("logs.aspx?type=NE,out," + Switch.SelectedValue);
        }

        protected void lnkb_ok_out_Click(object sender, EventArgs e)
        {
            Response.Redirect("logs.aspx?type=OK,out," + Switch.SelectedValue);
        }

        protected void lnkb_insert_in_Click(object sender, EventArgs e)
        {
            Response.Redirect("logs.aspx?type=I,in," + Switch.SelectedValue);
        }

        protected void lnkb_update_in_Click(object sender, EventArgs e)
        {
            Response.Redirect("logs.aspx?type=U,in," + Switch.SelectedValue);
        }

        protected void lnkb_delete_in_Click(object sender, EventArgs e)
        {
            Response.Redirect("logs.aspx?type=D,in," + Switch.SelectedValue);
        }

        protected void lnkb_reload_in_Click(object sender, EventArgs e)
        {
            Response.Redirect("logs.aspx?type=R,in," + Switch.SelectedValue);
        }

        protected void lnkb_insert_out_Click(object sender, EventArgs e)
        {
            Response.Redirect("logs.aspx?type=I,out," + Switch.SelectedValue);
        }

        protected void lnkb_update_out_Click(object sender, EventArgs e)
        {
            Response.Redirect("logs.aspx?type=U,out," + Switch.SelectedValue);
        }

        protected void lnkb_delete_out_Click(object sender, EventArgs e)
        {
            Response.Redirect("logs.aspx?type=D,out," + Switch.SelectedValue);
        }

        protected void lnkb_reload_out_Click(object sender, EventArgs e)
        {
            Response.Redirect("logs.aspx?type=R,out," + Switch.SelectedValue);
        }

        protected void lnkconfig_Click(object sender, EventArgs e)
        {
            Response.Redirect("wizard.aspx");
        }

        

      
    }
}