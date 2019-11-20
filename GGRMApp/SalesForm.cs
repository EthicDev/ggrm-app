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
    public partial class SalesForm : Form
    {
        public SalesForm()
        {
            InitializeComponent();
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            AddItemForm item = new AddItemForm();
            item.Show();
        }

        private void BtnCustomers_Click(object sender, EventArgs e)
        {
            CustomersForm cust = new CustomersForm();
            cust.Show();
        }
    }
}
