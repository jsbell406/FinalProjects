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
    public partial class Main : Form
    {
        private int childFormNumber = 0;

        public Main()
        {
            InitializeComponent();
            Login loginForm = new Login();
            loginForm.main = this;
            loginForm.MdiParent = this;
       
            loginForm.MaximizeBox = false;
            loginForm.MinimizeBox = false;
            loginForm.ControlBox = false;
            loginForm.Dock = DockStyle.Fill;
            loginForm.Show();
            
            loginForm.Activate();
            
            
        }

       
    }
}
