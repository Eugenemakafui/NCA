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

namespace NCA
{
    public partial class letter : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=.; Initial Catalog = NCA @ Reception; Integrated Security = True");
        public letter()
        {
            InitializeComponent();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            insertletter();
        }

        private void insertletter()
        {
            if (txtreceived.Text == "" || txtsubject.Text == "")
            {
                MessageBox.Show("Please fill the form");
            }

            else
            {
                try
                {
                    DateTime dt = DateTime.Now;
                    string n = dt.ToString("yyyy-MM-dd");

                    conn.Open();
                    string sql = "Insert into tbl_letter (Date_Received,To_Whom_Received,Date_Of_Letter,No_Of_Letter,Subject) VALUES(@DR,@TWR,@DOL,@NOL,@S)";
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.Parameters.AddWithValue("@DR", n);
                    command.Parameters.AddWithValue("@TWR", txtreceived.Text);
                    command.Parameters.AddWithValue("@DOL", dtpDOL.Value);
                    command.Parameters.AddWithValue("@NOL", nudletter.Value );
                    command.Parameters.AddWithValue("@S", txtsubject.Text);
                    command.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Letter Successfully Added");
                   
                }

                catch(Exception ex)
                {
                    MessageBox.Show(""+ex);
                }
                txtreceived.Clear();
                txtsubject.Clear();
                
            }
        }

    }
}