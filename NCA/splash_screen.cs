using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCA
{
    public partial class splash_screen : Form
    {
        public splash_screen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gdpmini.Width += 3;
            if(gdpmini.Width >= 467)
            {
                timer1.Stop();
                frmMain mainfrm = new frmMain();
                mainfrm.Show();
                this.Hide();
            }
        }

        private void splash_screen_Load(object sender, EventArgs e)
        {

        }
    }
}
