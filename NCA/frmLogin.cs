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
using Squirrel;

namespace NCA
{
    public partial class frmLogin : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=.; Initial Catalog = NCA @ Reception; Integrated Security = True");
        public frmLogin()
        {
            InitializeComponent();
            CheckForUpdates();
        }

        private async Task CheckForUpdates()
        {
            using (var manager = new UpdateManager(@"C:\Temp\Releases"))
            {
                await manager.UpdateApp();

            }
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            if (txtpassword.Text == "")
            {
                MessageBox.Show("Enter Password");
            }

            else
            {
                try
                {
                    string sql = "Select * from tbl_login where username='"+ txtusername.Text +"' and password='"+ txtpassword.Text +"'";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {
                        splash_screen ss = new splash_screen();
                        ss.Show();
                        this.Hide();
                    }

                    else
                    {
                        MessageBox.Show("Invalid Password");
                        txtpassword.Clear();
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            DialogResult repos = MessageBox.Show("Are you sure you want to exit the application?", "Exiting.....", MessageBoxButtons.YesNo);
                if (repos == DialogResult.Yes)
            {
                Application.Exit();
            }
                    else
                {
    
                }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void lbldate_Click(object sender, EventArgs e)
        {

        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
