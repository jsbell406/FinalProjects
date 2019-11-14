using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities.Models;
//using Service;

namespace Task2.Views
{
    public partial class NewField : Form
    {
        //IApplicationService service = new ApplicationServiceClient();

        public Field field;// = new Field();

        public NewField()
        {
            InitializeComponent();
            focusTxt.Hide();
            focusLbl.Hide();
            addBtn.Hide();
        }

        private void fieldTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void addFieldBtn_Click(object sender, EventArgs e)
        {
            string input = fieldTxt.Text;
            field = new Field();

            field.FieldOfStudy = input;

            //field = service.AddNewField(field);

            displayFieldLbl.Text = input;

            fieldTxt.Hide();
            fieldLbl.Hide();
            addFieldBtn.Hide();


            focusTxt.Show();
            focusLbl.Show();
            addBtn.Show();


        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            string input = focusTxt.Text;
            
            Focus focus = new Focus();

            //focus = service.AddNewFocus(focus, field.FieldId);
           
            
            

            //displayFociLbl.Text += "\n" + focus.Field.FieldOfStudy;
            displayFociLbl.Text += "\n" + focus.FocusForField;

        }

        private void focusTxt_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
