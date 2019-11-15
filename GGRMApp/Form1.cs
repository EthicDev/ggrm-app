using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGRMApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            NewCustomer add = new NewCustomer();
            add.Show();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            AddItem item = new AddItem();
            item.Show();
        }
    }
}
