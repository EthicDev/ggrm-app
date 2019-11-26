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
        private void SubtabCustomers_Enter(object sender, EventArgs e)
        {
            string status;
            btnEditCustomer.Enabled = false;
            // Get customer objects from database
            //List<Customer> customers = new List<Customer>();
            //customers = GlobalConfig.Connection.GetCustomersList(out status);

            // Display list of customer data in DataGridView
            DataTable dtCustomers = GlobalConfig.Connection.GetCustomersDataTable(out status);
            dgvCustomers.DataSource = dtCustomers;

        }

        // Code to run after customer data is populated
        private void dgvCustomers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvCustomers.Columns["id"].Visible = false;
        }

        private void BtnCustomersBack_Click(object sender, EventArgs e)
        {
            mainView.SelectedTab = tabPOS;
        }

        private void BtnSelectCustomer_Click(object sender, EventArgs e)
        {
            string status;
            Customer selectedCust = GlobalConfig.Connection.GetCustomerByID((int)dgvCustomers.SelectedRows[0].Cells["id"].Value, out status);

            if (GlobalData.ViewData.ContainsKey("posSelectedCustomer"))
            {
                GlobalData.ViewData["posSelectedCustomer"] = selectedCust;
            }
            else
            {
                GlobalData.ViewData.Add("posSelectedCustomer", selectedCust);
            }
            mainView.SelectedTab = tabPOS;
        }
        private void BtnNewCustomer_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            mainView.SelectedTab = subtabNewCustomer;
        }
        private void dgvCustomers_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnEditCustomer.Enabled = true;
        }
        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            savePreviousTab();
            GlobalData.ViewData["editSelectedCustomerID"] = (int)dgvCustomers.SelectedRows[0].Cells["id"].Value;
            mainView.SelectedTab = subtabEditCustomer;
        }

        private void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            string status;

            DataTable dtCustomers = GlobalConfig.Connection.GetCustomersDataTable(out status, txtCustomerSearch.Text);
            dgvCustomers.DataSource = dtCustomers;
        }
    }
}
