using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using Npgsql;
using System.IO;
using System.Net;
using System.Data;
namespace SymetricMonitor
{
    public partial class wizard : System.Web.UI.Page
    {
        string connserver = "";
        string connclient = "";
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strip = ""; string con = "";
                //path of config file server database
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
                //path of config file backup server database
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
                 Bind();
                populatedbserver();
                populatedbclient();
                showtablechk();
                showscript();
            }
        }

        private void Bind()
        {

            Switch.Items.Add("Server");
            Switch.Items.Add("Client");
          
        }

        private void showscript()
        {
            string con = (string)Session["cons"];
            string s1 = "";   
           
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            NpgsqlConnection connn = new NpgsqlConnection(con);
            try
            {
                connn.Open();
                string sql = "SELECT count(table_name)as n " +
         "FROM information_schema.tables " +
        "WHERE table_schema='public' " +
          "AND table_type='BASE TABLE' " +
         "and table_name not like '%sym%' " +
        " union all " +

       "select count(*)/2  as n from sym_trigger_router;";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, connn);
               

                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];

                connn.Close();
            }
            catch (NpgsqlException ae) { s1 = ae.ToString(); }
            if (s1 != "") { scriptchk.Checked = false; }
            else if (Convert.ToInt32(dt.Rows[0]["n"]) == Convert.ToInt32(dt.Rows[1]["n"]))
            {
                scriptchk.Checked = true;
            }
            else { scriptchk.Checked = false; }
        }

        private void showtablechk()
        {
            string con = (string)Session["cons"];
            string s1 = "";   
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            NpgsqlConnection connn = new NpgsqlConnection(con);
            try
            {
               
                connn.Open();
                //"select c_order.c_order_id as orderid ,c_order.ad_org_id  as org ,c_orderline.c_orderline_id as clorderid, c_orderline.ad_org_id  as orgl  FROM  c_order,  c_orderline WHERE  c_order.c_order_id = c_orderline.c_order_id limit 2;";
                string sql = "SELECT count(*) as nr " +
     " FROM information_schema.tables " +
    " WHERE table_schema='public' " +
      " AND table_type='BASE TABLE' " +
      "and table_name like '%sym%';";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, connn);

                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];

                connn.Close();
            }
            catch (NpgsqlException ae) { s1 = ae.ToString(); }
            if (s1 != "") { processsymchk.Checked = false; }
            else if (Convert.ToInt32(dt.Rows[0]["nr"].ToString()) == 41)
            {
                processsymchk.Checked = true;
            }
            else { processsymchk.Checked = false; }
           
            
        }
        //show properties of server database
        private void populatedbserver()
        {
            string propertiesfilename = @"..\..\sym-corp\symmetric-3.5.19\engines\corp-000.properties";
            string[] lines = System.IO.File.ReadAllLines(propertiesfilename);
            for (int i=0; i < lines.Length; i++)
            {
                if (i >= 24 && i <= 36)
                {

                    
                    if (lines[i].Substring(0, 1) != "#")
                    {
                        dbserver.Items.Add(lines[i]);
                        dbserver.SelectedValue = lines[i];
                        urlctxt.Text = lines[i + 15];
                        usertxt.Text = lines[54];
                        passtxt.Text = lines[57];
                        urlregtxt.Text = lines[59];
                       
                    }
                    else { dbserver.Items.Add(lines[i].Substring(1, lines[i].Length-1)); }
                }

            }
        
        }
        //show properties of server backup database
        private void populatedbclient()
        {
            string propertiesfilename = @"..\..\sym-store001\symmetric-3.5.19\engines\store-001.properties";
            string[] lines = System.IO.File.ReadAllLines(propertiesfilename);
            for (int i = 0; i < lines.Length; i++)
            {
                if (i >= 24 && i <= 36)
                {


                    if (lines[i].Substring(0, 1) != "#")
                    {
                        dbclient.Items.Add(lines[i]);
                        dbclient.SelectedValue = lines[i];
                        urlconcltxt.Text = lines[i + 15];
                       usercltxt.Text = lines[54];
                        passcltxt.Text= lines[57];
                        urlclregtxt.Text = lines[60];

                    }
                    else { dbclient.Items.Add(lines[i].Substring(1, lines[i].Length - 1)); }
                }

            }

        }

        protected void btnlogout_Click1(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        protected void dbserver_SelectedIndexChanged(object sender, EventArgs e)
        {

            string propertiesfilename = @"..\..\sym-corp\symmetric-3.5.19\engines\corp-000.properties";
                string[] lines = System.IO.File.ReadAllLines(propertiesfilename);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i >= 24 && i <= 36)
                    {
                        if (dbserver.SelectedItem.ToString() == lines[i].Substring(1, lines[i].Length - 1))
                        {
                            urlctxt.Text = lines[i + 15].Substring(1, lines[i+15].Length - 1);
                        }
                    }
                }
            }

    

        protected void Wizard1_NextButtonClick1(object sender, WizardNavigationEventArgs e)
        {
            if (e.CurrentStepIndex == 0)
            {

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
                            l[i] = "#" + l[i];
                            l[i+15] = "#" + l[i+15];
                        }
                        if (dbserver.SelectedValue == l[i].Substring(1, l[i].Length - 1)) { l[i] = dbserver.SelectedValue; l[i + 15] = urlctxt.Text; l[54] = usertxt.Text; l[57] = passtxt.Text; l[59] = urlregtxt.Text; l[60] = "#" + l[60]; }
                    }
                   
                }

                    System.IO.File.WriteAllLines(propertiesfilename, l.ToArray()); string con = (string)Session["cons"];
                    string[] s = urlctxt.Text.Split('/');
                string[] st = s[2].Split(':');  
                if(con!=st[0]){
                    Session["cons"] = st[0];
                }

            }
            else if (e.CurrentStepIndex == 1) {

                string propertiesfilename = @"..\..\sym-store001\symmetric-3.5.19\engines\store-001.properties";

                string[] full_file = System.IO.File.ReadAllLines(propertiesfilename);

                List<string> l = new List<string>();

                l.AddRange(full_file);
                for (int i = 0; i < l.Count(); i++)
                {
                    if (i >= 24 && i <= 36)
                    {
                        if (l[i].Substring(0, 1) != "#")
                        {
                            l[i] = "#" + l[i];
                            l[i + 15] = "#" + l[i + 15];
                        }
                        if (dbclient.SelectedValue == l[i].Substring(1, l[i].Length - 1)) { l[i] = dbclient.SelectedValue; l[i + 15] = urlconcltxt.Text; l[54] = usercltxt.Text; l[57] = passcltxt.Text; l[60] = urlclregtxt.Text;  }
                    }

                }

                System.IO.File.WriteAllLines(propertiesfilename, l.ToArray());
                string con = (string)Session["cons"];
                string[] s = urlconcltxt.Text.Split('/');
                string[] st = s[2].Split(':');
                if (con != st[0])
                {
                    Session["cons"] = st[0];
                }

            }
        }

        protected void dbclient_SelectedIndexChanged(object sender, EventArgs e)
        {
            string propertiesfilename = @"..\..\sym-store001\symmetric-3.5.19\engines\store-001.properties";
            string[] lines = System.IO.File.ReadAllLines(propertiesfilename);
            for (int i = 0; i < lines.Length; i++)
            {
                if (i >= 24 && i <= 36)
                {
                    if (dbclient.SelectedItem.ToString() == lines[i].Substring(1, lines[i].Length - 1))
                    {
                        urlconcltxt.Text = lines[i + 15].Substring(1, lines[i + 15].Length - 1);
                    }
                }
            }
        }

        protected void procesbtn_Click(object sender, EventArgs e)
        {
            
            
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            startInfo.WorkingDirectory = @"..\..\sym-corp\symmetric-3.5.19\samples";
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/C ..\bin\symadmin --engine corp-000 create-sym-tables";

            startInfo.UseShellExecute = true;
            startInfo.CreateNoWindow = false;
            process.StartInfo = startInfo;
            process.Start();
            procesbtn.Text = "Processing";

           
        }

        protected void scriptbtn_Click(object sender, EventArgs e)
        {
            string con = (string)Session["cons"];

            NpgsqlConnection _connPg = new NpgsqlConnection(con);

            FileInfo file = new FileInfo(@"..\..\sql_script_symmetric.txt");
            string script = file.OpenText().ReadToEnd();
            var m_createdb_cmd = new NpgsqlCommand(script, _connPg);
            try
            {
                _connPg.Open();

                m_createdb_cmd.ExecuteNonQuery();
                _connPg.Close();
                scriptbtn.Text = "executed";
            }
            catch (NpgsqlException ae) { scriptbtn.Text = ae.ToString(); }

        }

        protected void StartServerbtn_Click(object sender, EventArgs e)
        {
            Process process1 = new Process();
            ProcessStartInfo startInfo1 = new ProcessStartInfo();
            startInfo1.WindowStyle = ProcessWindowStyle.Maximized;
            startInfo1.WorkingDirectory = @"..\..\sym-corp\symmetric-3.5.19\samples";
            startInfo1.FileName = "cmd.exe";
            
            startInfo1.Arguments = @"/C ..\bin\sym --engine corp-000 --port 31415";
            startInfo1.UseShellExecute = true;
            startInfo1.CreateNoWindow = false;
            process1.StartInfo = startInfo1;
           
            process1.Start();
                
                StartServerbtn.Text="Stop Server";
               
           
        }

        protected void StartClientbtn_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            startInfo.WorkingDirectory = @"..\..\sym-store001\symmetric-3.5.19\samples";
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/C ..\bin\sym --engine store-001 --port 9090";

            startInfo.UseShellExecute = true;
            startInfo.CreateNoWindow = false;
            process.StartInfo = startInfo;
            process.Start();
           
            StartClientbtn.Text= "Stop Client";

        }

        protected void registatenodebtn_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            startInfo.WorkingDirectory = @"..\..\sym-corp\symmetric-3.5.19\samples";
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = @"/C ..\bin\symadmin --engine corp-000 open-registration store 001";

            startInfo.UseShellExecute = true;
            startInfo.CreateNoWindow = false;
            process.StartInfo = startInfo;
            process.Start();

           registatenodebtn.Text = "Registred";
        }

        protected void reloadchk_CheckedChanged(object sender, EventArgs e)
        {
           
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Maximized;
                startInfo.WorkingDirectory = @"..\..\sym-store001\symmetric-3.5.19\samples";
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = @"/C ..\bin\symadmin --engine corp-000 reload-node 001";

                startInfo.UseShellExecute = true;
                startInfo.CreateNoWindow = false;
                process.StartInfo = startInfo;
                process.Start();
           
        }

        protected void updatedbbtn_Click(object sender, EventArgs e)
        { // Server
            msglbl.Visible = true;
            DataTable TarGetTable;
            TarGetTable = AddDataToTable();
            String TargetStringFilePath;
            StreamWriter fw=null;
            TargetStringFilePath = @"c:\aaa.txt";
            if (!File.Exists(TargetStringFilePath) )
            { fw = File.CreateText(TargetStringFilePath);
            foreach (DataRow dr in TarGetTable.Rows)
            {
                String str = dr["query"].ToString();

                fw.WriteLine(str);
            }


            fw.Flush();

            fw.Close();
            }
            else if (File.ReadAllLines(TargetStringFilePath).Length == 0)
            {
                foreach (DataRow dr in TarGetTable.Rows)
                {
                    String str = dr["query"].ToString();

                    fw.WriteLine(str);
                }


                fw.Flush();

                fw.Close();
            }

           
            //Client
            DataTable tar;
            tar = AddDataToTableclient();
            String TargetStringFilePathclient;
            StreamWriter fw2 = null ;
            TargetStringFilePathclient = @"c:\bbbb.txt";
            if (!File.Exists(TargetStringFilePathclient))
            {
                fw2 = File.CreateText(TargetStringFilePathclient);
                foreach (DataRow dr in tar.Rows)
                {
                    String str = dr["query"].ToString();

                    fw2.WriteLine(str);
                }


                fw2.Flush();

                fw2.Close();
            }
            else if (File.ReadAllLines(TargetStringFilePathclient).Length == 0)
            {
                foreach (DataRow dr in tar.Rows)
                {
                    String str = dr["query"].ToString();

                    fw2.WriteLine(str);
                }


                fw2.Flush();

                fw2.Close();
            }

            string con = (string)Session["cons"];
            NpgsqlConnection _connPg = new NpgsqlConnection(con);

            FileInfo file = new FileInfo(@"..\..\aaa.txt");
            string script = file.OpenText().ReadToEnd();
            var m_createdb_cmd = new NpgsqlCommand(script, _connPg);
            try
            {
                _connPg.Open();

                m_createdb_cmd.ExecuteNonQuery();
                _connPg.Close();
                msglbl.Text="Ndiq hapat e wizard";
            }
            catch (NpgsqlException ae) { msglbl.Text = ae.ToString(); }

            string conn = (string)Session["conc"];
            NpgsqlConnection _connPg1 = new NpgsqlConnection(conn);

            FileInfo file1 = new FileInfo(@"..\..\bbbb.txt");
            string script1 = file.OpenText().ReadToEnd();
            var m_createdb_cmd1 = new NpgsqlCommand(script, _connPg1);
            try
            {
                _connPg1.Open();

                m_createdb_cmd1.ExecuteNonQuery();
                _connPg1.Close();
                msglbl.Text = "Ndiq hapat e wizard" + msglbl.Text;
            }
            catch (NpgsqlException ae) { msglbl.Text = ae.ToString() + msglbl.Text; }

        }
        //Insert data to database backup
        private DataTable AddDataToTableclient()
        {
            string con = (string)Session["conc"];
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            NpgsqlConnection connn = new NpgsqlConnection(con);
            try
            {
                connn.Open();
                string sql =
                   " select 'DROP TRIGGER IF EXISTS '||tgname||' ON '||tablename||';' as query from pg_trigger,pg_class,pg_tables where " +
                  " tgrelid=relfilenode and relname=tablename and schemaname='public' and tgname like '%sym%'  " +
                   "  union all " +
                   " SELECT 'DROP FUNCTION IF EXISTS '|| proname||'();' as query  " +
                   " FROM    pg_catalog.pg_namespace n " +
                   " JOIN    pg_catalog.pg_proc p " +
                   " ON      pronamespace = n.oid  " +
                   " WHERE   nspname = 'public' and proname like '%fsym%' " +
                   "  union all " +
                   "select   'DROP TABLE IF EXISTS '||tablename ||' CASCADE ;' as query from pg_tables where tablename like '%sym%' " +
                   "  union all " +
                   " SELECT 'DROP SEQUENCE IF EXISTS ' || quote_ident(c.relname) || ';' as query " +
                   " FROM   pg_class c  " +
                   " LEFT   JOIN pg_depend d ON d.refobjid = c.oid AND d.deptype <> 'i'  " +
                   " WHERE  c.relkind = 'S'  " +
                    "AND    c.relname like '%sym%'; ";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, connn);

                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                connn.Close();
            }catch(Exception ex)
            {

            }
            return dt;
           
        }
        //Insert data to database 
        private DataTable AddDataToTable()
        {
            string con = (string)Session["cons"];
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            NpgsqlConnection connn = new NpgsqlConnection(con);
            try { 
            connn.Open();
            string sql =
              " select 'DROP TRIGGER IF EXISTS '||tgname||' ON '||tablename||';' as query from pg_trigger,pg_class,pg_tables where " +
             " tgrelid=relfilenode and relname=tablename and schemaname='public' and tgname like '%sym%'  " +
              "  union all " +
              " SELECT 'DROP FUNCTION IF EXISTS '|| proname||'();' as query  " +
              " FROM    pg_catalog.pg_namespace n " +
              " JOIN    pg_catalog.pg_proc p " +
              " ON      pronamespace = n.oid  " +
              " WHERE   nspname = 'public' and proname like '%fsym%' " +
              "  union all " +
              "select   'DROP TABLE IF EXISTS '||tablename ||' CASCADE ;' as query from pg_tables where tablename like '%sym%' " +
              "  union all " +
              " SELECT 'DROP SEQUENCE IF EXISTS ' || quote_ident(c.relname) || ';' as query " +
              " FROM   pg_class c  " +
              " LEFT   JOIN pg_depend d ON d.refobjid = c.oid AND d.deptype <> 'i'  " +
              " WHERE  c.relkind = 'S'  " +
               "AND    c.relname like '%sym%'; ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, connn);

            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];
            connn.Close();
            }catch(Exception ex) { }
            return dt;
           
        }

        protected void Switch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Switch.SelectedValue=="Server") {
                string cons = (string)Session["cons"];
                string con = (string)Session["conc"];
               string vep=update(cons)+update2(con);
            }
            else if (Switch.SelectedValue == "Client")
            {
                string cons = (string)Session["cons"];
                string con = (string)Session["conc"];
           string vep= update(con)+update2(cons);
            }

        }

        private string update2(string con)
        {
            string s = "";
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(con);
                conn.Open();
                string sql = "update pg_trigger "+
                            " set tgenabled='O' "+
                            " from pg_class,pg_tables "+
                            " where tgrelid=relfilenode and relname=tablename and schemaname='public'"+
                            "and tgrelid=relfilenode and relname=tablename and schemaname='public' "+
                            "and tgname not like '%RI%'and tablename <>'ad_session_usage_audit';";


                NpgsqlCommand command = new NpgsqlCommand(sql, conn);
                Int32 rowsaffected = command.ExecuteNonQuery();

                conn.Close();
                s = "success";
            }
            catch (NpgsqlException e)
            {
                s = e.ToString();

            }

            return s;
        }

        private String update(string conn)
        {
            string s = "";
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(conn);
                con.Open();
                string sql = "update pg_trigger "+
                                "set tgenabled='D' "+
                                "where tgname not like '%RI%'";


                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                Int32 rowsaffected = command.ExecuteNonQuery();

                con.Close();
                s = "success";
            }
            catch (NpgsqlException e)
            {
                s = e.ToString();

            }

            return s;
        }

        protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            Response.Redirect("wizard.aspx");
        }

       
      
    }
}