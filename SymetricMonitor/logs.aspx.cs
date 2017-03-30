using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;
using System.Data;
using System.Net;
using System.Web.Configuration;
namespace SymetricMonitor
{
    public partial class logs : System.Web.UI.Page
    {
        string connserver = "";
        string connclient = "";
        NpgsqlConnection con = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string strip = ""; string con = "";
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
            connserver = "Server=" + strip + ";" +
                   "Database=openbravo;" +
                   "User ID=tad;" +
                   "Password=tad;";

            Session["cons"] = connserver;
            string con2 = "";
            string strip2 = "";
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

            connclient = "Server=" + strip2 + ";" +
                   "Database=openbravo;" +
                   "User ID=tad;" +
                   "Password=tad;";
            Session["conc"] = connclient;
            con = null;
            string tipi = "";
            string lloji = "";
            string user = "";
            string type = Request["type"];
            if (type != null)
            {
                string[] s1 = type.Split(',');
                tipi = s1[0];
                lloji = s1[1];
                user = s[2];

            }
            else
            {
                user = Switch.SelectedValue;
                if (Switch.SelectedValue == "") { user = "Server"; }
            }
           
            if (!IsPostBack)
            {
                if (Session["UserName"].Equals(WebConfigurationManager.AppSettings["user_username"].ToString())) { lnkconfig.Visible = false; }
                
                Bind();
              
            }
            showgrid(tipi, lloji, user);   
           
          
          
        }

        private void Bind()
        {

            Switch.Items.Add("Server");
            Switch.Items.Add("Client");
        }

