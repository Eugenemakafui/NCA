using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace NCA
{
    public partial class frmfulldb : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=.; Initial Catalog = NCA @ Reception; Integrated Security = True");
        public frmfulldb()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbldate.Text = DateTime.Now.ToLongDateString();
            lbltime1.Text = DateTime.Now.ToLongTimeString();
            countvisitor();
        }

        private void frmfulldb_Load(object sender, EventArgs e)
        {
            //btndatabaseBackup.Enabled = false;
        }

        private void countvisitor()
        {
            conn.Open();
            string sql = "Select Count(*) from tbl_visitor";
            SqlCommand command = new SqlCommand(sql, conn);
            var count = command.ExecuteScalar();
            lbltotal.Text=count.ToString();
            conn.Close();
        }

        private void countvisitor2()
        {
            conn.Open();
            string sql = "Select Count(*) from tbl_visitor where Date between '"+dtpfrom.Text+"' and '"+dtpto.Text+"'";
            SqlCommand command = new SqlCommand(sql, conn);
            var count = command.ExecuteScalar();
            guna2HtmlLabel1.Text=count.ToString();
            conn.Close();
        }
        private void btntdb_Click(object sender, EventArgs e)
        {
            fillDGVvisitor();

        }

        public void fillDGVvisitor()
        {
            string sql = "Select * from tbl_visitor";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvtotal.DataSource = dt;

        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            fillDGVvisitor1();
            countvisitor2();
        }

        public void fillDGVvisitor1()
        {
            string sql = "Select * from tbl_visitor where Date between '"+dtpfrom.Text+"' and '"+dtpto.Text+"'";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvtotal.DataSource = dt;

        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (txtsearch.Text =="")
            {

            }
            else
            {
                string sql = "Select * from tbl_visitor where Firstname='"+txtsearch.Text+"'";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvtotal.DataSource = dt;
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            fillDGVsearch();
            txtsearch.Clear();
        }

        public void fillDGVsearch()
        {
            conn.Open();
            string sql = "Select * from tbl_visitor where Firstname='"+txtsearch.Text+"'";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvtotal.DataSource = dt;
            conn.Close();

        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            fillDGVvisitor();
        }

        private void btndatabaseBackup_Click(object sender, EventArgs e)
        {
            BackupDatabase();
        }

        public void BackupDatabase()
        {
            try
            {
                conn.Open();
                string database = conn.Database.ToString();
                string backup = "Backup Database ["+database+"] to disk='Documents/NCA.bak'";
                SqlCommand backupdata = new SqlCommand(backup, conn);
                backupdata.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Backup Successful");
            }
            
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
 
        }

    }
}
    
