using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GGRMLib;
using GGRMLib.Models;

namespace GGRMApp
{
    public partial class NewCustomer : Form
    {
        public NewCustomer()
        {
            InitializeComponent();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            string status;
            Customer cust = new Customer(
                txtFirstName.Text,
                txtLastName.Text,
                txtPhone.Text,
                txtAddress.Text,
                txtCity.Text,
                txtPostal.Text,
                txtEmail.Text);
            GlobalConfig.Connection.CreateCustomer(cust, out status);
            MessageBox.Show(status, "Database Result");
        }
    }
}