        public void showgrid(string tipi,string lloji,string user)
        {
            string query = "";
            string conn="";
            if (user == "Server")
            {
                string con = (string)Session["cons"];
                conn = con;
            }
            else
            {string con = (string)Session["cons"];
                conn = con;
            
            }
            if (tipi != "" && lloji != "")
            {
                DataTable dt = new DataTable();
            DataSet ds = new DataSet();
             con = new NpgsqlConnection(conn);
            con.Open();
            if (lloji == "in")
            {
                query = " SELECT "+
                    "sym_incoming_batch.batch_id || ',in' as id," +
"sym_incoming_batch.channel_id,"+  
"sym_incoming_error.target_table_name as table_name, "+
  "sym_incoming_error.event_type,"+ 
 " sym_incoming_error.row_data,"+ 
"'store_2_corp'as router_id,"+
"sym_incoming_batch.status ,"+ 
"sym_incoming_batch.sql_message as error "+ 
" FROM "+
"sym_incoming_batch left join sym_incoming_error on sym_incoming_batch.batch_id = sym_incoming_error.batch_id where (sym_incoming_batch.status ='" + tipi + "' OR   sym_incoming_error.event_type ='" + tipi + "') Order by table_name ASC ";
            }
            else if (lloji == "out") { query = "  SELECT "+
                 " sym_outgoing_batch.batch_id || ',out' as id," +
  "sym_data.channel_id, "+
  "sym_data.table_name,"+ 
  "sym_data.event_type,"+ 
  "sym_data.row_data,"+ 
  "sym_data_event.router_id,"+ 
  "sym_outgoing_batch.status, "+
 " sym_outgoing_batch.sql_message as error "+  
" FROM "+
 " public.sym_data, "+
 " public.sym_data_event, "+
  "public.sym_outgoing_batch"+
" WHERE "+
 " sym_data.data_id = sym_data_event.data_id AND "+
 " sym_data_event.batch_id = sym_outgoing_batch.batch_id AND (  sym_outgoing_batch.status='"+tipi+"' OR sym_data.event_type ='"+tipi+"') Order by table_name ASC; "; }
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, con);

            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            con.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            }
            else
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                 con = new NpgsqlConnection(conn);
                con.Open();
                //"select c_order.c_order_id as orderid ,c_order.ad_org_id  as org ,c_orderline.c_orderline_id as clorderid, c_orderline.ad_org_id  as orgl  FROM  c_order,  c_orderline WHERE  c_order.c_order_id = c_orderline.c_order_id limit 2;";
                string sql = "  select * from (SELECT " +
                   " sym_outgoing_batch.batch_id || ',out' as id,"+
 " sym_data.channel_id, "+
"  sym_data.table_name, "+
  "sym_data.event_type,"+ 
  "sym_data.row_data,"+ 
  "sym_data_event.router_id,"+ 
  "sym_outgoing_batch.status,"+
  "sym_outgoing_batch.sql_message as error "+  
" FROM "+
  "public.sym_data, "+
  "public.sym_data_event, "+
  "public.sym_outgoing_batch"+
" WHERE "+
 " sym_data.data_id = sym_data_event.data_id AND "+
 " sym_data_event.batch_id = sym_outgoing_batch.batch_id "+
"union all "+

" SELECT "+
"sym_incoming_batch.batch_id || ',in' as id,"+
"sym_incoming_batch.channel_id, "+ 
"sym_incoming_error.target_table_name as table_name,"+ 
 " sym_incoming_error.event_type, "+
 " sym_incoming_error.row_data, "+
"'store_2_corp'as router_id,"+
"sym_incoming_batch.status, "+
"sym_incoming_batch.sql_message as error "+
  
" FROM "+
"sym_incoming_batch left join sym_incoming_error on sym_incoming_batch.batch_id = sym_incoming_error.batch_id )"+
"t order by t.table_name;  ";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);

                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                con.Close();
                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
        }

        protected void btnlogout_Click1(object sender, EventArgs e)
        {

        }

        protected void btnstartclient_Click(object sender, EventArgs e)
        {

        }

        protected void btnstartserver_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string tipi = "";
            string lloji = "";
            string user = "";
            string type = Request["type"];
            if (type != null)
            {
                string[] s = type.Split(',');
                tipi = s[0];
                lloji = s[1];
                user = s[2];

            }
            else
            {
                user = Switch.SelectedValue;
                if (Switch.SelectedValue == "") { user = "Server"; }
            }
           
            GridView1.PageIndex = e.NewPageIndex;
            showgrid(tipi,lloji,user);
        }

        protected void update(string id, string inout)
        {
            
            string tipi = "";
            string lloji = "";
            string user = "";
              string conn="";
            string type = Request["type"];
            if (type != null)
            {
                string[] s = type.Split(',');
                tipi = s[0];
                lloji = s[1];
                user = s[2];

            }
            else
            {
                user = Switch.SelectedValue;
                if (Switch.SelectedValue == "") { user = "Server"; }
            }
            
            if (user == "Server")
            {
                string con = (string)Session["cons"];
                conn = con;
           
            }
            else
            {
               string con = (string)Session["conc"];
                conn = con;
          
            }

             string s1 = "";
             if (inout == "out")
             {
                 try
                 {
                     NpgsqlConnection con = new NpgsqlConnection(conn);
                     con.Open();
                     string sql = " Update sym_outgoing_batch "+

"set status='LD' "+

"where  batch_id='"+id+"';";
                     NpgsqlCommand command = new NpgsqlCommand(sql, con);
                     Int32 rowsaffected = command.ExecuteNonQuery();

                     con.Close();
                     s1 = "success";
                 }
                 catch (NpgsqlException ae)
                 {
                     s1 = ae.ToString();

                 }

             }
             else
             {
                 try
                 {
                     NpgsqlConnection con = new NpgsqlConnection(conn);
                     con.Open();
                     string sql = "Update sym_incoming_batch "+

"set  status='LD' "+

"where   batch_id='" + id + "';"; 
                     NpgsqlCommand command = new NpgsqlCommand(sql, con);
                     Int32 rowsaffected = command.ExecuteNonQuery();

                     con.Close();
                     s1 = "success";
                 }
                 catch (NpgsqlException ae)
                 {
                     s1 = ae.ToString();

                 }

             }
       

     }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string tipi = "";
            string lloji = "";
            string user = "";
            string type = Request["type"];
            if (type != null)
            {
                string[] s = type.Split(',');
                tipi = s[0];
                lloji = s[1];
                user = s[2];

            }
            else
            {
                user = Switch.SelectedValue;
                if (Switch.SelectedValue == "") { user = "Server"; }
            }
            if (e.CommandName == "Update")
            {
                string s = e.CommandArgument.ToString();
                string[] st = s.Split(',');

                update(st[0], st[1]);

                showgrid(tipi, lloji, user);
            }
           
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {
            string tipi = "";
            string lloji = "";
            string user = "";
            string type = Request["type"];
            if (type != null)
            {
                string[] s = type.Split(',');
                tipi = s[0];
                lloji = s[1];
                user = s[2];

            }
            else
            {
                user = Switch.SelectedValue;
                if (Switch.SelectedValue == "") { user = "Server"; }
            }
            string query = "";
            string conn = "";
            if (user == "Server")
            {
              string con = (string)Session["cons"];
                conn = con;
            
            }
            else
            {
               string con = (string)Session["cons"];
                conn = con;
            
            }
            if (tipi != "" && lloji != "")
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                con = new NpgsqlConnection(conn);
                con.Open();
                if (lloji == "in")
                {
                    query = " SELECT " +
                        "sym_incoming_batch.batch_id || ',in' as id," +
    "sym_incoming_batch.channel_id," +
    "sym_incoming_error.target_table_name as table_name, " +
      "sym_incoming_error.event_type," +
     " sym_incoming_error.row_data," +
    "'store_2_corp'as router_id," +
    "sym_incoming_batch.status ," +
    "sym_incoming_batch.sql_message as error " +
    " FROM " +
    "sym_incoming_batch left join sym_incoming_error on sym_incoming_batch.batch_id = sym_incoming_error.batch_id where (sym_incoming_batch.status ='" + tipi + "' OR   sym_incoming_error.event_type ='" + tipi + "') and (sym_incoming_batch.channel_id like '%" + searchBox.Text + "%' or  sym_incoming_error.target_table_name like '%" + searchBox.Text + "%' or sym_incoming_error.event_type like '%" + searchBox.Text + "%' or sym_incoming_batch.status  like '%" + searchBox.Text + "%' ) Order by table_name ASC ";
                }
                else if (lloji == "out")
                {
                    query = "  SELECT " +
                        " sym_outgoing_batch.batch_id || ',out' as id," +
                        "sym_data.channel_id, " +
                        "sym_data.table_name," +
                        "sym_data.event_type," +
                        "sym_data.row_data," +
                        "sym_data_event.router_id," +
                        "sym_outgoing_batch.status, " +
                        " sym_outgoing_batch.sql_message as error " +
                        " FROM " +
                        " public.sym_data, " +
                        " public.sym_data_event, " +
                        "public.sym_outgoing_batch" +
                        " WHERE " +
                        " sym_data.data_id = sym_data_event.data_id AND " +
                        " sym_data_event.batch_id = sym_outgoing_batch.batch_id AND (  sym_outgoing_batch.status='" + tipi + "' OR sym_data.event_type ='" + tipi + "') and (sym_data.channel_id like '%" + searchBox.Text + "%' or sym_data.table_name like '%" + searchBox.Text + "%' or    sym_data.event_type like '%" + searchBox.Text + "%' or  sym_data_event.router_id like '%" + searchBox.Text + "%' or  sym_outgoing_batch.status like '%" + searchBox.Text + "%') Order by table_name ASC; ";
                }
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, con);

                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                con.Close();
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                con = new NpgsqlConnection(conn);
                con.Open();
                //"select c_order.c_order_id as orderid ,c_order.ad_org_id  as org ,c_orderline.c_orderline_id as clorderid, c_orderline.ad_org_id  as orgl  FROM  c_order,  c_orderline WHERE  c_order.c_order_id = c_orderline.c_order_id limit 2;";
                string sql = "  select * from (SELECT " +
                   " sym_outgoing_batch.batch_id || ',out' as id," +
 " sym_data.channel_id, " +
