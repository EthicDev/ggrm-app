using GGRMLib;
using GGRMLib.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
            DataTable dtCustomers = GlobalConfig.Connection.GetCustomersDataTable(out status);
            dgvDataCustomers.DataSource = dtCustomers;
        }

        private void dgvDataCustomers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvDataCustomers.Columns["id"].Visible = false;
        }

        private void btnDataCustomersSearch_Click(object sender, EventArgs e)
        {
            string status;
            string searchString = txtDataCustomersSearch.Text;
            DataTable dtCustomers = GlobalConfig.Connection.GetCustomersDataTable(out status,searchString);
            dgvDataCustomers.DataSource = dtCustomers;
        }

        private void BtnDataCustomersEdit_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            string status;
            GlobalData.ViewData["editSelectedCustomer"] = GlobalConfig.Connection.GetCustomerByID((int)dgvCustomers.SelectedRows[0].Cells["id"].Value, out status);
            mainView.SelectedTab = subtabEditCustomer;
        }

        private void BtnDataCustomersAdd_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            mainView.SelectedTab = subtabNewCustomer;
        }
    }
}
