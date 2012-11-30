using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace balda
{
    public partial class greeting : Form
    {
        public greeting()
        {
            InitializeComponent();
        }
        public static string str = "";
        public static string str2 = "";
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
         
            if (e.KeyCode == Keys.Enter)
            {
                str = textBox1.Text;
                textBox1.Enabled = false;
                textBox2.Enabled = true;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                str2 = textBox2.Text;
                textBox2.Enabled = false;
                button1.Enabled = true;
            }
        }

    }
}