"  sym_data.table_name, " +
  "sym_data.event_type," +
  "sym_data.row_data," +
  "sym_data_event.router_id," +
  "sym_outgoing_batch.status," +
  "sym_outgoing_batch.sql_message as error " +
" FROM " +
  "public.sym_data, " +
  "public.sym_data_event, " +
  "public.sym_outgoing_batch" +
" WHERE " +
 " sym_data.data_id = sym_data_event.data_id AND " +
 " sym_data_event.batch_id = sym_outgoing_batch.batch_id  and (sym_data.channel_id like '%" + searchBox.Text + "%' or sym_data.table_name like '%he%' or    sym_data.event_type like '%" + searchBox.Text + "%' or  sym_data_event.router_id like '%" + searchBox.Text + "%'  or  sym_outgoing_batch.status like '%" + searchBox.Text + "%')" +
"union all " +

" SELECT " +
"sym_incoming_batch.batch_id || ',in' as id," +
"sym_incoming_batch.channel_id, " +
"sym_incoming_error.target_table_name as table_name," +
 " sym_incoming_error.event_type, " +
 " sym_incoming_error.row_data, " +
"'store_2_corp'as router_id," +
"sym_incoming_batch.status, " +
"sym_incoming_batch.sql_message as error " +

" FROM " +
"sym_incoming_batch left join sym_incoming_error on sym_incoming_batch.batch_id = sym_incoming_error.batch_id where (sym_incoming_batch.channel_id like '%" + searchBox.Text + "%' or  sym_incoming_error.target_table_name like '%" + searchBox.Text + "%' or sym_incoming_error.event_type like '%" + searchBox.Text + "%' or sym_incoming_batch.status  like '%" + searchBox.Text + "%' ))" +
"t order by t.table_name;  ";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);

                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                con.Close();
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

        }

        protected void lnkRefresh_Click(object sender, EventArgs e)
        {
            string tipi = "";
            string lloji = "";
            string user = "";
            string type = Request["type"];
            if (type != null)
            {
                string[] s = type.Split(',');
                tipi = s[0];
                lloji = s[1];
                user = s[2];

            }
            else
            {
                user = Switch.SelectedValue;
                if (Switch.SelectedValue == "") { user = "Server"; }
            }
            showgrid(tipi, lloji, user);
        }

        protected void lnkconfig_Click(object sender, EventArgs e)
        {
            Response.Redirect("wizard.aspx");
        }

       

  
    }
}