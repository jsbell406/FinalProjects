using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task2.Views
{
    public partial class Login : Form
    {
        public Main main { get; set; }
        

        public Login()
        {
            InitializeComponent();
           
          
        }

        private void newAccountButton_Click(object sender, EventArgs e)
        {
            SelectAccountType selectAccoutForm = new SelectAccountType();
            selectAccoutForm.MdiParent = main;
            Hide();
            selectAccoutForm.MaximizeBox = false;
            selectAccoutForm.MinimizeBox = false;
            selectAccoutForm.ControlBox = false;
            selectAccoutForm.Dock = DockStyle.Fill;
            selectAccoutForm.Activate();
            selectAccoutForm.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
