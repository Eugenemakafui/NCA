using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace NCA
{
    public partial class frmMain : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=.; Initial Catalog = NCA @ Reception; Integrated Security = True");
        public frmMain()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
            InitializeComponent();
            this.WindowState=FormWindowState.Maximized;

            
        }

        private void fillcombo_staffname()
        {
                string sql = "SELECT * from tbl_staff";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbxvisited.DataSource = dt;
                cbxvisited.DisplayMember = "Staff_name";
                cbxvisited.ValueMember = "Staff_id";
            }

        private const int clse = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams mycp = base.CreateParams;
                mycp.ClassStyle = mycp.ClassStyle | clse;
                return mycp;
            }
        }

        private void insertvisitor()
        {
            if (txtfirstname.Text == "" || txtlastname.Text == "" || txtcontact.Text == "" || txtlocation.Text == "" || cbxremark.Text == "" || cbxvisited.Text =="")
            {
                MessageBox.Show("Please fill the form");
                
            }

            else
            {
                try
                {
                    DateTime dt = DateTime.Now;
                    string n = dt.ToString("yyyy-MM-dd");
                    string x = dt.ToString("hh:mm:ss");
                    
                    conn.Open();
                    string sql = "Insert into tbl_visitor (Firstname,Lastname,Othername,Date,Time_In,Contact,Location,Remark,Person_Visited) VALUES(@FN,@LN,@OT,@DT,@TI,@C,@L,@R,@PV)";
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.Parameters.AddWithValue("@FN", txtfirstname.Text);
                    command.Parameters.AddWithValue("@LN", txtlastname.Text);
                    command.Parameters.AddWithValue("@OT", txtother.Text);
                    command.Parameters.AddWithValue("@DT", n);
                    command.Parameters.AddWithValue("@TI", x);
                    command.Parameters.AddWithValue("@C", txtcontact.Text);
                    command.Parameters.AddWithValue("@L", txtlocation.Text);
                    command.Parameters.AddWithValue("@R", cbxremark.Text);
                    command.Parameters.AddWithValue("@PV", cbxvisited.Text);
                    command.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Visitor Successfully Added");
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }



        private void frmMain_Load(object sender, EventArgs e)
        {
            fillcombo_staffname();
            fillDGVvisitor();
            guna2Button2.Enabled = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {

                insertvisitor();
                fillDGVvisitor();
                txtlocation.Clear();
                txtother.Clear();
                txtfirstname.Clear();
                txtlastname.Clear();
           
        }

        public void fillDGVvisitor()
        {
            string sql = "Select * from tbl_visitor where Time_Out is null";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            guna2DataGridView1.DataSource = dt;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbldate.Text = DateTime.Now.ToLongDateString();
            lbltime.Text = DateTime.Now.ToLongTimeString();
        }





        private void guna2Button2_Click(object sender, EventArgs e)
        {
                if (textBox1.Text==""|| textBox2.Text=="")
                {
                    MessageBox.Show("Please Select a visitor from the table");
                }

                else
                {
                    DialogResult repos = MessageBox.Show("Are you sure you want to sign out this visitor?", "Time out....", MessageBoxButtons.YesNo);
                    if (repos == DialogResult.Yes)
                    {
                        try
                        {
                            DateTime dt = DateTime.Now;
                            string x = dt.ToString("hh:mm:ss");
                            conn.Open();
                            string mysql = "Update tbl_visitor set Time_Out=@TO where Contact=@key2";
                            SqlCommand mycommand = new SqlCommand(mysql, conn);
                            mycommand.Parameters.AddWithValue("@TO", x);
                            mycommand.Parameters.AddWithValue("@key2", textBox2.Text);
                            mycommand.ExecuteNonQuery();
                            conn.Close();
                            fillDGVvisitor();
                            MessageBox.Show("Visitor Successfully Signed Out");
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    else
                    {

                    }
                }
                textBox1.Clear();
                textBox2.Clear();
                lblfirst.Text = "";
                guna2Button2.Enabled = false;
            }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2DataGridView1.CurrentRow.Selected = true;
            textBox1.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            lblfirst.Text = guna2DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            MessageBox.Show("Visitor Selected");
            guna2Button2.Enabled = true;
        }








        public void fillDGVsearch()
        {
            conn.Open();
            string sql = "Select * from tbl_visitor where Time_Out is null and Firstname='"+txtsearch.Text+"'";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            guna2DataGridView1.DataSource = dt;
            conn.Close();

        }


        private void btnlogout_Click(object sender, EventArgs e)
        {
            DialogResult repos = MessageBox.Show("Are you sure you want to log out?", "Logging Out", MessageBoxButtons.YesNo);
            if(repos == DialogResult.Yes)
            {
                this.Close();
                frmLogin Loginfrm = new frmLogin();
                Loginfrm.Show();
            }
            else
            {

            }

        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            frmfulldb dboard = new frmfulldb();
            dboard.ShowDialog();
        }

        private void btnrefresh_Click_1(object sender, EventArgs e)
        {
            fillDGVvisitor();
        }

        private void btnsearch_Click_1(object sender, EventArgs e)
        {
            fillDGVsearch();
            txtsearch.Clear();
        }

        private void btnletter_Click_1(object sender, EventArgs e)
        {
            letter frmletter = new letter();
            frmletter.Show();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            DialogResult repos = MessageBox.Show("Are you sure you want to close this application without logging out?", "Exiting.....", MessageBoxButtons.YesNo);
            if (repos == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {

            }
        }



        private void guna2HtmlLabel9_Click(object sender, EventArgs e)
        {

        }

        private void txtcontact_TextChanged_1(object sender, EventArgs e)
        {
            if (txtcontact.Text.Length > 10)
            {
                MessageBox.Show("Contact can only be up to 10");
                txtcontact.Clear();
            }
        }

        private void txtcontact_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.Handled=!char.IsDigit(e.KeyChar))
            {
                errorProvider1.SetError(label1, "Please enter only Numeric Values");
                label1.Text = "Please enter only Numeric Values";
                txtcontact.Clear();
            }
            else
            {
                errorProvider1.SetError(lblerror, "");
                lblerror.Text="";
            }
        }
    }
    }

