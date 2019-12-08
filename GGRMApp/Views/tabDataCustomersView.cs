using GGRMLib;
using GGRMLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GGRMApp.Views
{
    public partial class Main : Form
    {
        private void tabDataCustomers_Enter(object sender, EventArgs e)
        {
            string status;
            List<Customer> listCustomers = GlobalConfig.Connection.GetCustomersList(out status);
            dgvDataCustomers.DataSource = listCustomers;
        }

        private void dgvDataCustomers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvDataCustomers.Columns["ID"].Visible = false;
        }

        private void btnDataCustomersSearch_Click(object sender, EventArgs e)
        {
            string status;
            string searchString = txtDataCustomersSearch.Text;
            List<Customer> listCustomers = GlobalConfig.Connection.GetCustomersList(out status, searchString);
            dgvDataCustomers.DataSource = listCustomers;
        }

    }
}
