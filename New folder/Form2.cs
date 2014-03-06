using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication6
{
    public partial class Form2 : Form
    {
        public Form2(string str)
        {
            this.str = str;

            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void p_old_Click(object sender, EventArgs e)
        {

        }

        private void p_new2_Click(object sender, EventArgs e)
        {

        }

        string str;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          

            
            
           




            }

        public void textBox3_TextChanged(object sender, EventArgs e)
        {


            if (this.textBox2.Text == this.textBox3.Text)
            {

                reset.Enabled = true;

            }
            else
                reset.Enabled = false;
           


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void reset_Click(object sender, EventArgs e)
        {
        
             if(this.textBox3.Text==this.textBox2.Text)
            {


                this.DialogResult = DialogResult.OK;

                
               
                Close();





        }

        }
    }
}
